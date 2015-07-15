using AftaScool.BL.Entities.SchoolData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Entities.AssessorData
{
    [Table ("AssessorSchool")]
    public class AssessorSchool
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        public virtual long AssessorId { get; set; }
        [ForeignKey("AssessorId")]

        public virtual Assessor Assessor { get; set; }

        public virtual long SchoolId { get; set; }
        [ForeignKey("SchoolId")]

        public virtual School School { get; set; }

        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }

        


    }
}