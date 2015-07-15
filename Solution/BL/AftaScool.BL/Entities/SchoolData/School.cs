using AftaScool.BL.Entities.AssessorData;
using AftaScool.BL.Entities.LearnerData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Entities.SchoolData
{
     [Table("School")]
    public class School
    {
      [Key]
     [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
     public virtual long Id { get; set; }

     [Required]
     [MaxLength(100)]
     [Index("IDX_School", IsUnique = true, Order = 1)]

     public virtual string SchoolName { get; set; }
     public SchoolType Status { get; set; }

     [MaxLength(200)]
     public virtual string AddressLine1 { get; set; }

     [MaxLength(200)]
     public virtual string AddressLine2 { get; set; }

     [MaxLength(200)]
     public virtual string City { get; set; }

     [MaxLength(50)]
     public virtual string PostalCode { get; set; }

    //Added a new property which is the location of the school ISchoolProvider and the Provider will need to cater to this before the tests
     public virtual DbGeography Location { get; set; }


     public virtual ICollection<LearnerSchool> LearnerSchools { get; set; }
     public virtual ICollection<AssessorSchool> AssessorSchools { get; set; }


    }
}