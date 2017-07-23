using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrimeReportSystem.Models.ReportCrime
{
    public class ReportCrimeModel
    {

        public virtual long? Id { get; set; }

        public virtual string TypeOfCrime { get; set; }

        public virtual string Location { get; set; }

        public virtual string Date { get; set; }

        public virtual string Time { get; set; }

        public virtual string Name { get; set; }
       
        public virtual string ContactNo { get; set; }
        
        public virtual string Status { get; set; }

       

        

        

        
    }
}