using Domain.Interfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ContainerID
    {


        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITareaRepository, TareaRepository>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAsignacionTareaRepository, AsignacionTareaRepository>();
            services.AddScoped<IStoreProcedureRepository, StoreProcedureRepository>();
            return services;
        }
    }
}
