using Data_Layer.Entities;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.EmployeeRepositoryFiles.Interface
{
    public interface IEmployeeRepository
    {
        bool Add(Employees Employee);
        bool IsExist(string EmployeeNumber);
        List<EmployeeBasicInfoDto> GetAllEmployees();
        EmployeeDetailsDto GetEmployeeByEmployeeNumber(string EmployeeNumber);
        bool UpdateEmployeeDepartment(string EmployeeNumber, int DepartmentId);
        bool UpdateEmployeePosition(string EmployeeNumber, int PositionId);
        bool UpdateEmployeeName(string EmployeeNumber, string EmployeeName);
        bool UpdateEmployeeSalary(string EmployeeNumber, decimal Salary);
        bool UpdateEmployeeVacationDaysBalance(string EmployeeNumber, int newBalance);

        List<EmployeePendingVacationDto> GetAllEmployeesHaveOneOrMorePendingVacationRequests(string ReportedToEmployeeNumber);
        List<EmployeeBasicInfoDto> GetAllEmployeeReportedToSpecificEmployee(string ReportedToNumber);
        bool AddListOfEmployees(List<Employees> Employees);

    }
}
