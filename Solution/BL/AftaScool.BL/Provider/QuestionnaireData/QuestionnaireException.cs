using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.QuestionnaireData
{
    public class QuestionnaireException : Exception
    {

        public QuestionnaireException(string errorMessage)
            : base(errorMessage)
        {

        }

    }
}