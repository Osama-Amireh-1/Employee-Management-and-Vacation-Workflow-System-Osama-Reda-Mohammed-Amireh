using Data_Layer.DepartmentRepositoryFile.Interface;
using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.DepartmentRepositoryFile
{
    public class DepartmentRepository : IDepartmentRepository
    {
        EFDbContext dbContext=new EFDbContext();
        public bool AddListOfDepartments(List<Departments> department)
        {
            try
            {
                dbContext.AddRange(department);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding departments: {ex.Message}");
                return false;

            }
            return true;
        }

        public  bool AddDepartment(Departments department)
        {
            try
            {
                dbContext.Departments.Add(department);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding department: {ex.Message}");
                return false;
            }
            return true;


        }

        public  List<Departments> GetAll()
        {
            List<Departments> dep;
            try
            {
                 dep = dbContext.Departments.ToList();

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching departments: {ex.Message}");

                return new List<Departments>();
                
             }
            return dep;

        }

        public bool DeleteDepartment(int departmentID)
        {
            try
            {

                var department = dbContext.Departments.Where(x => x.DepartmentId == departmentID).FirstOrDefault();
                dbContext.Departments.Remove(department);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public  bool UpdateDepartment(int departmentID, string departmentName)
        {
            try
            {
                var department = dbContext.Departments.Where(x => x.DepartmentId == departmentID).FirstOrDefault();
                department.DepartmentName = departmentName;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting department: {ex.Message}\n{ex.StackTrace}");
                    return false;
            }
            return true;

        }
    }
}
