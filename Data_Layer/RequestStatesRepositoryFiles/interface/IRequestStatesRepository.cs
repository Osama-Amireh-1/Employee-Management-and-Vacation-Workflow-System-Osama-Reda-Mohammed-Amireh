using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.RequestStatesRepositoryFiles.Interface
{
    public interface IRequestStatesRepository
    {
        bool Add(RequestStates requestStates);
        List<RequestStates> GetAll();
    }
}
