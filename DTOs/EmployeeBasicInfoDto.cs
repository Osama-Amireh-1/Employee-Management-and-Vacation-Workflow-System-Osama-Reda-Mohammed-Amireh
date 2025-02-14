using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class EmployeeBasicInfoDto
    {
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public decimal Salary { get; set; }
    }
}
