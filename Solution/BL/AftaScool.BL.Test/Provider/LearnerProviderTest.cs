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

namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LearnerProviderTest : ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.Learner")]
        public void SaveLearner()
        {
            var accountUser = SeedData.CreateAdminUser(Context);
            ILearnerProvider provider = new LearnerProvider(Context, accountUser);

            //Act
            var learner = provider.LearnerSave(null, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");

            //Test
            learner.ShouldNotBeNull();
            learner.Id.ShouldBeGreaterThan(0);
            learner.Status.ShouldEqual(Types.StatusType.Active);
        
        }
         [TestMethod]
        [TestCategory("Provider.Learner")]
        public void LearnerArchive()
        {
            //Setup
            var accountUser = SeedData.CreateAdminUser(Context);
            ILearnerProvider provider = new LearnerProvider(Context, accountUser);
            var learner = provider.LearnerSave(null, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Male, "address line1", "address line 2", "Centurion", "0124", "0113450000");

            //Act
            provider.ArchiveLearner(learner.Id);

            //Test
            var testLearner = provider.GetLearners().Where(a => a.Id == learner.Id).Single();
            testLearner.Status.ShouldEqual(Types.StatusType.Archive);

        }
         [TestMethod]
         [TestCategory("Provider.Learner")]
         public void LearnerListTest()
         {
             var accountUser = SeedData.CreateAdminUser(Context);
             ILearnerProvider provider = new LearnerProvider(Context, accountUser);
             var learner = provider.LearnerSave(null, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Male, "address line1", "address line 2", "Centurion", "0124", "0113450000");
             var learner2 = provider.LearnerSave(null, "Test LearnerName2", "Test LearnerSurname2", "grade2", "9102280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113550000");
        
             //Act
             var z = provider.GetLearners().Count();

             //Test
             z.ShouldEqual(2);
         }

    }
}
