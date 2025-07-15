using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Application.Validations;
namespace Application
{
    public static class ContainerID
    {



        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {       

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<TareaRequestValidator>();

            return services;
        }
    }
}
