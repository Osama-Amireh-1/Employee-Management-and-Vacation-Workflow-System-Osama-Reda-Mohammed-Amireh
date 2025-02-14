using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.PositionRepositoryFile.Interface
{
    public interface IPositionRepository
    {
        bool AddNewPosition(Positions New);
        bool AddListOfPositions(List<Positions> position);
        List<Positions> GetAll();
        bool DeletePosition(int positionId);
        bool UpdatePosition(int positionId, string positionNam);
    }
}
