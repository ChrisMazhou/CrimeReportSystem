using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftaScool.BL.Context;

namespace AftaScool.BL.Test.DataConnections
{
    public interface ITestDataConnection : IDisposable
    {
        DataContext Context { get; set; }
    }
}
