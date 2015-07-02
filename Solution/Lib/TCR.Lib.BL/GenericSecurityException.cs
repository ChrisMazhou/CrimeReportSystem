using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCR.Lib.BL
{
    public class GenericSecurityException :
     Exception
    {
        public GenericSecurityException(string message) :
            base(message)
        {

        }
    }
}
