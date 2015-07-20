using AftaScool.BL.Context;
using AftaScool.BL.Entities.Behaviour;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.BehaviourData
{
    public class BehaviourQuestionProvider:AftaScoolProvider,IBehaviourQuestionProvider
    {


        #region behaviour

        public BehaviourQuestionProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        { }
     
        #endregion


        public BehaviourQuestion bquestion(long? id, long behaviourId, double minimumWeighting, double maximumWeighting)
        {

            Authenticate(PrivilegeType.BehaviourQuestionMaintenance);


            BehaviourQuestion questionResults = new BehaviourQuestion();



            if(minimumWeighting  > 0  || minimumWeighting <= -5)
            {

                throw new BehaviourQuestionException("the value must be between 0 and -5");

            }
            if(maximumWeighting < 0 || maximumWeighting > 5)
            {


                throw new BehaviourQuestionException("the value must be between 0 and 5");

            }

           if (id != null && id > 0) 
            {

                questionResults = DataContext.BehaviourQuestionSet.Where(a => a.Id == id).SingleOrDefault();

            }
            else
            {

                questionResults = new BehaviourQuestion();
                DataContext.BehaviourQuestionSet.Add(questionResults);


            }



           questionResults.BehaviourId = behaviourId;
           questionResults.MinimumWeighting = minimumWeighting;
           questionResults.MaximumWeighting = maximumWeighting;

            DataContextSaveChanges();

            return questionResults;


        }
        public IQueryable<BehaviourQuestion> GetBehaviours()
        {
            var q = from h in DataContext.BehaviourQuestionSet
                    orderby h.Id
                    select h;

            return q;
        }

        public BehaviourQuestion ArchiveBehaviour(long id)
        {
            Authenticate(PrivilegeType.BehaviourQuestionMaintenance);
            var info = DataContext.BehaviourQuestionSet.Where(a => a.Id == id).SingleOrDefault();
            DataContextSaveChanges();

            return info;

         }

    }
}