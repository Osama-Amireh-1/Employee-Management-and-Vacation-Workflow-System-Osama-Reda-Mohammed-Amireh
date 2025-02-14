using Data_Layer.DBContext;
using Data_Layer.Entities;
using Data_Layer.RequestStatesRepositoryFiles.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.RequestStatesRepositoryFiles
{
    public class RequestStatesRepository : IRequestStatesRepository
    {
        EFDbContext dbContext = new EFDbContext();

        public bool Add(RequestStates requestStates)
        {
            try
            {
                dbContext.RequestStates.Add(requestStates);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding RequestState: {ex.Message}");
                return false;             }
        }
        public List<RequestStates> GetAll()
        {
            try
            {
                return dbContext.RequestStates.ToList();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching all RequestStates: {ex.Message}");
                return new List<RequestStates>();
            }
        }
    }
}
