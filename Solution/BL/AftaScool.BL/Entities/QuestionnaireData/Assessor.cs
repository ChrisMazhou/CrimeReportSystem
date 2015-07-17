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
    [Table("Learner")]
    public class Learner
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index("IDX_Learner", IsUnique = true, Order = 1)]

        public virtual string FirstName { get; set; }

        public virtual string Surname { get; set; }
        public StatusType Status { get; set; }

        public virtual int Grade { get; set; }

        public virtual GenderType Gender { get; set; }

        [MaxLength(200)]
        [Required]
        public virtual string IdPassportNum { get; set; }

        [MaxLength(20)]
        [Required]
        public virtual string Telephone { get; set; }

        [Required]
        [MaxLength(200)]
        public virtual string AddressLine1 { get; set; }

        [MaxLength(200)]
        public virtual string AddressLine2 { get; set; }

        [MaxLength(200)]
        public virtual string City { get; set; }

        [MaxLength(50)]
        public virtual string PostalCode { get; set; }
    }
}