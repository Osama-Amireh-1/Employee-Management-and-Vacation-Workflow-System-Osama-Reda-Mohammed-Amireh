using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.DepartmentRepositoryFile.Interface
{
    public interface IDepartmentRepository
    {
        bool AddListOfDepartments(List<Departments> department);
        bool AddDepartment(Departments department);
        List<Departments> GetAll();
        bool DeleteDepartment(int departmentID);
        bool UpdateDepartment(int departmentID, string departmentName);
    }
}
