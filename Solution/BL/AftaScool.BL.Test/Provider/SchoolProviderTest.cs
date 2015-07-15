using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
using AftaScool.BL.Test;
using AftaScool.BL.Entities.SchoolData;
using System.Diagnostics.CodeAnalysis;
using AftaScool.BL.Provider.SchoolData;

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
            var user = SeedData.CreateAdminUser(Context);
            ISchoolProvider provider = new SchoolProvider(Context,user);
            
            var school = provider.SchoolSave(null, "Carel De Vet HighSchool", "145 Gamyn Ave", "North", "Vereniging", "0001");
            
            school.Id.ShouldBeGreaterThan(0);
            
        }
        [TestMethod]
        [TestCategory("Provider.School")]
        public void SchoolList()
        {
            var user = SeedData.CreateAdminUser(Context);
            ISchoolProvider provider = new SchoolProvider(Context,user);
            var school = provider.SchoolSave(null, "Meyerton HighSchool", "22 Addee Ave", "North", "Meyerton", "0003");
            var school1 = provider.SchoolSave(null, "Thebisa HighSchool", "22 Addee Ave", "West", "Soweto", "0002");
            var school2 = provider.SchoolSave(null, "Katleho HighSchool", "11 Phere Street", "Sebokeng", "Vanderbiljpark", "0004");

            var x = provider.GetSchools();
        }
        [TestMethod]
        [TestCategory("Provider.School")]
        public void SchoolArchive()
        {
            var user = SeedData.CreateAdminUser(Context);
            ISchoolProvider provider = new SchoolProvider(Context, user);
            //var school = provider.SchoolSave(null, "Vaal HighSchool", "156 Roydeen Ave", "Three Rivers", "Vereeniging", "0012");

            //provider.ArchiveSchool(user.Id);

            
        }


    }
}
