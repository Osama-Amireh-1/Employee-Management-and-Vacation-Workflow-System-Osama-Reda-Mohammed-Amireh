using Business_Layer.EmployeeServiceFiles.Interface;
using Data_Layer.EmployeeRepositoryFiles;
using Data_Layer.Entities;
using Shared_Layer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.EmployeeServiceFiles
{
    public class EmployeeService : IEmployeeService
    {
        EmployeeRepository EmployeeRepository=new EmployeeRepository();


        public bool AddNewEmployee(string employeeeNumber, string employeeeName, int departemntId, int positionId, char genderCode, string? reportedToEmployeeNumber, decimal salary)
        {
            return EmployeeRepository.Add(new Employees(employeeeNumber, employeeeName, departemntId, positionId, genderCode, reportedToEmployeeNumber, salary));

        }

        public List<EmployeeBasicInfoDto> GetAllEmployees()
        {
            return EmployeeRepository.GetAllEmployees();

        }

        public List<EmployeeHavePendingVacationDto> GetAllEmployeesHaveOneOrMorePendingVacationRequests(string ReportedToEmployeeNumber)
        {
            return EmployeeRepository.GetAllEmployeesHaveOneOrMorePendingVacationRequests(ReportedToEmployeeNumber);
        }

        public EmployeeDetailsDto GetEmployeeByEmployeeNumber(string EmployeeNumber)
        {
            return EmployeeRepository.GetEmployeeByEmployeeNumber(EmployeeNumber);
        }

        public bool IsEmployeeExist(string EmployeeNumber)
        {
            return EmployeeRepository.IsExist(EmployeeNumber);

        }

        public bool UpdateEmployeeDepartment(string EmployeeNumber, int DepartmentId)
        {
            return EmployeeRepository.UpdateEmployeeDepartment(EmployeeNumber, DepartmentId);
        }

        public bool UpdateEmployeeName(string EmployeeNumber, string EmployeeName)
        {
            return EmployeeRepository.UpdateEmployeeName(EmployeeNumber, EmployeeName);
        }

        public bool UpdateEmployeePosition(string EmployeeNumber, int PositionId)
        {
            return EmployeeRepository.UpdateEmployeePosition(EmployeeNumber, PositionId);
        }

        public bool UpdateEmployeeSalary(string EmployeeNumber, decimal Salary)
        {
            return EmployeeRepository.UpdateEmployeeSalary(EmployeeNumber, Salary);
        }

        public bool UpdateEmployeeVacationDaysBalance(string EmployeeNumber, int newBalance)
        {
            return EmployeeRepository.UpdateEmployeeVacationDaysBalance(EmployeeNumber, newBalance);
        }
        public bool AddListOfEmployees(List<Employees> Employees)
        {

            return EmployeeRepository.AddListOfEmployees(Employees);
            

        }
        public List<EmployeeBasicInfoDto> GetAllEmployeeReportedToSpecificEmployee(string ReportedToNumber)
        {
            return EmployeeRepository.GetAllEmployeeReportedToSpecificEmployee(ReportedToNumber);
        }
    }
}
