using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAll();
        Task<Usuario?> GetById(object id);
        Task Insert(Usuario obj);
        Task Update(Usuario obj);
        Task Delete(object id);
        Task<Usuario?> GetByParam(Expression<Func<Usuario, bool>> obj);

    }
}
