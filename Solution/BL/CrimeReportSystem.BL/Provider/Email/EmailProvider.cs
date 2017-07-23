using CrimeReportSystem.BL.Context;
using CrimeReportSystem.BL.Provider.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrimeReportSystem.BL.Provider.Email
{
    public class EmailProvider:CrimeReportSystemProvider, IEmailProvider
    {
        public EmailProvider(DataContext context, ICurrentUser user)
            : base(context, user)
        {

        }

        public EmailProvider(DataContext context)
            : base(context)
        {

        }

        public void SendPasswordResetEmail(Entities.SecurityData.UserIdentity user, string newPassword)
        {
        }
    }
}