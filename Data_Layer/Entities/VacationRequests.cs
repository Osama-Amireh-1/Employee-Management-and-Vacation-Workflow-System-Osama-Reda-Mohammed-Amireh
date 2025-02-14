using Shared_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Entities
{
    public class VacationRequests
    {
        public int RequestId { get; set; }
        public DateTime RequestubmissionDate { get; set; }
        public string Description { get; set; }
        public string EmployeeNumber  { get; set; }
        public virtual Employees Employee { get; set; }
       public char VacationTypeCode { get; set; }
        public virtual VacationTypes VacationType { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public  int TotalVacationDays { get; set; }
        public int? RequestStateId { get; set; }
        public virtual RequestStates RequestStates { get; set; }
        public string ApprovedByEmployeeNumber { get; set; }
        public string DeclinedByEmployeeNumber { get; set; }
        public virtual Employees ApprovedByEmployee {  get; set; } 
        public virtual Employees DeclinedByEmployee { get; set; }

        public VacationRequests()
        {

        }
        public VacationRequests(string description, string employeeNumber, char vacationTypeCode, DateOnly startDate, DateOnly endDate)
        {
            RequestubmissionDate=DateTime.Now;
            Description = description;
            EmployeeNumber = employeeNumber;
            VacationTypeCode = vacationTypeCode;
            StartDate = startDate;
            EndDate = endDate;
            TotalVacationDays = (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days + 1; ;
            RequestStateId =(int) enRequestState.Pending;
            
        }
    }
}
