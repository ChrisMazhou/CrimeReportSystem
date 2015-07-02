using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftaScool.BL.Context;
using AftaScool.BL.Entities.SecurityData;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AftaScool.BL.Util;
using AftaScool.BL.Provider.Security;
using TCR.Lib.Utility;
using AfterScool.BL.Entities.SecurityData;

namespace AftaScool.BL.Test
{
    public static class SeedData
    {
        #region Security

        public static ICurrentUser CreateAdminUser(DataContext context, string userName = "AdminUser", string password = "password@1")
        {
            var result = CreateUser(context, userName, password);
            var role = CreateRole(context);
            var curr = result.Roles.Where(a => a.RoleName == role.RoleName).SingleOrDefault();
            if (curr == null)
            {
                result.Roles.Add(role);
                context.SaveChanges();
            }
            var qPrivs = from userP in context.UserIdentitySet
                         from r in userP.Roles
                         from privileges in r.Privileges
                         where userP.Id == result.Id
                         select privileges.Security;
            result.AllowedPrivileges = qPrivs.Distinct().ToList();

            var provider = new SecurityProvider(context);
            return provider.UserIdentityToCurrentUser(result);
        }

        public static Role CreateRole(DataContext context, string roleName = "admin", bool hasAllPrivileged = true)
        {
            Role r = context.RoleSet.Include(a => a.Privileges).Where(a => a.RoleName == roleName).SingleOrDefault();
            if (r == null)
            {
                r = new Role()
                {
                    RoleName = roleName,
                    Description = "My Role Description",
                    Privileges = new List<Privilege>()
                };
                context.RoleSet.Add(r);
            }
            if (hasAllPrivileged)
            {
                foreach (PrivilegeType p in Enum.GetValues(typeof(PrivilegeType)))
                {
                    var priv = context.PrivilegeSet.Where(a => a.Security == p).SingleOrDefault();
                    if (priv == null)
                    {
                        priv = new Privilege()
                        {
                            Description = NameSplitting.SplitCamelCase(p),
                            Security = p
                        };
                        context.PrivilegeSet.Add(priv);
                    }
                    var curr = r.Privileges.Where(a => a.Security == priv.Security).SingleOrDefault();
                    if (curr == null)
                        r.Privileges.Add(priv);
                }
            }
            context.SaveChanges();
            return r;
        }

        public static UserIdentity CreateUser(DataContext context, string userName = "James", string password = "Password",
        string title = "Mr", string firstName = "James", string surname = "Mccall", string idNumber = "7005245602074", GenderType gender = GenderType.Male,
        string Telephone = "0723257865", string email = "james@pede.org", string streetName = "3559 Tempor Street", string city = "Gianco", string postalCode = "1899")
        {
            try
            {
                //fetch according to unique index
                var current = context.UserIdentitySet
                                         .Include(a => a.Roles)
                                         .Where(a => a.UserName == userName).SingleOrDefault(); //check against unique index
                if (current == null) //if not found in db 
                {
                    //create instance of entity and insert to table.
                    current = new Entities.SecurityData.UserIdentity();
                    current.Roles = new List<Role>();
                    context.UserIdentitySet.Add(current);
                }

                //set all attributes - this will update if existing, or insert if new
                current.UserName = userName;
                current.Title = title;
                current.FirstName = firstName;
                current.Surname = surname;
                current.IdPassportNum = idNumber;
                current.Gender = gender;
                current.PasswordHash = Cipher.Encrypt(password);
                current.Telephone = Telephone;
                current.EmailAddress = email;
                current.AddressLine1 = streetName;
                current.City = city;
                current.PostalCode = postalCode;
                current.Active = true;

                context.SaveChanges(); //commit changes
                return current;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                throw e;
            }
        }


        #endregion

    

        public static List<Privilege> GetPrivileges(DataContext context)
        {
            var p = context.PrivilegeSet.OrderBy(x => x.Id).Take(2);

            return p.ToList();
        }

        public static List<long> GetRoles(DataContext context)
        {
            var r = context.RoleSet.OrderBy(a => a.Id).Select(x=>x.Id).Take(2);

            return r.ToList();
        }
    }
}
