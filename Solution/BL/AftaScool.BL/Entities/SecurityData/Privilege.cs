using AftaScool.BL.Entities.SecurityData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Entities.SecurityData
{
    [Table("Privilege")]
    public class Privilege
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Index("IDX_PrivilegeSecurity", IsUnique = true)]
        public virtual PrivilegeType Security { get; set; }

        [Required]
        [MaxLength(200)]
        public virtual string Description { get; set; }


        public virtual ICollection<Role> Roles { get; set; }

    }
}
