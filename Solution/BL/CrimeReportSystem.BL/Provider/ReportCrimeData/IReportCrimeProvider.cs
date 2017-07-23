using CrimeReportSystem.BL.Entities.ReportData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportSystem.BL.Provider.ReportCrimeData
{
    public interface IReportCrimeProvider:ICrimeReportSystemProvider
    {

        ReportCrime SaveReport(long? id, string typeOfCrime, string location, string date, string time,string name,string contactNo,string status);

        IQueryable<ReportCrime> getCrimes();

        ReportCrime ArchiveReport(long id);

    }
}
