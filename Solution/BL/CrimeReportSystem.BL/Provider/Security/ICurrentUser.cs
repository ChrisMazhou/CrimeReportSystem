using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCR.Lib.BL;
using CrimeReportSystem.BL.Entities.SecurityData;

namespace CrimeReportSystem.BL.Provider.Security
{
    public interface ICurrentUser : IUserContext<PrivilegeType>
    {
    }
}
