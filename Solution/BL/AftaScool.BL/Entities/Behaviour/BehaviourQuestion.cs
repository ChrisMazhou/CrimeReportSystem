using AftaScool.BL.Entities.QuestionnaireData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Entities.Behaviour
{
    [Table ("BehaviourQuestion")]
    public class BehaviourQuestion
    {
        
          [Key]
     [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
     public virtual long Id { get; set; }

     public virtual long BehaviourId { get; set; }
     [ForeignKey("BehaviourId")]

     public virtual Behaviour Behaviour { get; set; }

     public virtual double MinimumWeighting { get; set;}

     public virtual double MaximumWeighting { get; set; }

     public virtual ICollection<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }

    

      
    }
}