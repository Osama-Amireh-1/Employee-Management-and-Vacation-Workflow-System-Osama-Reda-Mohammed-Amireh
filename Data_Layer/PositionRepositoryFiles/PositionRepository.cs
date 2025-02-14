using Data_Layer.DBContext;
using Data_Layer.Entities;
using Data_Layer.PositionRepositoryFile.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.PositionRepositoryFile
{
    public class PositionRepository:IPositionRepository
    {
        EFDbContext dbContext = new EFDbContext();

        public bool AddNewPosition(Positions New)
        {
            try
            {
                dbContext.Positions.Add(New);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding new position: {ex.Message}");
                return false;

            }
        }

        public bool AddListOfPositions(List<Positions> position)
        {
            try
            {
                dbContext.Set<Positions>().AddRange(position);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding list of positions: {ex.Message}");
                return false;
            }

        }



        public List<Positions> GetAll()
        {
            try
            {
                var Positions = dbContext.Positions.ToList();
                return Positions;

            }
            catch (Exception ex)
            {

                Console.Error.WriteLine($"Error fetching positions: {ex.Message}");
                return new List<Positions>();
            }
        }

        public bool DeletePosition(int positionId)
        {
            try
            {
                var pos = dbContext.Positions.Where(e => e.PositionId == positionId).FirstOrDefault();
                dbContext.Positions.Remove(pos);
                dbContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting position with ID {positionId}: {ex.Message}");

                return false;
            }

        }

        public bool UpdatePosition(int positionId, string positionNam)
        {
            try
            {
                var pos = dbContext.Positions.Where(e => e.PositionId == positionId).FirstOrDefault();
                pos.PositionName = positionNam;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating position with ID {positionId}: {ex.Message}");
                return false ;
            }
        }
    }
}
