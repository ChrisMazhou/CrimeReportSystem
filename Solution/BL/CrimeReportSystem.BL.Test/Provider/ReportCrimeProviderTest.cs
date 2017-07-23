using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrimeReportSystem.BL.Provider.ReportCrimeData;
using SoftwareApproach.TestingExtensions;

namespace CrimeReportSystem.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ReportCrimeProviderTest:ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.ReportCrime")]
        public void SaveReport()
        {

            //SetUp
            var accountUser = SeedData.CreateAdminUser(Context);
            //var account = SeedData.CreateAdmin(Context);

            IReportCrimeProvider crimeReport = new ReportCrimeProvider(Context, accountUser);

            var crime = crimeReport.SaveReport(null, "", "", "","","","","");

            crime.ShouldNotBeNull();
            crime.Id.ShouldBeGreaterThan(0);

            
            
            
          

        }
    }
}
