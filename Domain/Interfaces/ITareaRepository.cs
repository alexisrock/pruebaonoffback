using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITareaRepository
    {
        Task<Tarea?> GetId(int idTarea);
        Task Create(Tarea tarea);
        Task Delete(Tarea tarea);
        Task Update(Tarea tarea);    
        Task<IEnumerable<Tarea>> GetAll();



    }
}
