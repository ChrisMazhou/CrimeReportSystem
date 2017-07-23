using CrimeReportSystem.BL.Entities.SecurityData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportSystem.BL.Provider.Email
{
    public interface IEmailProvider
    {
        void SendPasswordResetEmail(UserIdentity user, string newPassword);
    }
}
