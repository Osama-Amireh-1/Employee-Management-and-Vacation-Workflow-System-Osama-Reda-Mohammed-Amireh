using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.PositionServiceFiles.Interface
{
    public interface IPositionService
    {
        bool AddNewPosition(string PositionName);
        bool AddListOfPositions(List<Positions> position);
        List<Positions> GetAllPosition();
        bool DeletePosition(int positionId);
        bool UpdatePosition(int positionId, string positionName);
    }
}
