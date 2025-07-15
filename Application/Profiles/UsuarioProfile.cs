using AutoMapper;
using Domain.Dto;
using Domain.Dto.Request;
using Domain.Dto.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profile
{
    internal class UsuarioProfile: AutoMapper.Profile
    {

        public UsuarioProfile()
        {
            CreateMap<UsuarioRequest, Usuario>()
                .ReverseMap();

            CreateMap<UserResponse, Usuario>()
               .ReverseMap();

            //CreateMap<UserResponse, Usuario>()
            //   .ReverseMap();
        }
    }
}
