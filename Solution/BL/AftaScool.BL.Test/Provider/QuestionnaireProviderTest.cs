using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
using AftaScool.BL.Test;
using AftaScool.BL.Entities.QuestionnaireData;
using System.Diagnostics.CodeAnalysis;
using AftaScool.BL.Provider.QuestionnaireData;
using AftaScool.BL.Provider.LearnerData;
using AftaScool.BL.Context;
using AftaScool.BL.Provider.AssessorData;

namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class QuestionnaireProviderTest : ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.Questionnaire")]
        public void QuestionnaireSave()
        {



            var user = SeedData.CreateAdminUser(Context);
            IQuestionnaireProvider provider = new QuestionnaireProvider(Context, user);
            AssessorProvider assesso = new AssessorProvider(Context, user);
            ILearnerProvider learner = new LearnerProvider(Context, user);
            var learn = learner.LearnerSave(null, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
            var ass = assesso.createAssessor(null, user.Id);

            var questionnaire = provider.saveQuestionnaire(null, ass.Id, learn.Id, DateTime.Now);
            questionnaire.Id.ShouldNotBeNull();
            questionnaire.Id.ShouldBeGreaterThan(0);

        }
        [TestMethod]
        [TestCategory("Provider.Questionnaire")]
        public void QuestionnaireList()
        {
            var user = SeedData.CreateAdminUser(Context);
            IQuestionnaireProvider provider = new QuestionnaireProvider(Context, user);
            AssessorProvider assesso = new AssessorProvider(Context, user);
            ILearnerProvider learner = new LearnerProvider(Context, user);
            var learn = learner.LearnerSave(null, "Piet", "Test LearnerSurname", "grade", "9002280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
            var learn2 = learner.LearnerSave(null, "Piet T", " LearnerSurname", "grade", "9002280168044", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
            var ass = assesso.createAssessor(null, user.Id);
            var school = provider.saveQuestionnaire(null, ass.Id, learn.Id, DateTime.Today);
            var school1 = provider.saveQuestionnaire(null, ass.Id, learn2.Id, DateTime.Today);

            var x = provider.GetQuestionnaires();


        }
        /* [TestMethod]
         [TestCategory("Provider.Questionnaire")]
         public void QuestionnaireArchive()
         {
             var user = SeedData.CreateAdminUser(Context);
             IQuestionnaireProvider provider = new QuestionnaireProvider(Context, user);
             var questionnaire = provider.saveQuestionnaire(null, 6, 7, DateTime.Today);

             provider.ArchiveQuestionnaire(user.Id);


         }*/


    }
}
