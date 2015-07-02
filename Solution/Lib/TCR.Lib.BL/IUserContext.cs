using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCR.Lib.BL
{
    public interface IUserContext<TPrivilegeTypeEnum>
    {
        #region User Details

        long Id { get; }

        string UserName { get; }

        string DisplayName { get; }

        List<TPrivilegeTypeEnum> AllowedPrivileges { get; }

        bool IsSystemAdmin { get; }

        #endregion
    }
}
