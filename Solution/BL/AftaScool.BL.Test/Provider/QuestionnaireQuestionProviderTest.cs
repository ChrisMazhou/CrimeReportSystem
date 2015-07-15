﻿using System;
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
        public void saveQuestionnaire()
        {
            var accountUser = SeedData.CreateAdminUser(Context);
            IBehaviourProvider behaviour = new BehaviourProvider(Context, accountUser);

            IQuestionnaireQuestionProvider provider = new QuestionnaireQuestionProvider(Context, accountUser);
            IQuestionnaireProvider que = new QuestionnaireProvider(Context, accountUser);
            AssessorProvider assesso = new AssessorProvider(Context, accountUser);
            ILearnerProvider learner = new LearnerProvider(Context, accountUser);
            var learn = learner.LearnerSave(null, "Kefilwe", "Mkhwanazi", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
            var ass = assesso.createAssessor(null, accountUser.Id);
            IQuestionnaireQuestionProvider que2 = new QuestionnaireQuestionProvider(Context, accountUser);
            var beh = behaviour.saveBehaviour(null, "Rape");
            var questionnaire = que.saveQuestionnaire(null, ass.Id, learn.Id, DateTime.Now);

            var question = que2.saveQuestion(null, questionnaire.Id, beh.Id, "Luyanda assulted nokuthula");

            
        }
        [TestMethod]
         [TestCategory("Provider.Question")]
         public void QuestionnaireListTest()
         {
             //Setup
             var accountUser = SeedData.CreateAdminUser(Context);
             IQuestionnaireQuestionProvider provider = new QuestionnaireQuestionProvider(Context, accountUser);
             IQuestionnaireProvider que = new QuestionnaireProvider(Context, accountUser);
             AssessorProvider assesso = new AssessorProvider(Context, accountUser);
             ILearnerProvider learner = new LearnerProvider(Context, accountUser);
             var learn = learner.LearnerSave(null, "Kefilwe", "Mkhwanazi", "grade", "9202280168083", Entities.SecurityData.GenderType.Female, "address line1", "address line 2", "Centurion", "0124", "0113450000");
             var ass = assesso.createAssessor(null, accountUser.Id);

             IBehaviourProvider behaviour = new BehaviourProvider(Context, accountUser);
             var beh = behaviour.saveBehaviour(null, "Rape");
             var questionnaire = que.saveQuestionnaire(null, ass.Id, learn.Id, DateTime.Now);

             var lea = provider.saveQuestion(null, questionnaire.Id, beh.Id, "Luyanda Raped Sifiso Mazibuko Eish am not Safe");
             var lea2 = provider.saveQuestion(null, questionnaire.Id, beh.Id, "Luyanda Raped Sifiso");
             //Act
             var x = provider.getQuestions().Count();

             

         }
    }
}