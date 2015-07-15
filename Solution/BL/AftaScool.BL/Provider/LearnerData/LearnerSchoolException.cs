
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.LearnerData
{
    public class LearnerSchoolException : Exception
    {
        public LearnerSchoolException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}
