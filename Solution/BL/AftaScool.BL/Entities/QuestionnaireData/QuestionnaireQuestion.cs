using AftaScool.BL.Entities.Behaviour;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Entities.QuestionnaireData
{
    [Table("QuestionnaireQuestion")]
    public class QuestionnaireQuestion
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        public virtual long QuestionnaireId { get; set; }
        [ForeignKey("QuestionnaireId")]
        public virtual Questionnaire Questionnaires { get; set; }

        public virtual long BehaviourQuestionId { get; set; }
        [ForeignKey("BehaviourQuestionId")]

        public virtual BehaviourQuestion BehaviourQuestions { get; set; }
        public virtual string Trait { get; set; }


       

    }
}