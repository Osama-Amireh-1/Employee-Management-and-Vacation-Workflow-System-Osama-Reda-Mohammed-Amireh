using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.VacationTypesRepositoryFiles.Interface
{
    public interface IVacationTypesRepository
    {
        bool Add(VacationTypes entity);
        List<VacationTypes> GetAll();

    }
}
