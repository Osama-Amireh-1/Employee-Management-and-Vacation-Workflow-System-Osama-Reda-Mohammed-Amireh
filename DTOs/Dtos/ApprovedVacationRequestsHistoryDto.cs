using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Dtos
{
    public class ApprovedVacationRequestsHistoryDto
    {
        public string VacationTypeName { get; set; }
        public string VacationDescription { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TotalDayes;
        public string ApprovedByEmployeeName { get; set; }

    }
}
