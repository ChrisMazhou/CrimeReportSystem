using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftaScool.BL.Types;

namespace AftaScool.Models.Project
{
    public class ProjectModel
    {
        public long? Id { get; set; }

        public long ClientId { get; set; }

        [MaxLength(100)]
        [Required]
        public string ProjectName { get; set; }

        public StatusType Status { get; set; }

        [Required]
        [MaxLength(10)]
        public string CostCode { get; set; }

        public string Description { get; set; }
    }
}
