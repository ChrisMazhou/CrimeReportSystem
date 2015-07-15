using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.AssessorData
{
    public class AssessorSchoolException : Exception
    {
        public AssessorSchoolException(string errorMessage) :base(errorMessage){ }
    }
}