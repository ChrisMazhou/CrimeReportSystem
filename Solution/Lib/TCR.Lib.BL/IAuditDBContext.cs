using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCR.Lib.BL
{
    public interface IAuditDBContext<TPrivilegeTypeEnum>
    {
        int SaveChanges(IUserContext<TPrivilegeTypeEnum> currentUser);
        int SaveChanges();
        
        void AddSystemLogEntry(Object sender,Guid guid, long? currentUserId, LogEventType logEventType, 
                               string message, string stackTrace = null, string innerExceptionMessage = null, string innerExceptionStackTrace = null);

    }
}
