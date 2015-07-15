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
    public class QuestionnaireProvider : AftaScoolProvider, IQuestionnaireProvider
    {

        #region Ctor

        public QuestionnaireProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        { }
        #endregion


        #region create questionnaire date
        public Questionnaire saveQuestionnaire(long? id, long assessorId, long learnerId, DateTime questionnaireDate)
        {
            Authenticate(PrivilegeType.QuestionnaireMaintenance);



            Questionnaire results = new Questionnaire();



            results = DataContext.QuestionnaireSet.Where(a => a.LearnerId == learnerId && a.QuestionnaireDate == questionnaireDate && a.Id != id).SingleOrDefault();

            if (results != null && results.AssessorId == assessorId)
                throw new QuestionnaireException("Questionnaire date: " + questionnaireDate.ToString("yyyy-MM-dd") + "has been created for this Learner");

            /*  if (questionnaireDate.DayOfWeek != DayOfWeek.Monday)
                  throw new QuestionnaireException("The week must start on a Monday!");*/
            if (id != null && id > 0)
                results = DataContext.QuestionnaireSet.Where(a => a.Id == id).SingleOrDefault();
            else
            {
                results = new Questionnaire();
                DataContext.QuestionnaireSet.Add(results);
            }


            //set attributes
            results.AssessorId = assessorId;
            results.LearnerId = learnerId;
            results.QuestionnaireDate = questionnaireDate;

            DataContextSaveChanges();

            return results;


        }

        #endregion

        public IQueryable<Questionnaire> GetQuestionnaires()
        {
            var q = from h in DataContext.QuestionnaireSet
                    orderby h.QuestionnaireDate
                    select h;

            return q;
        }

    }



}