﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Dtos
{
    public class EmployeeHavePendingVacationDto
    {
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public int PendingRequestCount { get; set; }
        public int TotalVacationDaysLeft { get; set; }

    }
}
