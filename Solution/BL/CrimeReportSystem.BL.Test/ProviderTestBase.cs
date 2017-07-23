using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CrimeReportSystem.BL.Context;
using CrimeReportSystem.BL.Settings;
using CrimeReportSystem.BL.Test.DataConnections;
using CrimeReportSystem.BL.Test.Mocs;

namespace CrimeReportSystem.BL.Test
{
    public abstract class ProviderTestBase
    {
        private ITestDataConnection _TestDataConnection;
        private IAppSettings AppSettings { get; set; }

        protected DataContext Context
        {
            get
            {
                return _TestDataConnection.Context;
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _TestDataConnection = new SQLDataConnection(TearDownDB);
            InitTest();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _TestDataConnection.Dispose();
        }

        public virtual bool TearDownDB { get { return true; } }

        protected virtual void InitTest()
        {
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public string ReportFolder
        {
            get
            {
                string x = AssemblyDirectory;
                if (!x.EndsWith("\\"))
                    x = x + "\\";
                return x + @"Report\";
            }
        }

        public IAppSettings FakeSettings
        {
            get
            {
                return new FakeAppSettings();
            }
        }
    }
}
