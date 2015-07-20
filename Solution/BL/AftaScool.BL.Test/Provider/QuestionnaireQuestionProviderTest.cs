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
        [TestCategory("Provider.Question")]
        public void saveQuestionnaireQuestion()
        {
           //SetUp
            var accountUser = SeedData.CreateAdminUser(Context);
            IBehaviourProvider behaviour = new BehaviourProvider(Context, accountUser);
            IBehaviourQuestionProvider behaviourq = new BehaviourQuestionProvider(Context, accountUser);
            IQuestionnaireQuestionProvider provider = new QuestionnaireQuestionProvider(Context, accountUser);
            IQuestionnaireProvider que = new QuestionnaireProvider(Context, accountUser);
            AssessorProvider assesso = new AssessorProvider(Context, accountUser);
            ILearnerProvider learner = new LearnerProvider(Context, accountUser);
            var learn = learner.LearnerSave(null, accountUser.Id, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
            var ass = assesso.SaveAssessor(null, accountUser.Id,"Test UserName","Test password","Test email","Test Title","Test FirstName","Test Surname","9202280168083",Entities.SecurityData.GenderType.Female,"0124566512","address line1", "address line 2","Centurion", "0124");
            var beh = behaviour.saveBehaviour(null, "Rape");
            var questionnaire = que.saveQuestionnaire(null, ass.Id, learn.Id, DateTime.Now);
            var info = behaviourq.bquestion(null, beh.Id, -2, 4);

            //Act
            var question = provider.saveQuestion(null, questionnaire.Id, info.Id, "Luyanda assulted nokuthula");

            //Test
            question.Id.ShouldNotBeNull();
            question.ShouldNotBeNull();
        }
        [TestMethod]
         [TestCategory("Provider.Question")]
         public void QuestionnaireQuestionListTest()
         {
             //Setup
             var accountUser = SeedData.CreateAdminUser(Context);
             IQuestionnaireQuestionProvider provider = new QuestionnaireQuestionProvider(Context, accountUser);
             IQuestionnaireProvider que = new QuestionnaireProvider(Context, accountUser);
             AssessorProvider assesso = new AssessorProvider(Context, accountUser);
             ILearnerProvider learner = new LearnerProvider(Context, accountUser);
             var learn = learner.LearnerSave(null, accountUser.Id, "Test LearnerName", "Test LearnerSurname", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
             var ass = assesso.SaveAssessor(null, accountUser.Id, "Test UserName", "Test password", "Test email", "Test Title", "Test FirstName", "Test Surname", "9202280168083", Entities.SecurityData.GenderType.Female, "0124566512", "address line1", "address line 2", "Centurion", "0124");

             IBehaviourProvider behaviour = new BehaviourProvider(Context, accountUser);
             var beh = behaviour.saveBehaviour(null, "Rape");
             var beh1 = behaviour.saveBehaviour(null, "Stealling");
             var questionnaire = que.saveQuestionnaire(null, ass.Id, learn.Id, DateTime.Now);
           //  var questionnaire1 = que.saveQuestionnaire(null, ass.Id, learn.Id, DateTime.Today.AddDays(3));

             var lea = provider.saveQuestion(null, questionnaire.Id, beh.Id, "Luyanda Raped Sifiso Mazibuko Eish am not Safe");
            
             //Act
             var x = provider.getQuestions().Count();

             //Test
            
             //x.ShouldEqual(1);
             

         }
    }
}