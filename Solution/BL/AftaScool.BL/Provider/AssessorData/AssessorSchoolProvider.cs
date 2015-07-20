using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AftaScool.BL.Context;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Security;
using AftaScool.BL.Entities.AssessorData;
using AftaScool.BL.Provider.LearnerData;

namespace AftaScool.BL.Provider.AssessorData
{
    public class AssessorSchoolProvider : AftaScoolProvider,IAssessorSchoolProvider
    {
        #region asp

        public AssessorSchoolProvider(DataContext context, ICurrentUser user): base(context, user)
        { }
        #endregion

        public AssessorSchool saveAssessorSchool(long? id, long assessorId, long schoolId, DateTime startDate, DateTime endDate)
        {
            Authenticate(PrivilegeType.AssessorSchoolMaintenance);

            AssessorSchool AssessorSchoolSave = null;

            AssessorSchoolSave = DataContext.AssessorSchoolSet.Where(a => a.AssessorId == assessorId && a.SchoolId == schoolId  && a.StartDate== startDate).SingleOrDefault();
          

            if (startDate == null )
                throw new AssessorSchoolException("Select the start date");

            if( AssessorSchoolSave!=null)
                throw new AssessorSchoolException("Assessor Exist with the same date");

            if (id != null && id > 0)
                AssessorSchoolSave = DataContext.AssessorSchoolSet.Where(a => a.Id == id).SingleOrDefault();
            else
            {
                AssessorSchoolSave = new AssessorSchool();
                DataContext.AssessorSchoolSet.Add(AssessorSchoolSave);
            }

            //attribute
            AssessorSchoolSave.AssessorId = assessorId;
            AssessorSchoolSave.SchoolId = schoolId;
            AssessorSchoolSave.StartDate = startDate;
            AssessorSchoolSave.EndDate = endDate;

            DataContextSaveChanges();

            return AssessorSchoolSave;

        }

        public IQueryable<AssessorSchool> GetAssessorSchool()
        {
            var q = from h in DataContext.AssessorSchoolSet
                    orderby h.StartDate
                    select h;

            return q;
        }


       
    }
    
}