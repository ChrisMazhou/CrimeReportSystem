using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.QuestionnaireData
{
    public class QuestionnaireQuestionException : Exception
    {


        public QuestionnaireQuestionException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}