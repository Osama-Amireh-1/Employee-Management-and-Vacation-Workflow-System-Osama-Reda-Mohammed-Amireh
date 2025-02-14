using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DepartmentServiceFiles.Interface
{
    public interface IDepartmentService
    {
        bool AddListOfDepartments(List<Departments> department);
        bool AddNewDepartment(string departmentName);
        List<Departments> GetAllDepartments();
        bool DeleteDepartment(int departmentID);
        bool UpdateDepartment(int departmentID, string departmentName);
    }
}
