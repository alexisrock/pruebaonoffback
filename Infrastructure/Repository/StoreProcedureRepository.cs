using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto.Response;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class StoreProcedureRepository : IStoreProcedureRepository
    {

        private readonly PruebaContext _context;

        public StoreProcedureRepository(PruebaContext context)
        {
            _context = context;
        }

        public async Task<List<AsignarTareaIdUsuario>> GetAsignarTareaIdUsuario(int idUsuario)
        {
            var listAsignarTareaIdUsuario = await _context.AsignarTareaIdUsuario.FromSqlRaw("EXEC SPGetAsignacionTareasByIdUsuario @IdUsuario", new SqlParameter("@IdUsuario", idUsuario)).ToListAsync();
            return listAsignarTareaIdUsuario;
        }

        public async Task<List<TareaAsignadas>> GetTareasAsignadas()
        {
            var listAsignarTarea = await _context.TareaAsignadas.FromSqlRaw("EXEC SPGetAsignacionTareas").ToListAsync();
            return listAsignarTarea;
        }

        public async Task<List<TareasSinAsignar>> GetTareassinAsignadas()
        {
            var listAsignarSinTarea = await _context.TareasSinAsignar.FromSqlRaw("EXEC SPGettareaSinAsignacion").ToListAsync();
            return listAsignarSinTarea;
        }




    }
}
