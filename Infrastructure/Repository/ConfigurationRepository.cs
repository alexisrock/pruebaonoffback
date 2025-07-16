using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IRepository<Domain.Entities.Configuration> repository;

        public ConfigurationRepository(IRepository<Domain.Entities.Configuration> _repository)
        {
            repository = _repository;
        }
        public async  Task<Domain.Entities.Configuration?> GetByParam(Expression<Func<Domain.Entities.Configuration, bool>> obj)
        {
           return await repository.GetByParam(obj);
        }

    }
}
