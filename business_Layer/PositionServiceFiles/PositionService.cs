using Business_Layer.PositionServiceFiles.Interface;
using Data_Layer.Entities;
using Data_Layer.PositionRepositoryFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.PositionServiceFiles
{
    public class PositionService : IPositionService
    {
        PositionRepository PositionRepository=new PositionRepository();
   
        public bool AddListOfPositions(List<Positions> position)
        {
            return PositionRepository.AddListOfPositions(position);

        }

        public bool AddNewPosition(string positionName)
        {
            Positions position = new Positions
            {
                PositionName = positionName,
            };
            return PositionRepository.AddNewPosition(position); 
        }

        public bool DeletePosition(int positionId)
        {
            return PositionRepository.DeletePosition(positionId);

        }

        public List<Positions> GetAllPosition()
        {
            return PositionRepository.GetAll();

        }

        public bool UpdatePosition(int positionId, string positionName)
        {
            return PositionRepository.UpdatePosition(positionId, positionName);

        }
    }
}
