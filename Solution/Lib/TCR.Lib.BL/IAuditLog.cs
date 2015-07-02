using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCR.Lib.BL
{
    public interface IAuditLog
    {
        Guid Id { get; set; }

        string UserName { get; set; }

        long? UserId { get; set; }

        DateTime EventDate { get; set; }

        AuditEventType EventType { get; set; }

        string TableName { get; set; }

        long RecordId { get; set; }

        string ColumnName { get; set; }

        string NewValue { get; set; }

        string OriginalValue { get; set; }
    }
}
