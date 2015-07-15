using AftaScool.BL.Entities.AssessorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.AssessorData
{
    public interface IAssessorProvider
    {

        Assessor createAssessor(long? id, long userId);

        IQueryable<Assessor> GetAssessor();

    }
}

