using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CrimeReportSystem.BL.Entities.ReportData
{

    [Table("ReportCrime")]
    public class ReportCrime
    {
        //auto Generate
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public virtual long Id { get; set; }

     
        public virtual string TypeOfCrime { get; set; }
        public virtual string Location { get; set; }
       
        public virtual string Date { get; set; }
        public virtual string Time { get; set; }

        public virtual string Name { get; set; }
        public virtual string ContactNo { get; set; }
        public virtual string Status { get; set; }
        



    }
}