using Business_Layer.DepartmentServiceFiles.Interface;
using Data_Layer.DepartmentRepositoryFile;
using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DepartmentServiceFiles
{
    public class DepartmentService:IDepartmentService
    {
        DepartmentRepository DepartmentRepository= new DepartmentRepository();

        public bool AddListOfDepartments(List<Departments> department)
        {
            return DepartmentRepository.AddListOfDepartments(department);

        }

      public bool AddNewDepartment(string departmentName)
        {
            Departments department = new Departments
            {
                DepartmentName = departmentName,
            };
          return  DepartmentRepository.AddDepartment(department);
        }

        public bool DeleteDepartment(int departmentID)
        {
            return DepartmentRepository.DeleteDepartment(departmentID);

        }

        public List<Departments> GetAllDepartments()
        {
            return DepartmentRepository.GetAll();

        }

        public bool UpdateDepartment(int departmentID, string departmentName)
        {
            return DepartmentRepository.UpdateDepartment(departmentID, departmentName);

        }
    }
}
