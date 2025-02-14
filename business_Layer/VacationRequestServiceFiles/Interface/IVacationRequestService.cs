using Data_Layer.Entities;
using DTOs;
using Shared_Layer;
using Shared_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.VacationRequestServiceFiles.Interface
{
    public interface IVacationRequestService
    {
        bool AddNewVcationRequest(string description, string employeeNumber, char vacationTypeCode, DateOnly startDate, DateOnly endDate);
        List<VacationRequestsHistoryDto> GetAllApprovedRequestForEmployee(string employeeNumber);
        List<PendingVacationRequestsDto> GetAllPendingVacationRequestsForEmployee(string employeeNumber);

        bool UpdateVacationRequest(int RequestID, enRequestState state,string ReportedToNumber);
        bool IsOverlappingWithExistingRequests(string employeeNumber, DateOnly newStartDate, DateOnly newEndDate);
        List<VacationsDateDto> GetAllVacationRequestsDate(string employeeNumber);
    }
}
