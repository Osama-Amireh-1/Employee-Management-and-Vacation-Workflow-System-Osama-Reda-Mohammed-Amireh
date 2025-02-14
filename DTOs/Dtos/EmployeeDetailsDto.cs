using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Dtos
{
    public class EmployeeDetailsDto
    {
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string? ReportedToEmployeeName { get; set; }
        public int TotalVacationDaysLeft { get; set; }

    }
}
