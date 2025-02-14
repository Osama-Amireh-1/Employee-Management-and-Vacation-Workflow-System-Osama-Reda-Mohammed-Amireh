using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.VacationTypesServiceFiles.Interface
{
    public interface IVacationTypesService
    {
        bool AddNewVacationTypes(char vacationTypeCode, string vacationTypeName);
        List<VacationTypes> GetAllVacationTypes();
    }
}
