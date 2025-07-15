using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto.Request;
using Domain.Entities;

namespace Application.Profiles
{
    internal class AsignarTareaProfile : AutoMapper.Profile
    {


        public AsignarTareaProfile() {

            CreateMap<AsignarTareaRequest, AsignarTarea>()
               .ReverseMap();
        }
    }
}
