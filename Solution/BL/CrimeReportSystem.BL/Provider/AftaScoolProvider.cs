using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrimeReportSystem.BL.Context;
using CrimeReportSystem.BL.Entities.SecurityData;
using CrimeReportSystem.BL.Provider.Security;
using TCR.Lib.BL;

namespace CrimeReportSystem.BL.Provider
{
    public class CrimeReportSystemProvider : ProviderBase<PrivilegeType>, ICrimeReportSystemProvider
    {
        public CrimeReportSystemProvider(DataContext context)
            : this(context, null)
        {

        }

        public CrimeReportSystemProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        {

        }

        public DataContext DataContext
        {
            get { return this.AuditDBContext as DataContext; }
        }

        public ICurrentUser CurrentUser
        {
            get
            {
                return LoggedInUser as ICurrentUser;
            }
            set
            {
                LoggedInUser = value as IUserContext<PrivilegeType>;
            }
        }
    }
}