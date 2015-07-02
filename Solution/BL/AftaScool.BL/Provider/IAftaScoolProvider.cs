using AftaScool.BL.Context;
using AftaScool.BL.Provider.Security;
using AfterScool.BL.Entities.SecurityData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCR.Lib.BL;

namespace AftaScool.BL.Provider
{
    public interface IAftaScoolProvider : IProviderBase<PrivilegeType>
    {
        DataContext DataContext { get; }
        ICurrentUser CurrentUser { get; set; }
    }
}
