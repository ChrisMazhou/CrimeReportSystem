using CrimeReportSystem.BL.Context;
using CrimeReportSystem.BL.Provider.Security;
using CrimeReportSystem.BL.Entities.SecurityData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCR.Lib.BL;

namespace CrimeReportSystem.BL.Provider
{
    public interface ICrimeReportSystemProvider : IProviderBase<PrivilegeType>
    {
        DataContext DataContext { get; }
        ICurrentUser CurrentUser { get; set; }
    }
}
