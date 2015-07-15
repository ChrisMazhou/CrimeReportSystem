using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AftaScool.BL.Context;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Security;
using TCR.Lib.BL;

namespace AftaScool.BL.Provider
{
    public class AftaScoolProvider : ProviderBase<PrivilegeType>, IAftaScoolProvider
    {
        public AftaScoolProvider(DataContext context)
            : this(context, null)
        {

        }

        public AftaScoolProvider(DataContext context, ICurrentUser currentUser)
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