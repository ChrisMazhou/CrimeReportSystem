using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCR.Lib.BL;
using AftaScool.BL.Context;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Email;
using AftaScool.BL.Settings;
using AftaScool.BL.Types;
using AftaScool.BL.Util;
using System.Data.Entity;
using AftaScool.BL.Provider.Security;

namespace AftaScool.BL.Provider.Security
{
    public class SecurityProvider : AftaScoolProvider, ISecurityProvider
    {
        #region Ctor

        public SecurityProvider(DataContext context)
            : this(context, null)
        {

        }

        public SecurityProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        {

        }
        #endregion

        public ICurrentUser UserLogin(string userName, string password)
        {
            var result = DataContext.UserIdentitySet.Where(x => x.UserName == userName).SingleOrDefault(); 

            if (result == null || result.PasswordHash != Cipher.Encrypt(password))
            {
                if (result != null)
                {
                    result.FailedLoginAttempts = result.FailedLoginAttempts + 1;
                    if (result.FailedLoginAttempts >= 3)
                        result.LockedOut = DateTime.Now;
                    DataContextSaveChanges();
                }

                throw new SecurityException("Invalid user name and password combination.");
            }

            if (result.Deactivated != null)
                throw new SecurityException("Account Deactivated!");

            if (result.LockedOut != null)
                throw new SecurityException("Account locked out!");

            if (result.Active == false)
                throw new SecurityException("Account Deactivated!");

            result.LastLogin = DateTime.Now;

            if (result.FailedLoginAttempts > 0)
                result.FailedLoginAttempts = 0;

            DataContextSaveChanges();
      
            var qPrivs = from userP in DataContext.UserIdentitySet
                         from role in userP.Roles
                         from privileges in role.Privileges
                         where userP.Id == result.Id
                         select privileges.Security;

            result.AllowedPrivileges = qPrivs.Distinct().ToList();

            CurrentUser = UserIdentityToCurrentUser(result);

            return CurrentUser;
        }

        public ICurrentUser UserIdentityToCurrentUser(UserIdentity user)
        {
            return new LoggedInUser()
            {
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = user.FirstName + " " + user.Surname,
                AllowedPrivileges = user.AllowedPrivileges,
                IsSystemAdmin = user.IsSystemAdmin
            };
        }

        public ICurrentUser LoadUserWithAccess(long userIdentityId)
        {
            throw new NotImplementedException();
        }

        #region Role
        public Role SaveRole(long? roleId, string name, string description, List<Privilege> privileges)
        {
            Authenticate(PrivilegeType.RoleMaintenance);

            var role = DataContext.RoleSet.Include(a => a.Privileges).Where(r => r.RoleName == name).SingleOrDefault();

            if (role != null)
                throw new SecurityException(role + " role already exist.");
            else
            {
                if (roleId != null)
                {
                    role = DataContext.RoleSet.Include(a => a.Privileges).Where(r => r.Description == description).Single();
                }
                else
                {
                    role = new Role();
                    DataContext.RoleSet.Add(role);
                }
            }

            role.RoleName = name;
            role.Description = description;
            role.Privileges = privileges;

            DataContextSaveChanges();

            return role;
        }

        public Role GetRole(long roleId)
        {
            Authenticate(PrivilegeType.RoleMaintenance);

            var role = new Role();

            var q = DataContext.RoleSet.Include(p => p.Privileges).Where(r => r.Id == roleId).SingleOrDefault();

            if (q == null)
               return null;

            role.Id = q.Id;
            role.Status = q.Status;
            role.RoleName = q.RoleName;
            role.Privileges = q.Privileges.ToList();
            role.Description = q.Description;

            return role;
        }

        public IQueryable<Role> GetRoles()
        {
            Authenticate(PrivilegeType.RoleMaintenance);

            return DataContext.RoleSet.Include(a => a.Privileges);
        }

        public void DeleteRole(long roleId)
        {
            Authenticate(PrivilegeType.RoleMaintenance);

            var role = DataContext.RoleSet.Where(r => r.Id == roleId).Single();

            DataContext.RoleSet.Remove(role);

            DataContextSaveChanges();
        }


        public void ArchiveRole(long id)
        {
            Authenticate(PrivilegeType.RoleMaintenance);

            var role = DataContext.RoleSet.Where(r => r.Id == id).Single();

            if (role.Status == StatusType.Archive)
                throw new SecurityException("Role already archived.");

            role.Status = StatusType.Archive;

            DataContextSaveChanges();
        }
        
        #endregion

        public UserIdentity GetUser(long userId)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var userIdentity = DataContext.UserIdentitySet.Where(a => a.Id == userId).Single();

            if (userIdentity.Active == false)
                throw new SecurityException("User account is not active.");
            if (userIdentity.LockedOut != null)
                throw new SecurityException("User account is locked.");
            if (userIdentity.Deactivated != null)
                throw new SecurityException("User account was deactivated.");

            //obtain the user record          
            var u = DataContext.UserIdentitySet.Where(a => a.Id == userId).Single();

            //set the values 
            userIdentity.Title = u.Title;
            userIdentity.FirstName = u.FirstName;
            userIdentity.Surname = u.Surname;
            userIdentity.UserName = u.UserName;
            userIdentity.EmailAddress = u.EmailAddress;
            userIdentity.AddressLine1 = u.AddressLine1;
            userIdentity.AddressLine2 = u.AddressLine2;
            userIdentity.City = u.City;
            userIdentity.PostalCode = u.PostalCode;
            userIdentity.IdPassportNum = u.IdPassportNum;
            userIdentity.Gender = u.Gender;

            return userIdentity;
        }

        public bool UserExists(string userName)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var userExist = false;

            var thisUser = DataContext.UserIdentitySet.Where(u => u.UserName == userName).SingleOrDefault();

            if (thisUser != null)
                userExist = true;
            else
                userExist = false;

            return userExist;
        }

        public UserIdentity SignUp(string userName, string password, string emailAddress, string title, string firstName, string surname,
                                    string idOrPassportNumber, GenderType gender, string telephone, string addressLine1, string addressLine2,
                                    string city, string postalCode)
        {
            var curr = DataContext.UserIdentitySet.Where(a => a.UserName == userName).SingleOrDefault();

            if (curr != null)
                throw new SecurityException("User already exists");

            var usr = new UserIdentity()
            {
                UserName = userName,
                Active = true,
                Title = title,
                FirstName = firstName,
                Surname = surname,
                IdPassportNum = idOrPassportNumber,
                Gender = gender,
                PasswordHash = Cipher.Encrypt(password),
                Telephone = telephone,
                EmailAddress = emailAddress,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                City = city,
                PostalCode = postalCode,
                FailedLoginAttempts = 0,
                IsSystemAdmin = false
            };

            DataContext.UserIdentitySet.Add(usr);

            DataContextSaveChanges();

            return usr;
        }

        public UserIdentity SaveUser(long? id, string userName, string password, string emailAddress, string title, string firstName, string surname,
                                    string idOrPassportNumber, GenderType gender, string telephone, string addressLine1, string addressLine2, string city,
                                    string postalCode, List<long> allowedRoles, bool isAdmin)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var newUser = DataContext.UserIdentitySet.Where(u => u.UserName == userName).SingleOrDefault();

            if (newUser != null)
            {
                if (id != null && newUser.Id == id)
                    newUser = DataContext.UserIdentitySet.Where(u => u.Id == id).Single();
            }
            else
            {
                if (id == null)
                {
                    newUser = new UserIdentity();
                    DataContext.UserIdentitySet.Add(newUser);
                }
            }

            newUser.UserName = userName;
            newUser.Title = title;
            newUser.FirstName = firstName;
            newUser.Surname = surname;
            newUser.IdPassportNum = idOrPassportNumber;
            newUser.Telephone = telephone;
            newUser.EmailAddress = emailAddress;
            newUser.AddressLine1 = addressLine1;
            newUser.AddressLine2 = addressLine2;
            newUser.City = city;
            newUser.PostalCode = postalCode;
            newUser.IsSystemAdmin = isAdmin;

            if (allowedRoles != null)
            {
                var roles = new List<Role>();
                foreach (var r in allowedRoles)
                {
                    roles.Add(DataContext.RoleSet.Where(x => x.Id == r).SingleOrDefault());
                }
                newUser.Roles = roles;
            }

            DataContextSaveChanges();

            return newUser;
        }

        public bool ChangePassword(long userId, string password)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var result = false;

            var me = DataContext.UserIdentitySet.Where(m => m.Id == userId).Single();

            if (me.Deactivated != null)
                throw new SecurityException("User account is deactivated");
            else if (me.LockedOut != null)
                throw new SecurityException("User account is locked.");
            else
            {
                me.PasswordHash = Cipher.Encrypt(password);
                result = true;
            }

            DataContextSaveChanges();

            return result;
        }

        public IQueryable<UserIdentity> GetUserList()
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var userList = from u in DataContext.UserIdentitySet
                           orderby u.Active, u.UserName, u.IsSystemAdmin ascending
                           select u;
            return userList;
        }

        public UserIdentity LockAccount(long userId)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var thisUser = DataContext.UserIdentitySet.Where(u => u.Id == userId).Single();

            if (thisUser.LockedOut != null)
                throw new SecurityException("Account already locked.");
            else
                thisUser.LockedOut = DateTime.Today;

            DataContextSaveChanges();

            return thisUser;
        }

        public UserIdentity UnlockAccount(long userId)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var thisUser = DataContext.UserIdentitySet.Where(u => u.Id == userId).Single();

            thisUser.LockedOut = null;

            DataContextSaveChanges();

            return thisUser;
        }

        public UserIdentity DeactivateAccount(long userId)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var thisUser = DataContext.UserIdentitySet.Where(u => u.Id == userId).Single();

            if (thisUser.Deactivated != null)
                throw new SecurityException("Account already deactivated.");
            else
                thisUser.Deactivated = DateTime.Today;

            DataContextSaveChanges();

            return thisUser;
        }

        public UserIdentity ActivateAccount(long userId)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var thisUser = DataContext.UserIdentitySet.Where(u => u.Id == userId).Single();

            thisUser.Deactivated = null;

            DataContextSaveChanges();

            return thisUser;
        }

        public UserIdentity ChangeMyPassword(string oldPassword, string newPassword)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var me = DataContext.UserIdentitySet.Where(m => m.Id == CurrentUser.Id).Single();

            me.PasswordHash = Cipher.Encrypt(newPassword);

            DataContextSaveChanges();

            return me;
        }

        public UserIdentity GetMyProfile()
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var profile = new UserIdentity();
            var me = DataContext.UserIdentitySet.Where(u => u.Id == CurrentUser.Id).Single();

            profile.FirstName = me.FirstName;
            profile.Title = me.Title;
            profile.Surname = me.Surname;
            profile.UserName = me.UserName;
            profile.EmailAddress = me.EmailAddress;
            profile.AddressLine1 = me.AddressLine1;
            profile.AddressLine2 = me.AddressLine2;
            profile.City = me.City;
            profile.PostalCode = me.PostalCode;
            profile.IdPassportNum = me.IdPassportNum;
            profile.Gender = me.Gender;

            return profile;

        }

        public UserIdentity SaveMyProfile(string title, string firstName, string surname, string phone, string firstAddress
                                        , string secondAddress, string city, string code)
        {
            Authenticate(PrivilegeType.UserMaintenance);

            var myProfile = DataContext.UserIdentitySet.Where(u => u.Id == CurrentUser.Id).Single();

            myProfile.Title = title;
            myProfile.FirstName = firstName;
            myProfile.Surname = surname;
            myProfile.AddressLine1 = firstAddress;
            myProfile.AddressLine2 = secondAddress;
            myProfile.City = city;
            myProfile.PostalCode = code;

            DataContextSaveChanges();

            return myProfile;
        }

        public bool ResetMyPassword(string emailAddress, IEmailProvider emailProvider)
        {
            var user = DataContext.UserIdentitySet.Where(h => h.EmailAddress == emailAddress).SingleOrDefault();

            var isReset = false;
            var resetPassword = "#Brain_Dead";
            var encryptedPassword = Cipher.Encrypt(resetPassword);

            if (user == null)
                throw new SecurityException("This email account " + emailAddress + " does not exist in our system.");


            if (user.Deactivated != null || user.LockedOut != null)
                throw new SecurityException("You account is not active, please contact the administrator.");

            user.PasswordHash = encryptedPassword;

            emailProvider.SendPasswordResetEmail(user, resetPassword);

            DataContextSaveChanges();

            isReset = true;

            return isReset;
        }
    }
}
