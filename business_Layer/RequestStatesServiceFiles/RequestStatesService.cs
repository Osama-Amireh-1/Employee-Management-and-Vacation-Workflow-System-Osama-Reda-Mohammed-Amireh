using Business_Layer.RequestStatesServiceFiles.Interface;
using Data_Layer.Entities;
using Data_Layer.RequestStatesRepositoryFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.RequestStatesServiceFiles
{
    public class RequestStatesService:IRequestStatesService
    {
        RequestStatesRepository RequestStatesRepository=new RequestStatesRepository();
  
        public bool AddNewIRequestState(int id,string requestStatesName)
        {
            RequestStates requestStates = new RequestStates
            {
                StateId = id,
                StateName = requestStatesName
            };
           return RequestStatesRepository.Add(requestStates);
        }

        public List<RequestStates> GetAllRequestState()
        {
            return RequestStatesRepository.GetAll();
        }

       
    }
}
