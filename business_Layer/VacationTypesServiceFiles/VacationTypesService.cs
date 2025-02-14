using Business_Layer.VacationTypesServiceFiles.Interface;
using Data_Layer.Entities;
using Data_Layer.VacationTypesRepositoryFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.VacationTypesServiceFiles
{
    public class VacationTypesService : IVacationTypesService
    {
        VacationTypesRepository VacationTypesRepository=new VacationTypesRepository();
        public bool AddNewVacationTypes(char vcationTypeCode, string vacationTypeName)
        {
            VacationTypes vacation = new VacationTypes
            {
                VacationTypeCode = vcationTypeCode,
                VacationTypeName = vacationTypeName
            };
        return    VacationTypesRepository.Add(vacation);
        }

        public List<VacationTypes> GetAllVacationTypes()
        {
          return  VacationTypesRepository.GetAll();

        }
    }
}
