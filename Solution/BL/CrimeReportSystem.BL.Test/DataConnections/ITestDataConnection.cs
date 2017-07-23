using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrimeReportSystem.BL.Context;

namespace CrimeReportSystem.BL.Test.DataConnections
{
    public interface ITestDataConnection : IDisposable
    {
        DataContext Context { get; set; }
    }
}
