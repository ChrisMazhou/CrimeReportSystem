using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.Security
{
    public class SecurityException: Exception
    {
        public SecurityException(string errorMessage)
            :base(errorMessage)
        {

        }
    }
}
