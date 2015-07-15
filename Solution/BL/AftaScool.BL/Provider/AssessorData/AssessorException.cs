using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.AssessorData
{
    public class AssessorException : Exception
    {

        public AssessorException(string errorMessage)
            : base(errorMessage)
        {


        }

    }
}