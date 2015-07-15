using AftaScool.BL.Entities.AssessorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.AssessorData
{
    public interface IAssessorSchoolProvider:IAftaScoolProvider
    {

        AssessorSchool saveAssessorSchool(long? id, long assessorId, long schoolId, DateTime startDate, DateTime endDate);
       // void ArchiveAssessorSchoolProvider(long id);
        IQueryable<AssessorSchool> GetAssessorSchool();

    }
}
