using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto.Response;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStoreProcedureRepository
    {
        Task<List<AsignarTareaIdUsuario>> GetAsignarTareaIdUsuario(int idUsuario);
        Task<List<TareaAsignadas>> GetTareasAsignadas();
        Task<List<TareasSinAsignar>> GetTareassinAsignadas();

    }
}
