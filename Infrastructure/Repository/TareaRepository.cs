using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repository
{
    public class TareaRepository: ITareaRepository
    {
        private readonly IRepository<Tarea> repository;

        public TareaRepository(IRepository<Tarea> _repository)
        {
            repository = _repository;
        }

        public async Task Create(Tarea tarea)
        {
            await repository.Insert(tarea);            
        }

        public async Task Delete(Tarea tarea)
        {
            await repository.Delete(tarea);
        }

        public async Task<IEnumerable<Tarea>> GetAll()
        {
           return  await repository.GetAll();
        }

        public async Task<Tarea?> GetId(int idTarea)
        {
            return await repository.GetById(idTarea);
        }

        public async Task Update(Tarea tarea)
        {
            await repository.Update(tarea);
        }
    }
}
