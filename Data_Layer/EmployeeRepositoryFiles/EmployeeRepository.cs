using Data_Layer.DBContext;
using Data_Layer.EmployeeRepositoryFiles.Interface;
using Data_Layer.Entities;
using DTOs;
using Shared_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data_Layer.EmployeeRepositoryFiles
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EFDbContext dbContext = new EFDbContext();
        public bool Add(Employees employee)
        {
            try
            {
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false; 
            }
            return true;
        }

        public List<EmployeeBasicInfoDto> GetAllEmployees()
        {
            List<EmployeeBasicInfoDto> employees;
            try
            {
                employees = (from Emp in dbContext.Employees
                             join d in dbContext.Departments on Emp.DepartmentId equals d.DepartmentId
                             select new EmployeeBasicInfoDto
                             {
                                 EmployeeNumber = Emp.EmployeeNumber,
                                 EmployeeName = Emp.EmployeeName,
                                 DepartmentName = d.DepartmentName,
                                 Salary = Emp.Salary


                             }).ToList();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching employees: {ex.Message}");
                return new List<EmployeeBasicInfoDto>();
            }
            return employees;
        }
        public List<EmployeeBasicInfoDto> GetAllEmployeeReportedToSpecificEmployee(string ReportedToNumber)
        {
            try
            {
                return (from Emp in dbContext.Employees
                                 join d in dbContext.Departments on Emp.DepartmentId equals d.DepartmentId
                                 where Emp.ReportedToEmployeeNumber == ReportedToNumber
                                 select new EmployeeBasicInfoDto
                                 {
                                     EmployeeNumber = Emp.EmployeeNumber,
                                     EmployeeName = Emp.EmployeeName,
                                     DepartmentName = d.DepartmentName,
                                     Salary = Emp.Salary


                                 }).ToList();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error retrieving employees reported to {ReportedToNumber}: {ex.Message}");
                return new List<EmployeeBasicInfoDto>(); // Instead of throwing, return an empty list
            }

        }
        public EmployeeDetailsDto GetEmployeeByEmployeeNumber(string EmployeeNumber)
        {
            try
            { 
            var employee = (from Emp in dbContext.Employees
                            join d in dbContext.Departments on Emp.DepartmentId equals d.DepartmentId
                            join P in dbContext.Positions on Emp.PositionId equals P.PositionId
                            join ReportedTo in dbContext.Employees on Emp.ReportedToEmployeeNumber equals ReportedTo.EmployeeNumber into ReportedGroup
                            from ReportedTo in ReportedGroup.DefaultIfEmpty()
                            where Emp.EmployeeNumber == EmployeeNumber
                            select new EmployeeDetailsDto
                            {
                                EmployeeNumber = Emp.EmployeeNumber,
                                EmployeeName = Emp.EmployeeName,
                                DepartmentName = d.DepartmentName,
                                PositionName = P.PositionName,
                                ReportedToEmployeeName = ReportedTo != null ? ReportedTo.EmployeeName : null,
                                TotalVacationDaysLeft = Emp.VacationDaysLeft
                            }).FirstOrDefault();
                return employee;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while retrieving employee {EmployeeNumber}: {ex.Message}");
                return new EmployeeDetailsDto();
            }

        }

        public bool UpdateEmployeeDepartment(string EmployeeNumber, int DepartmentId)
        {
            try
            {
                var Emp = dbContext.Employees.Where(e => e.EmployeeNumber == EmployeeNumber).FirstOrDefault();
                Emp.DepartmentId = DepartmentId;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating employee department: {ex.Message}");
                return false;
            }


        }

        public bool UpdateEmployeeName(string EmployeeNumber,string EmployeeName)
        {
            try
            {
                var Emp = dbContext.Employees.Where(e => e.EmployeeNumber == EmployeeNumber).FirstOrDefault();
                Emp.EmployeeName = EmployeeName;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating employee name: {ex.Message}");
                return false;
            }
        }

        public bool UpdateEmployeePosition (string EmployeeNumber, int PositionId)
        {
            try
            {
                var Emp = dbContext.Employees.Where(e => e.EmployeeNumber == EmployeeNumber).FirstOrDefault();
                Emp.PositionId = PositionId;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating employee position: {ex.Message}");
                return false ;
            }
        }

        public bool UpdateEmployeeSalary(string EmployeeNumber, decimal Salary)
        {
            try
            {
                var Emp = dbContext.Employees.Where(e => e.EmployeeNumber == EmployeeNumber).FirstOrDefault();
                Emp.Salary = Salary;
                dbContext.SaveChanges();
                return true; 
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating employee salary: {ex.Message}");

                return false ;
            }
        }

        public bool UpdateEmployeeVacationDaysBalance(string EmployeeNumber, int newBalance)
        {
            try
            {
                var Emp = dbContext.Employees.Where(e => e.EmployeeNumber == EmployeeNumber).FirstOrDefault();
                Emp.VacationDaysLeft = newBalance;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating employee vacation days balance: {ex.Message}");

                return false;
            }

        }


        public bool IsExist(string EmployeeNumber)
        {
            try
            {
                return dbContext.Employees.Where(e => e.EmployeeNumber == EmployeeNumber).Any();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error checking if employee exists: {ex.Message}");
                return false; 
            }
        }

        public List<EmployeePendingVacationDto> GetAllEmployeesHaveOneOrMorePendingVacationRequests(string ReportedToEmployeeNumber)
        {
            try
            {
                var Employees = (from Emp in dbContext.Employees
                                 join req in dbContext.VacationRequests
                                 on Emp.EmployeeNumber equals req.EmployeeNumber
                                 where Emp.ReportedToEmployeeNumber == ReportedToEmployeeNumber && req.RequestStateId == (int)enRequestState.Pending
                                 group req by new { Emp.EmployeeNumber, Emp.EmployeeName, Emp.VacationDaysLeft } into grouprd
                                 where grouprd.Count() >= 1
                                 select new EmployeePendingVacationDto
                                 {
                                     EmployeeNumber = grouprd.Key.EmployeeNumber,
                                     EmployeeName = grouprd.Key.EmployeeName,
                                     PendingRequestCount = grouprd.Count(),
                                     TotalVacationDaysLeft = grouprd.Key.VacationDaysLeft


                                 }).ToList();
                return Employees;
            }
            catch (Exception ex) {
                Console.Error.WriteLine($"Error fetching pending vacation requests: {ex.Message}");
                return new List<EmployeePendingVacationDto>();

             }

        }
        public bool AddListOfEmployees(List<Employees> Employees)
        {
            try
            {
                foreach (Employees emp in Employees)
                {
                    dbContext.Employees.Add(emp);
                }
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding employees: {ex.Message}");
                return false;

            }
        }
        }
}
