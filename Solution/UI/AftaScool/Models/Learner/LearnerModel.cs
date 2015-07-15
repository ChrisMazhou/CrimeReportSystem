using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AftaScool.Models.Learner
{
    public class LearnerModel
    {
        public long? Id { get; set; }

        [Required]
        public string LearnerName { get; set; }
        [Required]
        public string LearnerSurname { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string TelephoneNumber { get; set; }

  
    }
}