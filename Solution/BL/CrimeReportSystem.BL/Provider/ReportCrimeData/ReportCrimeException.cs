using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrimeReportSystem.BL.Provider.ReportCrimeData
{
    public class ReportCrimeException:Exception
    {

        public ReportCrimeException(string errorMessage):base(errorMessage)
        {

        }


    }
}