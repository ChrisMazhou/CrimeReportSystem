using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportSystem.BL.Entities.SecurityData
{
    [Serializable]
    public enum PrivilegeType
    {
        ReportCrimeMaintenance = 0,
       
       
        //TimesheetApproval = 4,
       // ScheduleMeetings = 5,
        UserMaintenance = 1,
        RoleMaintenance = 2,
        //CaptureMinutes = 8,
        //ExpenseCodeMaintenance = 9,
       // ApproveExpense = 10,
       // AppraisalMaintenance = 11,
       // OfficeLocationMaintenance = 12,
        
    }
}
