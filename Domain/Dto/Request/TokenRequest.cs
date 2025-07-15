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
    public class TokenRequest : IRequest<bool>
    {
        public string? Token { get; set; }
    }



    public class TokenCreateRequest : IRequest<TokenResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }


}
