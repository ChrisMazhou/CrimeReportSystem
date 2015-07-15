using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AftaScool.BL.Context;
using AftaScool.BL.Entities.LearnerData;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Security;
using AftaScool.BL.Provider.LearnerData;


namespace AftaScool.BL.Provider.LearnerData
{
    public class LearnerSchoolProvider : AftaScoolProvider, ILearnerSchoolProvider
    {
        #region LSchool
        public LearnerSchoolProvider(DataContext context, ICurrentUser user)
            : base(context, user)
        { }
        #endregion


        public LearnerSchool saveLearnerSchool(long? id, long learnerId, long schoolId, DateTime startDate, DateTime endDate)
        {
            Authenticate(PrivilegeType.LearnerSchoolMaintenance);

            LearnerSchool results = null;
            endDate = startDate.AddDays(6);



            results = DataContext.LearnerSchoolSet.Where(a => a.SchoolId == schoolId && a.LearnerId == learnerId).SingleOrDefault();

            //if (results.StartDate == startDate && results != null)
            //    throw new LearnerSchoolException("Date " + startDate.ToString("yyyy-mm-dd") + " has been created for this learner ");

            if (startDate == null)
                throw new LearnerSchoolException("Select the start date");


            if (id != null && id > 0)
                results = DataContext.LearnerSchoolSet.Where(a => a.Id == id).SingleOrDefault();
            else
            {
                results = new LearnerSchool();
                DataContext.LearnerSchoolSet.Add(results);
            }

            results.SchoolId = schoolId;
            results.LearnerId = learnerId;
            results.StartDate = startDate;
            results.EndDate = endDate;



            DataContextSaveChanges();


            return results;
        }
        public IQueryable<LearnerSchool> GetLearnerSchools()
        {
            var q = from h in DataContext.LearnerSchoolSet
                    orderby h.Id
                    select h;

            return q;
        }

    }
}