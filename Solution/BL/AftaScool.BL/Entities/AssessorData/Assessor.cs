using AftaScool.BL.Entities.LearnerData;
using AftaScool.BL.Entities.QuestionnaireData;
using AftaScool.BL.Entities.SecurityData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Entities.AssessorData
{
    [Table ("Assessor")]
    public class Assessor
    {
        [Key]
     [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
     public virtual long Id { get; set; }

      public virtual long UserIdentityId { get; set; }
     [ForeignKey("UserIdentityId")]
     
     public virtual UserIdentity UserIdentities { get; set; }

     
     public virtual ICollection<Learner> Learners { get; set; }

     public virtual ICollection<AssessorSchool> AssessorSchools { get; set; }
     public virtual ICollection<Questionnaire> Questionnaires { get; set; }

    }
}