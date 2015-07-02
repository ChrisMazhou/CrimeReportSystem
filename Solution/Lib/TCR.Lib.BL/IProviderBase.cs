using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCR.Lib.BL
{
    public interface IProviderBase<TPrivilegeTypeEnum>
    {
        IAuditDBContext<TPrivilegeTypeEnum> AuditDBContext { get; set; }
        IUserContext<TPrivilegeTypeEnum> LoggedInUser { get; }
        bool UserIsAllowed(TPrivilegeTypeEnum privelege);

        int DataContextSaveChanges();

        void SendInformation(string message);
        void SendWarning(string infoMessage);
        void SendCriticalError(Exception exception);
        void SendError(Exception exception);
    }
}
