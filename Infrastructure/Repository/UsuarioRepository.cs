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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IRepository<Usuario> repository;

        public UsuarioRepository(IRepository<Usuario> _repository)
        {
            repository = _repository;
        }



        public async Task Delete(object id)
        {
            await repository.Delete(id);
        }

        public async Task<List<Usuario>> GetAll()
        {
           return await repository.GetAll();
        }

        public async Task<Usuario?> GetById(object id)
        {
            return await repository.GetById(id);
        }

        public async Task<Usuario?> GetByParam(Expression<Func<Usuario, bool>> obj)
        {
            return await repository.GetByParam(obj);
        }

        public async Task Insert(Usuario obj)
        {
            await repository.Insert(obj);
        }

        public async Task Update(Usuario obj)
        {
            await repository.Update(obj);
        }
    }
}
