using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.BehaviourData
{
    public class BehaviourException:Exception
    {

         public BehaviourException(string errorMessage)
            : base(errorMessage)
        {

        }


    }
}