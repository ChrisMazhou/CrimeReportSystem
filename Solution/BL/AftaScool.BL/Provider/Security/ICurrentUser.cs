using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCR.Lib.BL;
using AftaScool.BL.Entities.SecurityData;
using AfterScool.BL.Entities.SecurityData;

namespace AftaScool.BL.Provider.Security
{
    public interface ICurrentUser : IUserContext<PrivilegeType>
    {
    }
}
