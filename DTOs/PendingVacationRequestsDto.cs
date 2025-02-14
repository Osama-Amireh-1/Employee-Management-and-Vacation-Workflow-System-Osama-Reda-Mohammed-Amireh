using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class PendingVacationRequestsDto
    {
        public int Id { get; set; }
        public string VacationDescription { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public DateTime SubmittedOnDate { get; set; }
        public int VacationDuration { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal EmployeeSalary { get; set; }



    }
}
