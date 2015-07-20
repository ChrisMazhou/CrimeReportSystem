using AftaScool.BL.Context;
using AftaScool.BL.Entities.QuestionnaireData;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.QuestionnaireData
{
    public class QuestionnaireQuestionProvider : AftaScoolProvider, IQuestionnaireQuestionProvider
    {
        #region Ctor

        public QuestionnaireQuestionProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        { }
        #endregion

        public QuestionnaireQuestion saveQuestion(long? id, long questionnaireId, long behaviourId, string trait)
        {

          Authenticate(PrivilegeType.QuestionnaireQuestionMaintenance);

            QuestionnaireQuestion saveResults = null;

            ///saveResults = DataContext.QuestionnaireQuestionSet.Where(a => a.QuestionnaireId == questionnaireId && a.BehaviourQuestionId == behaviourId).SingleOrDefault();

           
            if(trait== null )
                throw new QuestionnaireQuestionException("Trait cannot be empty");


            if (id != null && id > 0)
                saveResults = DataContext.QuestionnaireQuestionSet.Where(a => a.Id == id).SingleOrDefault();
            else  //updating Trait
            {

                saveResults = new QuestionnaireQuestion();
                DataContext.QuestionnaireQuestionSet.Add(saveResults);

            }
            saveResults.QuestionnaireId = questionnaireId;
            saveResults.BehaviourQuestionId = behaviourId;
            saveResults.Trait = trait;

            DataContextSaveChanges();

            return saveResults;


        }

        public IQueryable<QuestionnaireQuestion> getQuestions()
        {

            var q = from h in DataContext.QuestionnaireQuestionSet
                    orderby h.Id
                    select h;

            return q;


        }

    }
}