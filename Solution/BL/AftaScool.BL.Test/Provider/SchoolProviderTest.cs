using AftaScool.BL.Entities.SchoolData;
using AftaScool.BL.Provider.SchoolData;
using AftaScool.BL.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
using System;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SchoolProviderTest:ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.School")]
        public void SchoolSave()
        {
            //SetUp
            var user = SeedData.CreateAdminUser(Context);
            ISchoolProvider provider = new SchoolProvider(Context,user);
            
            //Act
            var school = provider.SchoolSave(null, "Carel De Vet HighSchool", "145 Gamyn Ave", "North", "Vereniging", "0001");
            
            //Test
            school.Id.ShouldBeGreaterThan(0);
            school.ShouldNotBeNull();
        }
        [TestMethod]
        [TestCategory("Provider.School")]
        public void SchoolList()
        {
            //SetUp
            var user = SeedData.CreateAdminUser(Context);
            ISchoolProvider provider = new SchoolProvider(Context,user);
            var school = provider.SchoolSave(null, "Meyerton HighSchool", "22 Addee Ave", "North", "Meyerton", "0003");
            var school1 = provider.SchoolSave(null, "Thebisa HighSchool", "22 Addee Ave", "West", "Soweto", "0002");
            var school2 = provider.SchoolSave(null, "Katleho HighSchool", "11 Phere Street", "Sebokeng", "Vanderbiljpark", "0004");

            //Act
            var chkSchool = provider.GetSchools().Count();

            //Test
            chkSchool.ShouldEqual(3);
            
        }
        [TestMethod]
        [TestCategory("Provider.School")]
        public void ArchiveSchool()
        {
            //SetUp
            var user = SeedData.CreateAdminUser(Context);
            ISchoolProvider provider = new SchoolProvider(Context, user);
            var school = provider.SchoolSave(null, "Vaal HighSchool", "156 Roydeen Ave", "Three Rivers", "Vereeniging", "0012");

            //Act
            provider.ArchiveSchool(user.Id);

            //Test
            var testSchool = provider.GetSchools().Where(a => a.Id == school.Id).Single();
            
        }


    }
}
