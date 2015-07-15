using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.LearnerData
{
    public  class  LearnerException : Exception
    {
        public LearnerException(string errorMessage)
            : base(errorMessage)
        {
            
        }


    }
}
