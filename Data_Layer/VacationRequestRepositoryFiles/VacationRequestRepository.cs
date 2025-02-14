using Data_Layer.Entities;
using Data_Layer.VacationRequestRepositoryFiles.Interface;
using DTOs;
using Shared_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.VacationRequestRepositoryFiles
{
    public class VacationRequestRepository : IVacationRequestRepository
    {
        EFDbContext dbContext = new EFDbContext();

        public bool AddNewVcationRequest(VacationRequests vacationRequests)
        {
            try
            {
                dbContext.VacationRequests.Add(vacationRequests);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {

                Console.Error.WriteLine($"Error adding VacationRequest: {ex.Message}");

                return false;
            }

        }

        public List<VacationRequestsHistoryDto> GetAllApprovedRequestForEmployee(string employeeNumber)
        {
            try
            {
                var ApprovedRequest = (from req in dbContext.VacationRequests
                                       where req.EmployeeNumber == employeeNumber
                                       && req.RequestStateId == (int)enRequestState.Approved
                                       join VacType in dbContext.VacationTypes on req.VacationTypeCode equals VacType.VacationTypeCode
                                       join ApprovedBy in dbContext.Employees on req.EmployeeNumber equals ApprovedBy.EmployeeNumber
                                       select new VacationRequestsHistoryDto
                                       {
                                           VacationTypeName = VacType.VacationTypeName,
                                           VacationDescription = req.Description,
                                           StartDate = req.StartDate,
                                           EndDate = req.EndDate,
                                           TotalDayes = (req.EndDate.DayNumber - req.StartDate.DayNumber) + 1,
                                           ApprovedByEmployeeName = ApprovedBy.EmployeeName,




                                       }).ToList();

                return ApprovedRequest;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching approved vacation requests: {ex.Message}");
                return new List<VacationRequestsHistoryDto>();
            }

        }

        

        public List<PendingVacationRequestsDto> GetAllPendingVacationRequestsForEmployee(string employeeNumber)
        {
            try
            {
                var PendingVacation = (from req in dbContext.VacationRequests
                                       where req.EmployeeNumber == employeeNumber
                                       && req.RequestStateId == (int)enRequestState.Pending
                                       join Emp in dbContext.Employees on req.EmployeeNumber equals Emp.EmployeeNumber
                                       select new PendingVacationRequestsDto
                                       {
                                           Id = req.RequestId,
                                           VacationDescription = req.Description,
                                           EmployeeNumber = Emp.EmployeeNumber,
                                           EmployeeName = Emp.EmployeeName,
                                           SubmittedOnDate = req.RequestubmissionDate,
                                           StartDate = req.StartDate,
                                           EndDate = req.EndDate,
                                           VacationDuration = req.TotalVacationDays,
                                           EmployeeSalary = Emp.Salary

                                       }).ToList();
                return PendingVacation;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching pending vacation requests: {ex.Message}");
                return new List<PendingVacationRequestsDto>();
            }

        }

        public bool UpdateVacationRequest(int RequestID,enRequestState state,string ReportedToNumber)
        {
            try
            {
                var req = dbContext.VacationRequests.Where(e => e.RequestId == RequestID).FirstOrDefault();
                req.RequestStateId = (int)state;
                if (state == enRequestState.Approved)
                {
                    req.ApprovedByEmployeeNumber = ReportedToNumber;
                }
                else
                {
                    req.DeclinedByEmployeeNumber = ReportedToNumber;
                }
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating vacation request: {ex.Message}");
                return false;
            }

        }

        public bool IsOverlappingWithExistingRequests(string employeeNumber, DateOnly newStartDate, DateOnly newEndDate)
        {
            try
            {
                var isOverlapping = dbContext.VacationRequests
                                      .Any(req => req.EmployeeNumber == employeeNumber &&
                                                  newStartDate <= req.EndDate &&
                                                  newEndDate >= req.StartDate);
                return isOverlapping;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error checking for overlapping vacation requests: {ex.Message}");
                return true;

            }
        }

        public List<VacationsDateDto>GetAllVacationRequestsDate(string employeeNumber)
        {
            try
            {
                var existingRequests = (from Vac in dbContext.VacationRequests
                                        where Vac.EmployeeNumber == employeeNumber
                                        select new VacationsDateDto
                                        {
                                            StartDate = Vac.StartDate,
                                            EndDate = Vac.EndDate,

                                        }).ToList();
                return existingRequests;
            }
            catch (Exception ex)
            {

                Console.Error.WriteLine($"Error fetching vacation requests: {ex.Message}");

                return new List<VacationsDateDto>();
             }
                
        }

    }
}
