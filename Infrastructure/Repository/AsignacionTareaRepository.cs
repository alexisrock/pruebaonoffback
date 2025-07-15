using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repository
{
    public class AsignacionTareaRepository : IAsignacionTareaRepository
    {


        private readonly IRepository<AsignarTarea> repository;

        public AsignacionTareaRepository(IRepository<AsignarTarea> repository)
        {
            this.repository = repository;
        }

        public async Task Delete(object id)
        {
            await this.repository.Delete(id);
        }

        public async Task<List<AsignarTarea>> GetAll()
        {
            return await this.repository.GetAll();
        }

        public async Task<AsignarTarea?> GetById(object id)
        {
            return await this.repository.GetById(id);
        }

        public async Task<AsignarTarea?> GetByParam(Expression<Func<AsignarTarea, bool>> obj)
        {
            return await this.repository.GetByParam(obj);
        }

        public async Task Insert(AsignarTarea obj)
        {
            await this.repository.Insert(obj);
        }

        public async Task Update(AsignarTarea obj)
        {
            await this.repository.Update(obj);
        }
    }
}
