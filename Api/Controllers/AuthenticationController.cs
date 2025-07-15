using Domain.Dto;
using System.Net;
using Microsoft.AspNetCore.Mvc; 
using Domain.Dto.Response;
using Domain.Dto.Request;
using MediatR;
using Domain.Common;

namespace ApiRest.Controllers
{
    /// <summary>
    /// Controller of Authentication
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _sender;

        

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthenticationController(ISender sender)
        {
            this._sender = sender;
        }



        /// <summary>
        /// Metodo de autenticacion      
        /// </summary>
        ///  
        /// <returns></returns>
        /// /// <remarks>
        /// Request example:
        ///  
        ///     {
        ///        "Email": "prueba@correo.com",
        ///        "Password": "cHJ1RUJB"
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authentication([FromBody] TokenCreateRequest userTokenRequest)
        {
            var user = await _sender.Send(userTokenRequest);
            return Ok(user);
        }






        /// <summary>
        /// Metodo para la creacion del usuario sin token      
        /// </summary>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "NameUsuario": "prueba",
        ///        "Email": "prueba@gmail.com",
        ///        "Password": "cHJ1RUJB"
        ///       
        ///     }
        ///
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UsuarioRequest request)
        {
            var user = await _sender.Send(request);
            return Ok(user);
        }


    }
}
