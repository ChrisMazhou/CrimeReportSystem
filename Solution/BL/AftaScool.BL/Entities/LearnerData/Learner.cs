using AftaScool.BL.Entities.AssessorData;
using AftaScool.BL.Entities.QuestionnaireData;
using AftaScool.BL.Entities.SchoolData;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Entities.LearnerData
{
    //Same as the assessor, the learner will be contained in the UserIdentity Table
 [Table ("Learner")]
    public class Learner
    {
     [Key]
     [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
     public virtual long Id { get; set; }

     public virtual long UserIdentityId { get; set; }
     [ForeignKey("UserIdentityId")]
     public virtual UserIdentity UserIdentities { get; set; }
  
     public virtual ICollection<Questionnaire> Questionnaires { get; set; }
     public virtual ICollection<LearnerSchool> LearnerSchools { get; set; }

     public virtual ICollection<Assessor> Assessors { get; set; }
    }
}