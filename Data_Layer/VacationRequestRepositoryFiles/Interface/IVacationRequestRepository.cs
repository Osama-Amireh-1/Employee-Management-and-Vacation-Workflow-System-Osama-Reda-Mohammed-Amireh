using Data_Layer.Entities;
using Shared_Layer.Dtos;
using Shared_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.VacationRequestRepositoryFiles.Interface
{
    public interface IVacationRequestRepository
    {
        bool AddNewVcationRequest(VacationRequests vacationRequests);
        List<ApprovedVacationRequestsHistoryDto> GetAllApprovedRequestForEmployee(string employeeNumber);
        List<PendingVacationRequestsDetailsDto> GetAllPendingVacationRequestsForEmployee(string employeeNumber);

        bool UpdateVacationRequest(int RequestID, enRequestState state,  string ReportedToNumber);
        bool IsOverlappingWithExistingRequests(string employeeNumber, DateOnly newStartDate, DateOnly newEndDate);
        List<VacationsDateDto> GetAllVacationRequestsDate(string employeeNumber);
    }
}
