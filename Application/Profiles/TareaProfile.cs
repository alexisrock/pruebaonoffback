using System.Diagnostics.CodeAnalysis;
using Domain.Dto.Request;
using Domain.Dto.Response;
using Domain.Entities;

namespace Application.Profiles
{
    [ExcludeFromCodeCoverage]
    internal class TareaProfile : AutoMapper.Profile
    {


        public TareaProfile() {

            CreateMap<Tarea, TareaRequest>()
                 .ReverseMap();

            CreateMap<Tarea, TareaResponse>()
                .ReverseMap();

        }
    }
}
