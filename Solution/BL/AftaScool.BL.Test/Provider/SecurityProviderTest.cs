using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftaScool.BL.Provider.Security;
using SoftwareApproach.TestingExtensions;
using TCR.Lib.BL;
using AftaScool.BL.Util;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Types;

namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SecurityProviderTest : ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.Security")]
        public void UserLogin()
        {
            //Setup
            ISecurityProvider Provider = new SecurityProvider(Context);
            var testUser = SeedData.CreateUser(Context, "testUser", "password@1");
            
            //Act / invoke
            var user = Provider.UserLogin(testUser.UserName, "password@1");

            //Assert / Test
            user.ShouldNotBeNull();
            user.UserName.ShouldEqual(testUser.UserName);
            user.AllowedPrivileges.ShouldNotBeNull();
            Provider.CurrentUser.ShouldNotBeNull();
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        [ExpectedException(typeof(SecurityException))]
        public void UserLoginFailed()
        {
            ISecurityProvider Provider = new SecurityProvider(Context);

            //setup user
            var testUser = SeedData.CreateUser(Context, "test@mail.com", "password@1");
            //setup data for test
            var user = Provider.UserLogin(testUser.UserName, "password@2");//wrong password
            user.ShouldNotBeNull();
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void SignUp()
        {
            ISecurityProvider Provider = new SecurityProvider(Context);
            var usr = Provider.SignUp("testuser", "password@1", "user@mail.com", "Mr", "Johan", "van Wyk", "271125112451", Entities.SecurityData.GenderType.Male, "+27820000000",
                "21 Witkoppen Street", "Sandton", "JoBurg", "2000");
            //create user
            usr.ShouldNotBeNull();
            usr.Id.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void SaveRole()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);
            var privileges = SeedData.GetPrivileges(Context);
            var createRole = provider.SaveRole(null, "MVP", "You master it all.", privileges);

            //then            
            createRole.RoleName.ShouldEqual("MVP");
            createRole.Privileges.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void GetRole()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider= new SecurityProvider(Context, me);
            var role = SeedData.CreateRole(Context, "Developer");
            //when
            var getRole = provider.GetRole(role.Id);
            //then 
            getRole.Id.ShouldBeGreaterThan(0);
            getRole.RoleName.ShouldEqual("Developer");
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void GetRoles()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);
            var role1 = SeedData.CreateRole(Context, "Director");
            var role2 = SeedData.CreateRole(Context, "Mentor");
            var role3 = SeedData.CreateRole(Context, "Secretary");

            //when
            var roles = provider.GetRoles();

            //then   
            roles.Count().ShouldEqual(4);
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void DeleteRole()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);
            var newRole = SeedData.CreateRole(Context, "Director");

            //when
            provider.DeleteRole(newRole.Id);
            
            //then   
            provider.GetRole(newRole.Id).ShouldBeNull();            
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void ArchiveRole()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);

            var newRole = SeedData.CreateRole(Context, "Mentor");
            
            //when     
            provider.ArchiveRole(newRole.Id);
            var role = provider.GetRole(newRole.Id);
            
            
            //then 
            role.RoleName.ShouldEqual("Mentor");
            role.Status.ShouldEqual(StatusType.Archive);
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void GetUser()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context, "David");
            ISecurityProvider provider = new SecurityProvider(Context, me);
            //when
            var getUser = provider.GetUser(me.Id);
            //then   
            getUser.Id.ShouldBeGreaterThan(0);
            getUser.UserName.ShouldEqual("David");
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void UserExists()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);
            var dummyUser = SeedData.CreateAdminUser(Context, "Lexus");
            //when
            var userExist = provider.UserExists("Lexus");
            //then   
            userExist.ShouldBeTrue();
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void SaveUser()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);

            var privileges = SeedData.GetPrivileges(Context);

            var roles = SeedData.GetRoles(Context);

            var createRole = provider.SaveRole(null, "Tester", "Write and manage Unit Tests", privileges);
            
            var newSignUp = provider.SignUp("Martian", "loyal007", "007@cia.gov", "Mr", "James",
                                            "Bond", "01111111110", GenderType.Male, "+440000000",
                                            "123 Old Trafford", "UK", "Manchester", "0000");

            //when
            var u = provider.GetUser(newSignUp.Id);
            
            var saveUser = provider.SaveUser(null, u.UserName, u.PasswordHash, u.EmailAddress, u.Title,
                                             u.FirstName, u.Surname, u.IdPassportNum, u.Gender, u.Telephone,
                                             u.AddressLine1, u.AddressLine2, u.City, u.PostalCode, roles, true);
            //then   
            saveUser.UserName.ShouldEqual("Martian");
            saveUser.AddressLine2.ShouldEqual("UK");
            saveUser.Gender.ShouldEqual(GenderType.Male);
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void ChangePassword()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            var dummyUser = SeedData.CreateUser(Context, "Terminator", "Asta la vista");
            ISecurityProvider provider = new SecurityProvider(Context, me);
            //when
            var setNewPassword = provider.ChangePassword(dummyUser.Id, "John Connor"); 
            //then  
            setNewPassword.ShouldBeTrue();
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void GetUserList()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            var dummyUser = SeedData.CreateUser(Context, "Terminator", "Asta la vista");
            var dummyUser2 = SeedData.CreateUser(Context, "Superman", "Cryptonite");
            ISecurityProvider provider = new SecurityProvider(Context, me);

            //when
            var getUsers = provider.GetUserList();

            //then   
            getUsers.Count().ShouldEqual(3);
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void LockAccount()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            var dummyUser = SeedData.CreateAdminUser(Context, "Lexus");
            ISecurityProvider provider = new SecurityProvider(Context, me);

            //when
            var lockAccount = provider.LockAccount(dummyUser.Id);
            //then   
            lockAccount.ShouldNotBeNull();
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void UnlockAccount()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            var dummyUser = SeedData.CreateAdminUser(Context, "Steve");
            ISecurityProvider provider = new SecurityProvider(Context, me);
            var lockAccount = provider.LockAccount(dummyUser.Id);
            //when
            var unlock = provider.UnlockAccount(dummyUser.Id);

            //then   
            unlock.LockedOut.ShouldBeNull();
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void DeactivateAccount()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);
            var secondUser = SeedData.CreateAdminUser(Context, "Njabulo");
            //when
            var deactivate = provider.DeactivateAccount(secondUser.Id);
            //then   
            deactivate.Deactivated.ShouldNotBeNull();
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void ActivateAccount()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);
            var secondUser = SeedData.CreateAdminUser(Context, "Thuli");
            var deactivate = provider.DeactivateAccount(secondUser.Id);
            //when
            var activate = provider.ActivateAccount(secondUser.Id);
            //then   
            activate.Deactivated.ShouldBeNull();
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void ChangeMyPassword()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);          

            //when
            var changePassword = provider.ChangeMyPassword("password@1", "#UnitTest");

            //then   

            changePassword.PasswordHash.ShouldEqual(changePassword.PasswordHash);
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void GetMyProfile()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);
           
            //when
            var profile = provider.GetMyProfile();
           
            //then   
            profile.UserName.ShouldEqual("AdminUser");
            profile.IsSystemAdmin.ShouldBeFalse();
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void SaveMyProfile()
        {
            //given 
            var me = SeedData.CreateAdminUser(Context);
            ISecurityProvider provider = new SecurityProvider(Context, me);
            
            //when
            var profile = provider.SaveMyProfile("MVP", "David", "Letaoana", "0800111213", "Unit 3", "Berea Rd", null, "4000");
            //then   
            profile.Id.ShouldEqual(me.Id);
            profile.City.ShouldBeNull();
            profile.Title.ShouldEqual("MVP");
        }

        [TestMethod]
        [TestCategory("Provider.Security")]
        public void ResetMyPassword()
        {
            //given 
            ISecurityProvider provider = new SecurityProvider(Context);
            //when
            
            //then   
        }

    }
}
