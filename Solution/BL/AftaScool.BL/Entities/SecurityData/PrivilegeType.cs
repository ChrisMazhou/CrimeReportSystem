using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Entities.SecurityData
{
    [Serializable]
    public enum PrivilegeType
    {
        LearnerMaintenance = 0,
        SchoolMaintenance = 1,
        BehaviourMaintenance = 2,
        AssessorSchoolMaintenance = 3,
        LearnerSchoolMaintenance = 4,
        QuestionnaireMaintenance = 5,
        QuestionnaireQuestionMaintenance = 6,
        AssessorMaintenance=7,
        //TimesheetApproval = 4,
       // ScheduleMeetings = 5,
        UserMaintenance = 8,
        RoleMaintenance = 9,
        //CaptureMinutes = 8,
        //ExpenseCodeMaintenance = 9,
       // ApproveExpense = 10,
       // AppraisalMaintenance = 11,
       // OfficeLocationMaintenance = 12,
        
    }
}
