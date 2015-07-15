using AftaScool.BL.Entities.AssessorData;
using AftaScool.BL.Entities.LearnerData;
using AftaScool.BL.Entities.SecurityData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCR.Lib.BL;

namespace AftaScool.BL.Entities.SecurityData
{
    [Table("UserIdentity")]
    public class UserIdentity:
        AuditedEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Index("IDX_UserName", IsUnique = true,Order=1)]
        [MaxLength(50)]
        [Required]
        public virtual string UserName { get; set; }

        public bool Active { get; set; }

        [MaxLength(50)]
        [Required]
        public virtual string Title { get; set; }

        [MaxLength(200)]
        [Required]
        public virtual string FirstName { get; set; }

        [MaxLength(200)]
        [Required]
        public virtual string Surname { get; set; }

        [MaxLength(200)]
        [Required]
        public virtual string IdPassportNum { get; set; }

        public virtual GenderType Gender { get; set; }

        [MaxLength(200)]
        [Required]
        public virtual string PasswordHash { get; set; }

        [MaxLength(20)]
        [Required]
        public virtual string Telephone { get; set; }

        [MaxLength(200)]
        [Required]
        public virtual string EmailAddress { get; set; }

        [MaxLength(200)]
        public virtual string AddressLine1 { get; set; }

        [MaxLength(200)]
        public virtual string AddressLine2 { get; set; }

        [MaxLength(200)]
        public virtual string City { get; set; }

        [MaxLength(50)]
        public virtual string PostalCode { get; set; }

        public int FailedLoginAttempts { get; set; }
        public DateTime? LockedOut { get; set; }
        public DateTime? Deactivated { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsSystemAdmin { get; set; }

        [NotMapped]
        public List<PrivilegeType> AllowedPrivileges { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
     
        public virtual ICollection<Assessor> Assessors { get; set; }
        
        

    }
}
