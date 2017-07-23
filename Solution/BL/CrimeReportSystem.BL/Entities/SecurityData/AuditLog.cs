using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCR.Lib.BL;

namespace CrimeReportSystem.BL.Entities.SecurityData
{
    [Table("AuditLog")]
    public class AuditLog : IAuditLog
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public long? UserId { get; set; }

        [Index("IDX_AuditLog", Order = 0)]
        [Index("IDX_AuditUser")]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Index("IDX_AuditLog", Order = 1)]
        [Index("IDX_AuditDate")]
        public DateTime EventDate { get; set; }

        [Index("IDX_AuditLog", Order = 2)]
        [Index("IDX_EventType")]
        public AuditEventType EventType { get; set; }

        [MaxLength(200)]
        [Required]
        [Index("IDX_AuditLog", Order = 3)]
        [Index("IDX_AuditTable")]
        public string TableName { get; set; }

        public long RecordId { get; set; }

        [MaxLength(200)]
        [Required]
        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }

        public string NewValue { get; set; }
    }
}
