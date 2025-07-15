using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IConfigurationRepository
    {
        Task<Configuration?> GetByParam(Expression<Func<Configuration, bool>> obj);
    }
}
