using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.RequestStatesServiceFiles.Interface
{
    public interface IRequestStatesService
    {
        bool AddNewIRequestState(int id, string requestStatesName);
        List<RequestStates> GetAllRequestState();

    }
}
