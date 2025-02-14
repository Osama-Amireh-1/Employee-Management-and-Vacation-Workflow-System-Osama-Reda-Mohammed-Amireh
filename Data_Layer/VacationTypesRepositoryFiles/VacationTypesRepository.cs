using Data_Layer.DBContext;
using Data_Layer.Entities;
using Data_Layer.VacationTypesRepositoryFiles.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.VacationTypesRepositoryFiles
{
    public class VacationTypesRepository : IVacationTypesRepository
    {
        EFDbContext dbContext = new EFDbContext();

        public bool Add(VacationTypes entity)
        {
            try 
            {
                dbContext.VacationTypes.Add(entity);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding VacationType: {ex.Message}");

                return false;
            }




        }

        public List<VacationTypes> GetAll()
        {
            try
            {
                var VacationTypes = dbContext.VacationTypes.ToList();
                return VacationTypes;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching VacationTypes: {ex.Message}");
                return new List<VacationTypes>();
            }


        }
    }
}
