using Data_Layer.Entities;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.EmployeeServiceFiles.Interface
{
    public interface IEmployeeService
    {
        bool AddNewEmployee(string employeeeNumber, string employeeeName, int departemntId, int positionId, char genderCode, string reportedToEmployeeNumber, decimal salary);
        bool IsEmployeeExist(string EmployeeNumber);
        List<EmployeeBasicInfoDto> GetAllEmployees();
        EmployeeDetailsDto GetEmployeeByEmployeeNumber(string EmployeeNumber);
        bool UpdateEmployeeDepartment(string EmployeeNumber, int DepartmentId);
        bool UpdateEmployeePosition(string EmployeeNumber, int PositionId);
        bool UpdateEmployeeName(string EmployeeNumber, string EmployeeName);
        bool UpdateEmployeeSalary(string EmployeeNumber, decimal Salary);
        bool UpdateEmployeeVacationDaysBalance(string EmployeeNumber, int newBalance);
        List<EmployeePendingVacationDto> GetAllEmployeesHaveOneOrMorePendingVacationRequests(string ReportedToEmployeeNumber);
        bool AddListOfEmployees(List<Employees> Employees);
        List<EmployeeBasicInfoDto> GetAllEmployeeReportedToSpecificEmployee(string ReportedToNumber);
    }
}
