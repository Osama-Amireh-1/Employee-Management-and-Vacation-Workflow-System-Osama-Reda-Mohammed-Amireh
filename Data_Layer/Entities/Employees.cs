using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Entities
{
    public class Employees
    {
      public  string EmployeeNumber {  get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentId { get; set; }
        public virtual Departments Department { get; set; }
        public int PositionId { get; set; }
        public virtual Positions Position {  get; set; }
        public char GenderCode { get; set; }
        public string ReportedToEmployeeNumber {  get; set; }
        public virtual Employees? ReportedToEmployee { get; set; }
        public int VacationDaysLeft { get; set; }
        public decimal Salary { get; set; }
        public virtual List<VacationRequests> Vacations { get; set; }
        public Employees()
        {

        }
        public Employees(string employeeeNumber,string employeeeName,int departemntId,int positionId,char genderCode, string? reportedToEmployeeNumber, decimal salary)
        {
            EmployeeNumber = employeeeNumber;
            EmployeeName = employeeeName;
            DepartmentId = departemntId;
            PositionId = positionId;
            GenderCode = genderCode;
            ReportedToEmployeeNumber = reportedToEmployeeNumber;
            Salary = salary;
            
        }

    }
}
