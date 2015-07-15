using AftaScool.BL.Entities.SchoolData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Entities.LearnerData
{
    [Table ("LearnerSchool")]
    public class LearnerSchool
    {
    [Key]

     [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
     public virtual long Id { get; set; }

     public virtual long LearnerId { get; set; }
     [ForeignKey("LearnerId")]
     public virtual Learner Learners { get; set; }

     public virtual long SchoolId { get; set; }
     [ForeignKey("SchoolId")]

     public virtual School Schools { get; set; }

     public virtual DateTime StartDate { get; set; }
     public virtual DateTime EndDate { get; set; }




    }
}