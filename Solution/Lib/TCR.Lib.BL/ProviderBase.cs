using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCR.Lib.SysLog;

namespace TCR.Lib.BL
{
    public class ProviderBase<TPrivilegeTypeEnum> : IProviderBase<TPrivilegeTypeEnum>
    {
        public ProviderBase(IAuditDBContext<TPrivilegeTypeEnum> context)
            :this(context,null)
        {

        }

        public ProviderBase(IAuditDBContext<TPrivilegeTypeEnum> context, IUserContext<TPrivilegeTypeEnum> userContext)
        {
            AuditDBContext = context;
            LoggedInUser = userContext;
        }

        public IAuditDBContext<TPrivilegeTypeEnum> AuditDBContext { get; set; }

        public IUserContext<TPrivilegeTypeEnum> LoggedInUser { get; protected set; }

        public int DataContextSaveChanges()
        {
            try
            {
                if (LoggedInUser != null)
                    return AuditDBContext.SaveChanges(LoggedInUser);
                else
                    return AuditDBContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException error)
            {
                SendError(error);
                throw ;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException err)
            {
                SendError(err);
                throw;
            }
        }

        #region Logging

        public void SendInformation(string message)
        {
            AuditDBContext.AddSystemLogEntry(this, System.Guid.NewGuid(), LoggedInUser == null ? (long?)null : LoggedInUser.Id, LogEventType.Information, message);
            SyslogSender.SendInformation(this, message);
        }

        public void SendWarning(string warningMessage)
        {
            AuditDBContext.AddSystemLogEntry(this, System.Guid.NewGuid(), LoggedInUser == null ? (long?)null : LoggedInUser.Id, LogEventType.Warning, warningMessage);
            SyslogSender.SendWarning(this, warningMessage);
        }

        public void SendCriticalError(Exception exception)
        {
            AuditDBContext.AddSystemLogEntry(this, System.Guid.NewGuid(), LoggedInUser == null ? (long?)null : LoggedInUser.Id, LogEventType.CriticalException,
                        exception.Message,
                        exception.StackTrace,
                        exception.InnerException != null ? exception.InnerException.Message : string.Empty,
                        exception.InnerException != null ? exception.InnerException.StackTrace : string.Empty
                        );
            SyslogSender.SendCriticalError(this, exception);
        }

        public void SendError(Exception exception)
        {
            AuditDBContext.AddSystemLogEntry(this, System.Guid.NewGuid(), LoggedInUser == null ? (long?)null : LoggedInUser.Id, LogEventType.Error,
                       exception.Message,
                       exception.StackTrace,
                       exception.InnerException != null ? exception.InnerException.Message : string.Empty,
                       exception.InnerException != null ? exception.InnerException.StackTrace : string.Empty
                       );
            SyslogSender.SendError(this, exception);

        }

        #endregion



        public bool UserIsAllowed(TPrivilegeTypeEnum privelege)
        {
            if (LoggedInUser == null)
                return false;

            if (LoggedInUser.IsSystemAdmin)
                return true;

            if (LoggedInUser.AllowedPrivileges == null)
                return false;

            return LoggedInUser.AllowedPrivileges.Contains(privelege);
        }

        public void Authenticate(TPrivilegeTypeEnum privelege)
        {
            
            try
            {
                if (!UserIsAllowed(privelege))
                { throw new GenericSecurityException("Not Allowed!"); }
                return;
            }
            catch (GenericSecurityException)
            {
                
               
            }
            
              //  return;
        }

      
    }
}
