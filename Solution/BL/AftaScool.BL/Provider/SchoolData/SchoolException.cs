using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.SchoolData
{
    public class SchoolException:Exception
    {
        public SchoolException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}