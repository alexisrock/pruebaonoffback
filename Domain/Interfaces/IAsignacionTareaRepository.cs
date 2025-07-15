using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAsignacionTareaRepository
    {
        Task<List<AsignarTarea>> GetAll();
        Task<AsignarTarea?> GetById(object id);
        Task Insert(AsignarTarea obj);
        Task Update(AsignarTarea obj);
        Task Delete(object id);
        Task<AsignarTarea?> GetByParam(Expression<Func<AsignarTarea, bool>> obj);
    }
}
