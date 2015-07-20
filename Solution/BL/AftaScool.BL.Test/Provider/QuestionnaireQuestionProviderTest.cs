using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AftaScool.BL.Provider.QuestionnaireData;
using AftaScool.BL.Entities.QuestionnaireData;
using SoftwareApproach.TestingExtensions;
using System.Diagnostics.CodeAnalysis;
using AftaScool.BL.Context;
using AftaScool.BL.Test;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using AftaScool.BL.Provider.SchoolData;
using AftaScool.BL.Provider.LearnerData;
using AftaScool.BL.Provider.BehaviourData;
using AftaScool.BL.Provider.AssessorData;

namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class QuestionnaireQuestionProviderTest : ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.QuestionaireQuestion")]
        public void saveQuestionnaireQuestion()
        {
            //SetUp

            var user = SeedData.CreateAdminUser(Context);
            IQuestionnaireQuestionProvider provider = new QuestionnaireQuestionProvider(Context, user);

            AssessorProvider assesso = new AssessorProvider(Context, user);
            ILearnerProvider learner = new LearnerProvider(Context, user);
            IQuestionnaireProvider que = new QuestionnaireProvider(Context, user);
            IBehaviourProvider behaviour = new BehaviourProvider(Context, user);
            IBehaviourQuestionProvider behaviourq = new BehaviourQuestionProvider(Context, user);
            var learn = learner.LearnerSave(null, user.Id, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
            var ass = assesso.SaveAssessor(null, user.Id, "Test UserName", "Test password", "Test email", "Test Title", "Test FirstName", "Test Surname", "9202280168083", Entities.SecurityData.GenderType.Female, "0124566512", "address line1", "address line 2", "Centurion", "0124");

            var beh = behaviour.saveBehaviour(null, "Rape");
            var questionnaire = que.saveQuestionnaire(null, ass.Id, learn.Id, DateTime.Now);
            var info = behaviourq.bquestion(null, beh.Id, -2, 4);

            //Act
            var question = provider.saveQuestion(null, questionnaire.Id, info.Id, "Luyanda assulted nokuthula");

            //Test
            question.Id.ShouldNotBeNull();
            question.Id.ShouldBeGreaterThan(0);

        }
        [TestMethod]
        [TestCategory("Provider.QuestionaireQuestion")]
        public void QuestionnaireQuestionListTest()
        {
            //SetUp

            var user = SeedData.CreateAdminUser(Context);
            IQuestionnaireQuestionProvider provider = new QuestionnaireQuestionProvider(Context, user);

            AssessorProvider assesso = new AssessorProvider(Context, user);
            ILearnerProvider learner = new LearnerProvider(Context, user);
            IQuestionnaireProvider que = new QuestionnaireProvider(Context, user);
            IBehaviourProvider behaviour = new BehaviourProvider(Context, user);
            IBehaviourQuestionProvider behaviourq = new BehaviourQuestionProvider(Context, user);
            var learn = learner.LearnerSave(null, user.Id, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
            var ass = assesso.SaveAssessor(null, user.Id, "Test UserName", "Test password", "Test email", "Test Title", "Test FirstName", "Test Surname", "9202280168083", Entities.SecurityData.GenderType.Female, "0124566512", "address line1", "address line 2", "Centurion", "0124");

            var beh = behaviour.saveBehaviour(null, "Rape");
            var questionnaire = que.saveQuestionnaire(null, ass.Id, learn.Id, DateTime.Now);
            var info = behaviourq.bquestion(null, beh.Id, -2, 4);

            //Act
            var question = provider.saveQuestion(null, questionnaire.Id, info.Id, "Luyanda assulted nokuthula");
            var question1 = provider.saveQuestion(null, questionnaire.Id, info.Id, "Luyanda assulted piet");

            //Test
            var x = provider.getQuestions().Count();

            x.ShouldEqual(2);


        }
    }
}