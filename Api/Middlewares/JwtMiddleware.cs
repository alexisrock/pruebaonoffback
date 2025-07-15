
using Domain.Dto.Request;
using MediatR;

namespace ApiRest.Middlewares
{
    /// <summary>
    /// Middleware de comprobacion del token
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate requestDelegate;


        /// <summary>
        /// Constructor
        /// </summary>
        public JwtMiddleware(RequestDelegate requestDelegat )
        {
            this.requestDelegate = requestDelegat;
           
        }

        /// <summary>
        /// Metodo para validar el token
        /// </summary>

        public async Task InvokeAsync(HttpContext context, ISender _sender) 
        {           
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if ((token is not null) && ValidateToken(token, _sender))
            {
                context.Items["UserId"] = "ALEXIS";
            }
            await requestDelegate(context);
        }

        private bool ValidateToken(string token, ISender _sender)
        {                
            return _sender.Send(new TokenRequest(){ Token= token }).Result;
        }


    }
}
