using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
using AftaScool.BL.Test;
using AftaScool.BL.Entities.LearnerData;
using System.Diagnostics.CodeAnalysis;
using AftaScool.BL.Provider.LearnerData;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.AssessorData;

namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AssessorProviderTest : ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.Assessor")]
        public void SaveAssessor()
        {
            var accountUser = SeedData.CreateAdminUser(Context);
            IAssessorProvider provider = new AssessorProvider(Context, accountUser);


            //Act
            var assessor = provider.createAssessor(null, accountUser.Id);

            //Test
            assessor.ShouldNotBeNull();
            assessor.Id.ShouldBeGreaterThan(0);

        }
        [TestMethod]
        [TestCategory("Provider.Assesor")]
        public void AssessorArchive()
        {
            //Setup
            var accountUser = SeedData.CreateAdminUser(Context);

            IAssessorProvider provider = new AssessorProvider(Context, accountUser);
            var assessor = provider.createAssessor(null, accountUser.Id);


            //Act


            //Test
            var testAssessor = provider.GetAssessor();




        }
       

    }
}