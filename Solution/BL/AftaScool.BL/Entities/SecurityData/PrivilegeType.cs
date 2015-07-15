using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterScool.BL.Entities.SecurityData
{
    [Serializable]
    public enum PrivilegeType
    {
        ClientMaintenance = 0,
        LeaveCodeMaintenance = 1,
        ApproveLeave = 2,
        ProjectMaintenance = 3,
        TimesheetApproval = 4,
        ScheduleMeetings = 5,
        UserMaintenance = 6,
        RoleMaintenance = 7,
        CaptureMinutes = 8,
        ExpenseCodeMaintenance = 9,
        ApproveExpense = 10,
        AppraisalMaintenance =11,
        OfficeLocationMaintenance =12
    }
}
