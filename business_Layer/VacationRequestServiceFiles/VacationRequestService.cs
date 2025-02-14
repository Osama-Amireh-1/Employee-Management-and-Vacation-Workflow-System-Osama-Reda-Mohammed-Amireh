using Business_Layer.VacationRequestServiceFiles.Interface;
using Data_Layer.Entities;
using Data_Layer.VacationRequestRepositoryFiles;
using Shared_Layer.Dtos;
using Shared_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.VacationRequestServiceFiles
{
    public class VacationRequestService : IVacationRequestService
    {
        VacationRequestRepository VacationRequestRepository=new VacationRequestRepository();

    

        public bool AddNewVcationRequest(string description, string employeeNumber, char vacationTypeCode, DateOnly startDate, DateOnly endDate)
        {
          return  VacationRequestRepository.AddNewVcationRequest(new VacationRequests(description, employeeNumber, vacationTypeCode, startDate, endDate));
        }

        public List<ApprovedVacationRequestsHistoryDto> GetAllApprovedRequestForEmployee(string employeeNumber)
        {
            return VacationRequestRepository.GetAllApprovedRequestForEmployee(employeeNumber);
        }

        public List<PendingVacationRequestsDetailsDto> GetAllPendingVacationRequestsForEmployee(string employeeNumber)
        {
            return VacationRequestRepository.GetAllPendingVacationRequestsForEmployee( employeeNumber);
        }

        public bool IsOverlappingWithExistingRequests(string employeeNumber, DateOnly newStartDate, DateOnly newEndDate)
        {
            return VacationRequestRepository.IsOverlappingWithExistingRequests(employeeNumber, newStartDate, newEndDate);
        }

        public bool UpdateVacationRequest(int RequestID, enRequestState state, string ReportedToNumber)
        {
           return VacationRequestRepository.UpdateVacationRequest( RequestID, state,ReportedToNumber);
        }

        public List<VacationsDateDto> GetAllVacationRequestsDate(string employeeNumber)
        {
            return VacationRequestRepository.GetAllVacationRequestsDate(employeeNumber);
        }
    }
}
