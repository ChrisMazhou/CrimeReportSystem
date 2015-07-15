using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Entities.Behaviour
{
    [Table ("Behaviour")]
    public class Behaviour
    {
         
          [Key]
     [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
           public virtual long Id { get; set; }


    
          public virtual string Type { get; set; }
        

          public virtual ICollection<BehaviourQuestion> BehaviourQuestions { get; set; }






    }
}