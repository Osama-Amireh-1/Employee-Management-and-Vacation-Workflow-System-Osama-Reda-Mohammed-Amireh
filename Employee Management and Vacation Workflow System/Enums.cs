using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_and_Vacation_Workflow_System
{
   public enum enEnterToSystem
    {
        NewEmployee=1,
        Login,
        Exit
    }
    public enum enMainMenuOption
    {
        ShowAllEmployees=1,
        SubmitNewVacationRequest,
        ShowAllEmployeeHavePendingRequest,
        ShowMyApprovedVacationHistory,
        EditMyReportedEmployeeInfo,
        Logout
    }
    public enum enShowAllEmployeeHavePendingRequestOperation
    {
        TakeActionForEmployee=1,
        BackToMainMenu

    }
    public enum enTakeActionOption
    {
        Approved=1,
        Decline,
        BackToMainMenu
    }
    public enum enEditEmployeeInfo
    {
        EditName=1,
        EditDepartment,
        EditPosition,
        EditSalary,
        BackToMainMenu
    }
}
