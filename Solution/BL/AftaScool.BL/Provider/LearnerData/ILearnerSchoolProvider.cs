using AftaScool.BL.Entities.LearnerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.LearnerData
{
    public interface ILearnerSchoolProvider : IAftaScoolProvider
    {
        LearnerSchool saveLearnerSchool(long? id, long learnerId, long schoolId, DateTime startDate, DateTime endDate);

     
        IQueryable<LearnerSchool> GetLearnerSchools();
    }
}
