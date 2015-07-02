 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftaScool.BL.Types;

namespace AftaScool.BL.Entities.SecurityData
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }
        
        public virtual ICollection<Privilege> Privileges { get; set; }
        public virtual ICollection<UserIdentity> UserIdentities { get; set; }
        
        [Required]
        [MaxLength(100)]
        public virtual string RoleName { get; set; }

        public virtual string Description { get; set; }

        public virtual StatusType Status { get; set; }
    }
}
