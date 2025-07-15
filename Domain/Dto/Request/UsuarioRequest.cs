using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Dto.Response;
using MediatR;

namespace Domain.Dto.Request
{
    public class UsuarioRequest: IRequest<BaseResponse>
    {
        public string? NameUsuario { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class UsuarioUpdateRequest : IRequest<BaseResponse>
    {
        public int Id { get; set; }
        public string? NameUsuario { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Idrol { get; set; }
    }


    public class UsuarioDeleteRequest : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class UsuarioIdRequest : IRequest<UserResponse>
    {
        public int Id { get; set; }
    }


    public class UsuarioAll : IRequest<List<UserResponse>>
    {

    }
}
