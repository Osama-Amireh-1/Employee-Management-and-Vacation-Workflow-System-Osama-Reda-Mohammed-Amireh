using Business_Layer.DepartmentServiceFiles;
using Business_Layer.EmployeeServiceFiles;
using Business_Layer.PositionServiceFiles;
using Business_Layer.RequestStatesServiceFiles;
using Business_Layer.VacationRequestServiceFiles;
using Business_Layer.VacationTypesServiceFiles;
using Data_Layer.DepartmentRepositoryFile;
using Data_Layer.EmployeeRepositoryFiles;
using Data_Layer.Entities;
using Data_Layer.PositionRepositoryFile;
using Data_Layer.VacationRequestRepositoryFiles;
using Shared_Layer.Dtos;
using Shared_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_and_Vacation_Workflow_System
{
    public static class Screens
    {
        static DepartmentService DepartmentService=new DepartmentService();
        static PositionService PositionService = new PositionService();
        static EmployeeService EmployeeService = new EmployeeService();
        static VacationTypesService VacationTypesService = new VacationTypesService();
        static VacationRequestService VacationRequestService = new VacationRequestService();
        static RequestStatesService RequestStatesService=new RequestStatesService();
        static EmployeeDetailsDto ActiveEmployee;
        #region 'Validate'
        static private decimal ValidateUserInputIsANumber(string Choice, string S, bool withRange = false, int min = 1, int max = 4)
        {
            decimal num = -1;
            if (withRange)
            {
                while (!(decimal.TryParse(Choice, out num) && num >= min && num <= max))
                {
                    Console.Write($"Invlid input,Please enter only numbers from {min} to {max} \n{S} ");
                    Choice = Console.ReadLine();
                }

                return num;
            }
            else
            {
                while (!(decimal.TryParse(Choice, out num)))
                {
                    Console.Write($"Invlid input,Please enter only integar number \n{S} ");
                    Choice = Console.ReadLine();
                }

                return num;
            }
        }

        static string GetInputValue(string fieldValue, string fieldType, string S = "")
        {
            bool IsValid = false;
            while (!IsValid)
            {
                if (string.IsNullOrWhiteSpace(fieldValue))
                {
                    Console.WriteLine($"{fieldType} cannot be empty. Please try again. {S}");
                    Console.Write($"Enter {fieldType}: ");
                    fieldValue = Console.ReadLine();
                }
                else
                    IsValid = true;
            }
            return fieldValue;
        }
        static DateOnly ValidateDate(string fieldValue, string fieldType, string S = "")
        {
            DateOnly validatedDate;

            while (!DateOnly.TryParse(fieldValue, out validatedDate))
            {
                Console.Write($"Invalid {fieldType}. Please enter a valid date in the format yyyy-MM-dd.");
                fieldValue = Console.ReadLine();
            }
            return validatedDate;
        }
        static string ValidateMaxlength(string fieldValue, int MaxLength)
        {
            while (fieldValue.Length > MaxLength)
            {
                Console.Write($"Input exceeds maximum length ({MaxLength} characters). Please try again:");
                fieldValue = GetInputValue(Console.ReadLine(), "Employee Number", $"Enter Employee Number (Max {MaxLength} chars):");
            }
            return fieldValue.Trim();
        }
        static string VaidateNewEmployeeNumber(string fieldValue)
        {
            while (EmployeeService.IsEmployeeExist(fieldValue.ToUpper()))
            {
                Console.WriteLine("This Employee Number already exisit please enter anoter Number!,");
                fieldValue = ValidateMaxlength(GetInputValue(Console.ReadLine(), "Employee Number", "Enter Employee Number (Max 6 chars):"), 6);

            }
            return fieldValue;
        }
        static char ValidateGender(char fieldValue)
        {
            while(fieldValue != 'M' && fieldValue != 'F')
            {
                Console.Write("Invalid Gender Code!,Enter Gender Code (M/F):");
                fieldValue= Console.ReadKey().KeyChar;
                Console.WriteLine();

            }
            return fieldValue;
        }
        static string IsEmployeeNumberExist(string fieldValue,bool AlowSkip=false,string skip= "Enter a valid employee number or press enter to skip:")
        {
            while(!EmployeeService.IsEmployeeExist(fieldValue))
            {
                Console.WriteLine("The entered employee number does not exist. Please try again.");
                if(AlowSkip)
                {
                    Console.Write($"{skip}");

                }
                fieldValue = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(fieldValue)|| fieldValue=="0")
                {
                    return null;
                }

                ValidateMaxlength(fieldValue, 6);
            }
            return fieldValue.ToUpper();

        }
        static string ValidateVacationType(string fieldValue)
        {
            List<string> validCodes = new List<string> { "S", "U", "A", "O", "B" }; 
            while(!validCodes.Contains(fieldValue))
            {

                    Console.Write("Invalid code! Enter a valid Vacation Type Code (S, U, A, O, B): ");
                    fieldValue = ValidateMaxlength(GetInputValue(Console.ReadLine(), "Vacation Type Code", "Enter Vacation Type Code"), 1).ToUpper();
                
            }
            return fieldValue;
        }
        static DateOnly ValidateEndDate(DateOnly SartDate, DateOnly EndDate) {

            while(SartDate>EndDate)
            {
                Console.WriteLine("End date cannot be earlier than the start date. Please enter a valid end date.");
                EndDate = ValidateDate(GetInputValue(Console.ReadLine(), "End Date", "Enter End Date (YYYY-MM-DD):"), "End Date");

            }
            return EndDate;
        }

        static bool IsDateInFuture(ref DateOnly date)
        {
            while (date <= DateOnly.FromDateTime(DateTime.Today))
            {
                Console.Write("The start date must be in the future. Please enter a new vacation start date:");

                string Sdate = GetInputValue(Console.ReadLine(), "Start Date", "Please enter a new vacation start date (YYYY-MM-DD) or Enter 0 to cancel:");
                if (Sdate == "0") return false; 

                date = ValidateDate(Sdate, "Start Date");
            }
            return true;  
        }
        #endregion

        #region 'Department Event'
        static void ShowAllDeparment()
        {
            List<Departments> departments=DepartmentService.GetAllDepartments();
            if (departments == null || departments.Count == 0)
            {
                Console.WriteLine("No departments found.");
                return;
            }
            Console.WriteLine("List of Departments:");
            Console.WriteLine("---------------------");

            foreach (var department in departments)
            {
                Console.WriteLine($"ID: {department.DepartmentId}, Name: {department.DepartmentName}");
            }

        }
        #endregion

        #region 'Static Add '
        public static void AddListOfDepatment()
      {
            List<Departments> list = new List<Departments>
            {
             new Departments { DepartmentName = "Human Resources" },
            new Departments { DepartmentName = "Finance" },
            new Departments { DepartmentName = "Marketing" },
            new Departments { DepartmentName = "Sales" },
            new Departments { DepartmentName = "IT" },
            new Departments { DepartmentName = "Customer Support" },
            new Departments { DepartmentName = "Legal" },
            new Departments { DepartmentName = "Operations" },
            new Departments { DepartmentName = "Research & Development" },
            new Departments { DepartmentName = "Product Management" },
            new Departments { DepartmentName = "Engineering" },
            new Departments { DepartmentName = "Quality Assurance" },
            new Departments { DepartmentName = "Business Development" },
            new Departments { DepartmentName = "Design" },
            new Departments { DepartmentName = "Procurement" },
            new Departments { DepartmentName = "Logistics" },
            new Departments { DepartmentName = "Public Relations" },
            new Departments { DepartmentName = "Administration" },
            new Departments { DepartmentName = "Training & Development" },
            new Departments { DepartmentName = "Data Analytics" },
            };

           if( DepartmentService.AddListOfDepartments(list))
            {
                Console.WriteLine("Departments Added Successfully");
            }
           else
            {
                Console.WriteLine("Add Departments failed");
            }
      }
       public static void AddListOfEmployees()
        {
            List<Employees> employees = new List<Employees>
            {
        new Employees("EMP001", "Osama Amireh", 6, 6, 'M', null, 2500.50m),
        new Employees("EMP002", "Tariq Aymen", 2, 2, 'M', "EMP001", 3000.00m),
        new Employees("EMP003", "Rami Mohammed", 5,11, 'M', "EMP001", 2800.75m),
        new Employees("EMP004", "Bayan khaled", 1, 1, 'F', null, 3200.00m),
        new Employees("EMP005", "Ghaidaa Hammad", 19,19 ,'F', "EMP003", 3100.50m),
        new Employees("EMP006", "Abdullah Khaled", 20, 20, 'M', "EMP004", 2900.00m),
        new Employees("EMP007", "Nadia Zeyad", 12, 12, 'F', "EMP005", 2700.25m),
        new Employees("EMP008", "Razan Ali", 18, 18, 'F', "EMP005", 3300.75m),
        new Employees("EMP009", "Mohammed Yaser", 16, 16, 'M', "EMP006", 3400.00m),
        new Employees("EMP010", "Ahmed Omar", 9, 9, 'M', "EMP007", 2500.50m)
            };
            if(EmployeeService.AddListOfEmployees(employees))
            {
                Console.WriteLine("Employees Added Successfully");

            }
            else
            {
                Console.WriteLine("Employees Added Successfully");

            }
        }
        public static void AddListOfPositions()
        {
            List<Positions> list = new List<Positions>
            {
        new Positions { PositionName = "HR Manager" },
        new Positions { PositionName = "Finance Analyst" },
        new Positions { PositionName = "Marketing Specialist"},
        new Positions { PositionName = "Sales Representative" },
        new Positions { PositionName = "IT Support Specialist"},
        new Positions { PositionName = "Manager"},
        new Positions { PositionName = "Legal Advisor" },
        new Positions { PositionName = "Operations Manager"},
        new Positions { PositionName = "R&D Scientist"},
        new Positions { PositionName = "Product Manager" },
        new Positions { PositionName = "Software Engineer" },
        new Positions { PositionName = "QA Engineer" },
        new Positions { PositionName = "Business Development Manager" },
        new Positions { PositionName = "UI/UX Designer" },
        new Positions { PositionName = "Procurement Specialist"},
        new Positions { PositionName = "Logistics Coordinator" },
        new Positions { PositionName = "Public Relations Specialist" },
        new Positions { PositionName = "Admin Assistant" },
        new Positions { PositionName = "Training Coordinator" },
        new Positions { PositionName = "Data Analyst" }
            };
           if( PositionService.AddListOfPositions(list))
            {
                Console.WriteLine("Positions Added Successfully");

            }
           else
            {
                Console.WriteLine("Add Positions failed");

            }
        }
        public static void AddRequestStates()
        {
            if (RequestStatesService.AddNewIRequestState(1,"Pending"))
            {
                Console.WriteLine("Request States Added Successfully");

            }
            else
            {
                Console.WriteLine("Add Request States failed");

            }
            if(RequestStatesService.AddNewIRequestState(2,"Approved"))
            {
                Console.WriteLine("Request States Added Successfully");

            }
            else
            {
                Console.WriteLine("Add Request States failed");

            }
           if( RequestStatesService.AddNewIRequestState(3,"Declined"))
            {
                Console.WriteLine("Request States Added Successfully");

            }
           else
            {
                Console.WriteLine("Add Request States failed");

            }


        }
        public static void AddVacationTypes()
        {
            VacationTypesService.AddNewVacationTypes('S', "Sick");
            VacationTypesService.AddNewVacationTypes('U', "Unpaid");
            VacationTypesService.AddNewVacationTypes('A', "Annual");
            VacationTypesService.AddNewVacationTypes('O', "Day Off");
            VacationTypesService.AddNewVacationTypes('B', "Business Trip");

        }

        #endregion

        #region 'Position Event'
        static void ShowAllPosition()
        {
            List<Positions>positions =PositionService.GetAllPosition();
            if (positions == null || positions.Count == 0)
            {
                Console.WriteLine("No positions found.");
                return;
            }

            // Display positions
            Console.WriteLine("List of Positions:");
            Console.WriteLine("-------------------");

            foreach (var position in positions)
            {
                Console.WriteLine($"ID: {position.PositionId}, Name: {position.PositionName}");
            }
        }
        #endregion

        #region 'Employee Event'
        static int GetValidRequestNumber(List<PendingVacationRequestsDetailsDto> pendingVacations,int ReqID)
        {
            while(!pendingVacations.Any(vacation => vacation.Id == ReqID)&& ReqID!=0)
            {
                Console.WriteLine("No matching request found with this ID. Please enter a valid request number.");
                 ReqID = (int)ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "Request Number", "Enter the Request Number of the request you want to update (or enter 0 to cancel)"), "Request Number");

            }
            return ReqID;
        }
        static void ShowAllEmployeeBriefDetails()
        {
            List<EmployeeBasicInfoDto>employees=EmployeeService.GetAllEmployees();
            if (employees == null || employees.Count == 0)
            {
                Console.WriteLine("\nNo employees found.");
                return;
            }

            Console.WriteLine("\nEmployee Brief Details:");
            Console.WriteLine("------------------------");

            foreach (var employee in employees)
            {
                Console.WriteLine($"Number: {employee.EmployeeNumber}, Name: {employee.EmployeeName}, Department: {employee.DepartmentName}");
            }

        }
        static void ShowAllEmployee()
        {
            List<EmployeeBasicInfoDto> employees = EmployeeService.GetAllEmployees();
            if (employees == null || employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
                return;
            }

            Console.WriteLine("Employee Details:");
            Console.WriteLine("------------------------");

            foreach (var employee in employees)
            {
                Console.WriteLine($"Number: {employee.EmployeeNumber}, Name: {employee.EmployeeName}, Department: {employee.DepartmentName},Salary: {employee.Salary}");
            }
        }
        static void AddNewEmployee()
        {

            Console.Write("Enter Employee Number (Max 6 chars) or Enter 0 to cancel: ");
            string employeeNumber = VaidateNewEmployeeNumber(ValidateMaxlength(GetInputValue( Console.ReadLine(), "Employee Number", "Enter Employee Number (Max 6 chars) or Enter 0 to cancel:"),6));
            if (employeeNumber == "0")
                return;
            Console.Write("Enter Employee Name (Max 20 chars) or Enter 0 to cancel: ");
            string employeeName = ValidateMaxlength(GetInputValue(Console.ReadLine(), "Employee Name", "Enter Employee Name (Max 20 chars) or Enter 0 to cancel:"), 20);
            if (employeeName == "0")
                return;
            ShowAllDeparment();
            Console.Write("Enter Department Id or Enter 0 to cancel: ");
            int departmentIdInput =(int) ValidateUserInputIsANumber( GetInputValue( Console.ReadLine(), "Department Id", "Enter Department Id or Enter 0 to cancel:"), "Enter Department Id or Enter 0 to cancel:");
            if (departmentIdInput == 0)
                return;
            ShowAllPosition();
            Console.Write("Enter Position Id or Enter 0 to cancel: ");
            int positionIdInput =(int) ValidateUserInputIsANumber( GetInputValue( Console.ReadLine(), "Position Id", "Enter Position Id or Enter 0 to cancel:"), "Enter Position Id or Enter 0 to cancel:");
            if (positionIdInput == 0)
                return;
            Console.Write("\nEnter Gender Code (M/F) or Enter 0 to cancel: ");
            char genderCode = Console.ReadKey().KeyChar;
            if (genderCode == '0')
                return;
            genderCode = ValidateGender(genderCode);

            ShowAllEmployeeBriefDetails();
            Console.Write("Enter Reported To Employee Number (Max 6 chars, press Enter to skip): ");
            string reportedTo = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(reportedTo))
            {
                reportedTo = IsEmployeeNumberExist(reportedTo,true);
            }

                Console.Write("Enter Salary or Enter 0 to cancel: ");
             decimal salary = ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "Salary", "Enter Salary  or Enter 0 to cancel:"), "Enter Salary  or Enter 0 to cancel:");
            if(salary == 0) return;
            if (EmployeeService.AddNewEmployee(employeeNumber.Trim(), employeeName.Trim(), departmentIdInput, positionIdInput, genderCode, reportedTo.Trim(), salary))
            {
                Console.WriteLine("Employee Added Successfully");
            }
            else
            {
                Console.WriteLine("Add Employee Falied");
            }

        }    
        static void ResponseToShowAllEmployeeHavePendingRequestMenu(enShowAllEmployeeHavePendingRequestOperation operation)
        {
            switch (operation)
            {
                case enShowAllEmployeeHavePendingRequestOperation.TakeActionForEmployee:
                    Console.Write("Enter Employee Number To Tack action on:");
                    string Number = IsEmployeeNumberExist(GetInputValue(Console.ReadLine(), "Employee Number", "Enter your Number:"), true, "Enter a valid employee number or press 0 to back to main menu:");
                    if (Number == "0")
                    {
                        return;

                    }
                    TakeActionForEmployee(Number);
                    break;
                case enShowAllEmployeeHavePendingRequestOperation.BackToMainMenu:
                    return;
            }
        }
        static void ShowAllEmployeeHavePendingRequestMenu()
        {
            Console.WriteLine("1.Take Action For Employee");
            Console.WriteLine("2.Back To Main Menu");
            Console.Write("Enter your choice: ");
            int choice = (int)ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "your choice", "Enter your choice:"), "your choice", true, 1, 2);
            ResponseToShowAllEmployeeHavePendingRequestMenu((enShowAllEmployeeHavePendingRequestOperation)choice);
        }
        static  bool PrintAllEmployeeHavePendingRequest()
        {
            List<EmployeeHavePendingVacationDto> employeeHavePendingVacations = EmployeeService.GetAllEmployeesHaveOneOrMorePendingVacationRequests(ActiveEmployee.EmployeeNumber);
            if (employeeHavePendingVacations == null || employeeHavePendingVacations.Count == 0)
            {
                Console.WriteLine("\n No employees currently have pending vacation requests.");
                return false;
            }

            Console.WriteLine("\n Employees with Pending Vacation Requests:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(" Employee # | Employee Name   | Pending Requests | Days Left ");
            Console.WriteLine("--------------------------------------------------");

            foreach (var employee in employeeHavePendingVacations)
            {
                Console.WriteLine($" {employee.EmployeeNumber,-11} | {employee.EmployeeName,-15} | {employee.PendingRequestCount,-15} | {employee.TotalVacationDaysLeft,-10}");
            }

            Console.WriteLine("--------------------------------------------------");
            return true;
        }
        static void ShowAllEmployeeHavePendingRequest()
        {

           if( PrintAllEmployeeHavePendingRequest())
            ShowAllEmployeeHavePendingRequestMenu();
        }
        static void PrintAllEmployeeReportedToSpecificEmployee(List<EmployeeBasicInfoDto> employees)
        {
            if (employees == null || employees.Count == 0)
            {
                Console.WriteLine("No employees report to you.");
                return;
            }

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("| Employee Number | Name              | Department       | Salary    |");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var emp in employees)
            {
                Console.WriteLine($"| {emp.EmployeeNumber,-15} | {emp.EmployeeName,-17} | {emp.DepartmentName,-15} | {emp.Salary,8:C} |");
            }

            Console.WriteLine("-------------------------------------------------------------");
        }
        static string GetValidEmployeeNumber(List<EmployeeBasicInfoDto> employees,string number)
        {
            while (!employees.Any(Emp => Emp.EmployeeNumber == number) && number != "0")
            {
                Console.Write("No matching employee found with this ID. Please enter a valid Employee Number (or enter 0 to cancel):");
                number = ValidateMaxlength(GetInputValue(Console.ReadLine(), "Employee Number", "Enter the Employee Number you want to update (or enter 0 to cancel):"), 6);

            }
            return number;
        }
        static void ShowEmployeeDetails(string EmpNumper)
        {
           EmployeeDetailsDto employee= EmployeeService.GetEmployeeByEmployeeNumber(EmpNumper);
            Console.WriteLine("Current Employee Details:");
            Console.WriteLine("=============================");
            Console.WriteLine($"Employee Number: {employee.EmployeeNumber}");
            Console.WriteLine($"Name: {employee.EmployeeName}");
            Console.WriteLine($"Department: {employee.DepartmentName}");
            Console.WriteLine($"Position: {employee.PositionName}");
            Console.WriteLine("=============================");

        }
        static void EditMyReportedEmployeeInfo()
        {
            List<EmployeeBasicInfoDto> employees = EmployeeService.GetAllEmployeeReportedToSpecificEmployee(ActiveEmployee.EmployeeNumber);
            if (employees.Count > 0)
            {
                PrintAllEmployeeReportedToSpecificEmployee(employees);
                Console.Write("Enter the Employee Number you want to update (or enter 0 to cancel): ");

                string Number = ValidateMaxlength(GetInputValue(Console.ReadLine(), "Employee Number", "Enter the Employee Number you want to update (or enter 0 to cancel):"), 6);
                Number = GetValidEmployeeNumber(employees, Number);
                if (Number == "0")
                {
                    Console.WriteLine("Action canceled.");
                    return;
                }
                ShowEmployeeDetails(Number);
                EditEmployeeMenu(Number);
            }
            else
            {
                Console.WriteLine("No Employee Reported To you");
            }
        }
        static void EditEmployeeName(string EmpNumber)
        {
            Console.Write("Enter New Neme (Max 20 chars):");
            string employeeName = ValidateMaxlength(GetInputValue(Console.ReadLine(), "Employee Name", "Enter New Employee Name (Max 20 chars) or Enter 0 to cancel:"), 20);
            if (employeeName == "0")
                return;
            if (EmployeeService.UpdateEmployeeName(EmpNumber, employeeName))
            {
                Console.WriteLine("Employee Updated Successfully");
            }
            else
            {
                Console.WriteLine("Update Employee Failed");

            }

        }
        static void EditDepartment(string EmployeeNumber)
        {
            ShowAllDeparment();
            Console.Write("Enter New Department Id or Enter 0 to cancel: ");
            int departmentIdInput = (int)ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "Department Id", "Enter New Department Id or Enter 0 to cancel:"), "Enter New Department Id or Enter 0 to cancel:");
            if (departmentIdInput == 0)
                return;
            if(EmployeeService.UpdateEmployeeDepartment(EmployeeNumber, departmentIdInput))
            Console.WriteLine("Employee Updated Successfully");
            else
             Console.WriteLine("Update Employee Failed");

        }
        static void EditPosition(string EmployeeNumber)
        {
            ShowAllPosition();
            Console.Write("Enter New Position Id or Enter 0 to cancel: ");
            int positionIdInput = (int)ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "Position Id", "Enter New Position Id or Enter 0 to cancel:"), "Enter New Position Id or Enter 0 to cancel:");
            if (positionIdInput == 0)
                return;
           if( EmployeeService.UpdateEmployeePosition(EmployeeNumber, positionIdInput))
            Console.WriteLine("Employee Updated Successfully");
           else
                Console.WriteLine("Update Employee Failed");

        }
        static void UpdateSalary(string EmployeeNumber)
        {
            Console.Write("Enter Salary: ");
            decimal salary = ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "Salary", "Enter Salary or Enter 0 to cancel:"), "Enter Salary or Enter 0 to cancel:");
            if (salary == 0) return;
            if(EmployeeService.UpdateEmployeeSalary(EmployeeNumber, salary))
            Console.WriteLine("Employee Updated Successfully");
            else
                Console.WriteLine("Update Employee Failed");


        }
        static void ResponseToEditEmployeeMenu(enEditEmployeeInfo option,string EmpNumber)
        {
            switch (option)
            {
                case enEditEmployeeInfo.EditName:
                    EditEmployeeName(EmpNumber);
                    break;
                case enEditEmployeeInfo.EditDepartment:
                    EditDepartment(EmpNumber);
                    break;
                case enEditEmployeeInfo.EditPosition:
                    EditPosition(EmpNumber);
                    break;
                case enEditEmployeeInfo.EditSalary:
                    UpdateSalary(EmpNumber);
                    break;
                case enEditEmployeeInfo.BackToMainMenu:
                    return;
            }
        }
        static void EditEmployeeMenu(string EmpNumber)
        {
            Console.WriteLine("1.Edit Name");
            Console.WriteLine("2.Edit Department");
            Console.WriteLine("3.Edit Position");
            Console.WriteLine("4.Edit Salary");
            Console.WriteLine("5.Back To Main Menu");
            int Choice = (int)ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "your choice", "Enter your choice:"), "Enter your choice:", true, 1, 5);
            ResponseToEditEmployeeMenu((enEditEmployeeInfo)Choice, EmpNumber);
        }
        #endregion

        #region 'Vacation Types'

        static void ShowAllVacationTypes()
        {
            List<VacationTypes> vacationTypes=VacationTypesService.GetAllVacationTypes();
            if (vacationTypes == null || vacationTypes.Count == 0)
            {
                Console.WriteLine("No vacation types available.");
                return;
            }

            Console.WriteLine("===== Vacation Types =====");
            foreach (var type in vacationTypes)
            {
                Console.WriteLine($"Code: {type.VacationTypeCode} - Name: {type.VacationTypeName}");
            }
        }
        #endregion

        #region 'Vacation Request Event'
        static bool IsRequestGreaterThanVacationDaysAvailable(ref DateOnly startDate, ref DateOnly endDate)
        {
            int totalDays = (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days + 1;

            while (ActiveEmployee.TotalVacationDaysLeft < totalDays)
            {
                Console.WriteLine($"You only have {ActiveEmployee.TotalVacationDaysLeft} vacation days left. You cannot exceed this limit.");

                Console.Write("Please enter a new vacation start date (YYYY-MM-DD) or Enter 0 to cancel: ");
                string Sdate = GetInputValue(Console.ReadLine(), "Start Date", "Please enter a valid start date (YYYY-MM-DD) or Enter 0 to cancel:");
                if (Sdate == "0") return false;

                startDate = ValidateDate(Sdate, "Start Date");
                if(!IsDateInFuture(ref startDate))
                {
                    return false;
                }
                Console.Write("Please enter a new vacation end date (YYYY-MM-DD) or Enter 0 to cancel: ");
                string EDate = GetInputValue(Console.ReadLine(), "End Date", "Enter End Date (YYYY-MM-DD) or Enter 0 to cancel:");
                if (EDate == "0") return false;

                endDate = ValidateEndDate(startDate, ValidateDate(EDate, "End Date"));

                totalDays = (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days + 1;
            }

            return true;
        }

        static bool IsOverlappingDateWithExistingRequests(ref DateOnly startDate, ref DateOnly endDate)
        {
            bool isFirstTime = true;

            while (VacationRequestService.IsOverlappingWithExistingRequests(ActiveEmployee.EmployeeNumber, startDate, endDate))
            {
                if (isFirstTime)
                {
                    ShowAllVacationsDateForEmployee();
                    isFirstTime = false;
                }

                Console.WriteLine("The selected vacation dates overlap with an existing request.");

                Console.Write("Please enter new vacation start date (YYYY-MM-DD) or Enter 0 to cancel: ");
                string Sdate = GetInputValue(Console.ReadLine(), "Start Date", "Enter Start Date (YYYY-MM-DD) or Enter 0 to cancel:");
                if (Sdate == "0") return true;

                startDate = ValidateDate(Sdate, "Start Date");

                if (!IsDateInFuture(ref startDate))
                {
                    return true;
                }
                Console.Write("Please enter new vacation end date (YYYY-MM-DD) or Enter 0 to cancel: ");
                string EDate = GetInputValue(Console.ReadLine(), "End Date", "Enter End Date (YYYY-MM-DD) or Enter 0 to cancel:");
                if (EDate == "0") return true;

                endDate = ValidateEndDate(startDate, ValidateDate(EDate, "End Date"));

                if (!IsRequestGreaterThanVacationDaysAvailable(ref startDate, ref endDate))
                {
                    return true;
                }
            }

            return false;
        }
        static void ShowAllVacationsDateForEmployee()
        {
            Console.WriteLine("\n Existing Vacation Requests:");

            List<VacationsDateDto> vacationsDates=VacationRequestService.GetAllVacationRequestsDate(ActiveEmployee.EmployeeNumber);
            foreach (var vacationDate in vacationsDates)
                Console.WriteLine($"Start Date :{vacationDate.StartDate} End Date: {vacationDate.EndDate}");
        }
        static void SubmitNewVacationRequest()
        {
            if (ActiveEmployee.ReportedToEmployeeName == null)
            {
                Console.WriteLine("You Don't need Submit Vacation");
                return;
            }

            Console.WriteLine("Submit New VacationR Screen");
            ShowAllVacationTypes();
            Console.Write("Enter Vacation Type Code (A for Annual, S for Sick, etc.) or Enter 0 to cancel: ");

            string vacationTypeCode = ValidateMaxlength(GetInputValue(Console.ReadLine(), "Vacation Type Code", "Enter Vacation Type Code"), 1);
            if (vacationTypeCode == "0")return;
            ValidateVacationType(vacationTypeCode.ToUpper());
            Console.Write("Enter Vacation Description (max 100 chars) or Enter 0 to cancel: ");
            string description = ValidateMaxlength(GetInputValue(Console.ReadLine(), "Vacation Type Code", "Enter Vacation Type Code"), 100);
            if (description == "0") return;
            Console.Write("Enter Start Date (YYYY-MM-DD) or Enter 0 to cancel: ");
    
            string Sdate = GetInputValue(Console.ReadLine(), "Start Date", "Enter Start Date (YYYY-MM-DD) or Enter 0 to cancel:");
            if(Sdate == "0") return;
            DateOnly startDate =ValidateDate(Sdate, "Start Date");
            if (!IsDateInFuture(ref startDate))
            {
                return;
            }
            DateOnly endDate;
            if (vacationTypeCode[0] != 'O')
            {
                Console.Write("Enter End Date (YYYY-MM-DD)  or Enter 0 to cancel: ");
                string EDate = GetInputValue(Console.ReadLine(), "End Date", "Enter End Date (YYYY-MM-DD)  or Enter 0 to cancel:");
                if (EDate == "0") return;
                 endDate = ValidateEndDate(startDate, ValidateDate(EDate, "End Date"));
            }
            else
            {
                endDate = startDate;
            }
            if (IsRequestGreaterThanVacationDaysAvailable(ref startDate, ref endDate))
            {
                if (!IsOverlappingDateWithExistingRequests(ref startDate, ref endDate))
                {
                    if (VacationRequestService.AddNewVcationRequest(description.Trim(), ActiveEmployee.EmployeeNumber.Trim(), vacationTypeCode[0], startDate, endDate))
                        Console.WriteLine("Submit Vacation Request Successfully");
                    else
                        Console.WriteLine("Submit Vacation Request Failed");

                }
                else
                {
                    Console.WriteLine("Canceled Request");
                }
            }
            else
            {
                Console.WriteLine("Canceled Request");
            }
        }
        static void ShowMyApprovedVacationHistory()
        {
           List<ApprovedVacationRequestsHistoryDto> vacationRequestsHistories= VacationRequestService.GetAllApprovedRequestForEmployee(ActiveEmployee.EmployeeNumber);
            if(vacationRequestsHistories == null || !vacationRequestsHistories.Any())
            {
                Console.WriteLine("\n No approved vacation requests found.");
                return;
            }
            Console.WriteLine("\nMy Approved Vacation History:");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(" Vacation Type | Description      | Start Date | End Date   | Days | Approved By  ");
            Console.WriteLine("----------------------------------------------------------------------------------");

            foreach (var vacation in vacationRequestsHistories)
            {
                Console.WriteLine($" {vacation.VacationTypeName,-13} | {vacation.VacationDescription,-15} | {vacation.StartDate:yyyy-MM-dd} | {vacation.EndDate:yyyy-MM-dd} | {vacation.TotalDayes,-4} | {vacation.ApprovedByEmployeeName}");
            }

            Console.WriteLine("----------------------------------------------------------------------------------");
        }
        static void ResponseTakeActionForEmployeeMenu(enTakeActionOption option, PendingVacationRequestsDetailsDto ActiveRequest, string EmployeeNumber)
        {
            EmployeeDetailsDto employeeDetails = EmployeeService.GetEmployeeByEmployeeNumber(EmployeeNumber);
            int requestedDays = (ActiveRequest.EndDate.ToDateTime(TimeOnly.MinValue) - ActiveRequest.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1;
            switch (option)
            {
                case enTakeActionOption.Approved:
                    if (employeeDetails.TotalVacationDaysLeft < requestedDays)
                    {
                        Console.WriteLine($"Insufficient vacation days. The employee only has {employeeDetails.TotalVacationDaysLeft} days left but requested {requestedDays} days.");
                        Console.WriteLine("Approval denied.");
                        return;
                    }
                    VacationRequestService.UpdateVacationRequest(ActiveRequest.Id, enRequestState.Approved, ActiveEmployee.EmployeeNumber);
                   if( EmployeeService.UpdateEmployeeVacationDaysBalance(EmployeeNumber, employeeDetails.TotalVacationDaysLeft - requestedDays))
                    {
                        Console.WriteLine("Vacation Approved");
                    }
                    break;
                case enTakeActionOption.Decline:
                   if( VacationRequestService.UpdateVacationRequest(ActiveRequest.Id, enRequestState.declined, ActiveEmployee.EmployeeNumber))
                    {
                        Console.WriteLine("Vacation Decline");

                    }
                    break;
                case enTakeActionOption.BackToMainMenu:
                    return;
            }
        }
        static void TakeActionForEmployeeMenu(PendingVacationRequestsDetailsDto ActiveRequest, string EmployeeNumber)
        {
            Console.WriteLine("\n1.Approved");
            Console.WriteLine("2.Decline");
            Console.WriteLine("3.Back To Main Menu");
            Console.Write("Enter your choice:");
            int choice = (int)ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "your choice", "Enter your choice:"), "your choice", true, 1, 3);
            ResponseTakeActionForEmployeeMenu((enTakeActionOption)choice, ActiveRequest, EmployeeNumber);
        }
        static void ShowAllPendingVacationRequestsForEmployee(string EmployeeNumber, List<PendingVacationRequestsDetailsDto> pendingVacations)
        {
            Console.Clear();

            if (pendingVacations == null || !pendingVacations.Any())
            {
                Console.WriteLine("\n No pending vacation requests found for this employee.");
               
            }

            Console.WriteLine("\nPending Vacation Requests:");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Req# | Employee ID | Employee Name   | Submitted On | Duration  | Start Date | End Date   | Salary    | Description          ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");

            foreach (var vacation in pendingVacations)
            {
                Console.WriteLine($" {vacation.Id,-5} | {vacation.EmployeeNumber,-10} | {vacation.EmployeeName,-15} | " +
                                  $"{vacation.SubmittedOnDate:yyyy-MM-dd} | {GetVacationDuration(vacation.VacationDuration),-9} | " +
                                  $"{vacation.StartDate:yyyy-MM-dd} | {vacation.EndDate:yyyy-MM-dd} | " +
                                  $"{vacation.EmployeeSalary,-10:C} | {vacation.VacationDescription,-20}");
            }

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
        }
        static void TakeActionForEmployee(string EmployeeNumber)
        {
            List<PendingVacationRequestsDetailsDto> pendingVacations = VacationRequestService.GetAllPendingVacationRequestsForEmployee(EmployeeNumber);
            ShowAllPendingVacationRequestsForEmployee(EmployeeNumber, pendingVacations);
            if (pendingVacations == null || !pendingVacations.Any())
                return;
            Console.Write("Enter the Request Number of the request you want to update (or enter 0 to cancel): ");

            int input = (int)ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "Request Number", "Enter the Request Number of the request you want to update (or enter 0 to cancel)"), "Request Number");
            input = GetValidRequestNumber(pendingVacations, input);
            if (input == 0)
            {
                Console.WriteLine("Action canceled.");
                return;
            }
            PendingVacationRequestsDetailsDto ActiveRequest = pendingVacations.Where(e => e.Id == input).FirstOrDefault();
            TakeActionForEmployeeMenu(ActiveRequest, EmployeeNumber);
        }
        static string GetVacationDuration(int totalDays)
        {

            int weeks = totalDays / 7;
            int days = totalDays % 7;
            string duration = $"{(weeks > 0 ? $"{weeks} week{(weeks > 1 ? "s" : "")}" : "")}" +
                     $"{(weeks > 0 && days > 0 ? ", " : "")}" +
                     $"{(days > 0 ? $"{days} day{(days > 1 ? "s" : "")}" : "")}";

            return string.IsNullOrWhiteSpace(duration) ? "0 days" : duration;
        }
        #endregion
        static bool ResponseToMainMenu(enMainMenuOption menuOption)
        {
            Console.Clear();
            switch (menuOption)
            {
                case enMainMenuOption.ShowAllEmployees:
                    ShowAllEmployee();
                    break;
                case enMainMenuOption.SubmitNewVacationRequest:
                    SubmitNewVacationRequest();
                    break;
                case enMainMenuOption.ShowAllEmployeeHavePendingRequest:
                    ShowAllEmployeeHavePendingRequest();
                    break;
                case enMainMenuOption.ShowMyApprovedVacationHistory:
                    ShowMyApprovedVacationHistory();
                    break;
                case enMainMenuOption.EditMyReportedEmployeeInfo:
                    EditMyReportedEmployeeInfo();
                    break;
                case enMainMenuOption.Logout:
                    return false;

            }
            Console.WriteLine("Press any key to return main menu");
            Console.ReadKey();
            return true;
        }
        static void MainMenu()
        {

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine($"\nEmoloyee Number: {ActiveEmployee.EmployeeNumber}, Emplpyee Name {ActiveEmployee.EmployeeName}");
                Console.WriteLine("Main Menu");
                Console.WriteLine("==================================");
                Console.WriteLine("1.Show All Employees");
                Console.WriteLine("2.Submit New Vacation Request");
                Console.WriteLine("3.Show All Employee have pending request");
                Console.WriteLine("4.Show My Approved Vacation History");
                Console.WriteLine("5.Edit My Reported Employee Info");
                Console.WriteLine("6.Logout");
                Console.Write("Enter your choice:");
                int choice = (int)ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "your choice", "Enter your choice:"), "your choice", true, 1, 6);
                isRunning= ResponseToMainMenu((enMainMenuOption)choice);

            }
            Console.WriteLine("Logging out... Returning to system entry.");
        }

        static void Login()
        {
            Console.WriteLine("Login Screeen");
            Console.Write("Enter your Number or Enter 0 to skip:");
            string Number =  GetInputValue(Console.ReadLine(), "Employee Number", "Enter your Number:");
            if(Number =="0")
            {

                return;
            }
            Number= IsEmployeeNumberExist(Number.ToUpper(), true, "Enter a valid employee number or Enter 0 to skip:");

            ActiveEmployee =EmployeeService.GetEmployeeByEmployeeNumber(Number.Trim());

        }

        public static bool ResponseToEnterToSystem(enEnterToSystem enterToSystem)
        {
            Console.Clear();
            switch (enterToSystem)
            {
                case enEnterToSystem.NewEmployee:
                    AddNewEmployee();
                    Console.WriteLine("Press any key to Enter To System");

                    Console.ReadKey();
                    return true;
                case enEnterToSystem.Login:
                    Login();
                    if (ActiveEmployee != null)
                    {
                        Console.Clear();
                        MainMenu();
                    }
                       return true;
                case enEnterToSystem.Exit:
                    Console.WriteLine("Exiting....");
                    return false;
            }
            return true;
        }
        public static void EnterToSystem()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Welcome To Our System");
                Console.WriteLine("1.Add New Employee");
                Console.WriteLine("2.Login");
                Console.WriteLine("3.Exit");
                Console.Write("Enter your choice:");
                int Choice = (int)ValidateUserInputIsANumber(GetInputValue(Console.ReadLine(), "your choice", "Enter your choice:"), "Enter your choice:", true, 1, 3);
                isRunning= ResponseToEnterToSystem((enEnterToSystem)Choice);
            }

        }

    }
}
