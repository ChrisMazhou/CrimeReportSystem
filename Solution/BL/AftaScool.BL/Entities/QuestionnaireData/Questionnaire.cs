using AftaScool.BL.Entities.AssessorData;
using AftaScool.BL.Entities.LearnerData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Entities.QuestionnaireData
{
    [Table("Questionnaire")]
    public class Questionnaire
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        public virtual long AssessorId { get; set; }
        [ForeignKey("AssessorId")]
        public virtual Assessor Assessors { get; set; }



        public virtual long LearnerId { get; set; }
        [ForeignKey("LearnerId")]
        public virtual Learner Learners { get; set; }

        public virtual DateTime QuestionnaireDate { get; set; }

        public virtual ICollection<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }

    }
}