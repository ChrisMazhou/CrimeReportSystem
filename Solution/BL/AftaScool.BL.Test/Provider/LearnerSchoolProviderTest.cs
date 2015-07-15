using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
using AftaScool.BL.Test;
using System.Diagnostics.CodeAnalysis;
using AftaScool.BL.Entities.LearnerData;
using AftaScool.BL.Provider.LearnerData;
using AftaScool.BL.Provider.SchoolData;


namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LearnerSchoolProviderTest : ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.Test")]
        public void saveLearnerSchool()
        {

            /*  public School SchoolSave(long? id, string schoolName, string addressline1, string addressline2, string city, string postalCode)*/
            var user = SeedData.CreateAdminUser(Context);
            ISchoolProvider skul = new SchoolProvider(Context, user);
            ILearnerProvider learner = new LearnerProvider(Context, user);
            LearnerSchoolProvider provider = new LearnerSchoolProvider(Context, user);
            var learn = learner.LearnerSave(null, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
            var skuul = skul.SchoolSave(null, "Carel De Vet HighSchool", "145 Gamyn Ave", "North", "Vereniging", "0001");


            var lea = provider.saveLearnerSchool(null, learn.Id, skuul.Id, DateTime.Today, DateTime.Today.AddDays(6));

         //   var learnerSchool = provider.saveLearnerSchool(null, learn.Id, skuul.Id, DateTime.Today, DateTime.Today.AddDays(6));
        }
        [TestMethod]
        [TestCategory("Provider.LearnerSchool")]
        public void GetLearnerSchools()
        {
            var user = SeedData.CreateAdminUser(Context);
            ILearnerSchoolProvider provider = new LearnerSchoolProvider(Context, user);
            ISchoolProvider skul = new SchoolProvider(Context, user);
            ILearnerProvider learner = new LearnerProvider(Context, user);
            var learn = learner.LearnerSave(null, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
            var skuul = skul.SchoolSave(null, "Carel De Vet HighSchool", "145 Gamyn Ave", "North", "Vereniging", "0001");


            var lea = provider.saveLearnerSchool(null, learn.Id, skuul.Id, DateTime.Today, DateTime.Today.AddDays(6));
            //Act
            var l = provider.GetLearnerSchools();



        }
    }
}