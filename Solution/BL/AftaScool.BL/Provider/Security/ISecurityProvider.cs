using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Email;
using AftaScool.BL.Settings;
using AftaScool.BL.Types;

namespace AftaScool.BL.Provider.Security
{
    public interface ISecurityProvider:IAftaScoolProvider
    {
        #region User Login

        ICurrentUser UserLogin(string userName, string password);
        ICurrentUser UserIdentityToCurrentUser(UserIdentity user);
        ICurrentUser LoadUserWithAccess(long userIdentityId);

        #endregion

        #region Roles
        Role SaveRole(long? roleId, string name, string description, List<Privilege> privileges);

        Role GetRole(long roleId);

        IQueryable<Role> GetRoles();

        void DeleteRole(long roleId);
        void ArchiveRole(long id);

        #endregion

        #region User Maintenance

        UserIdentity GetUser(long userId);

        bool UserExists(string userName);

        bool ChangePassword(long userId, string password);
        UserIdentity SignUp(string userName, string password, string emailAddress,
                            string title, string firstName, string surname,
                            string idOrPassportNumber, GenderType gender,
                            string telephone, string addressLine1, string addressLine2,string city, string postalCode);

        UserIdentity SaveUser(long? id, string userName, string password, string emailAddress,
                              string title, string firstName, string surname,
                               string idOrPassportNumber, GenderType gender,
                              string telephone, string addressLine1, string addressLine2, string city, string postalCode, List<long> allowedRoles, bool isAdmin);


     
        IQueryable<UserIdentity> GetUserList();

        UserIdentity LockAccount(long userId);
        UserIdentity UnlockAccount(long userId);

        UserIdentity DeactivateAccount(long userId);
        UserIdentity ActivateAccount(long userId);

        #endregion

        #region Manage Current Loggedin user
        
        //changed from ICurrentUser to UserIdentity
        UserIdentity ChangeMyPassword(string oldPassword, string newPassword);
   
        UserIdentity GetMyProfile();

        UserIdentity SaveMyProfile(string title, string firstName, string surname, string phone, string firstAddress, string secondAddress, string city, string code);
      
        bool ResetMyPassword(string emailAddress, IEmailProvider emailProvider);

        #endregion
        
    }
}
