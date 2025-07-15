using ApiRest.Helpers;
using Domain.Common;
using Domain.Dto.Request;
using Domain.Dto.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Controlador de Usuario
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly ISender _sender;

        /// <summary>
        /// Constructor
        /// </summary>
        public UsuarioController(ISender _sender)
        {
            this._sender = _sender;
        }



        /// <summary>
        /// Metodo para la creacion del usuario con token      
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




        /// <summary>
        /// Metodo para la actualizar del usuario     
        /// </summary>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "Id": "1",
        ///        "NameUsuario": "prueba",
        ///        "Email": "prueba@gmail.com",
        ///        "Password": "cHJ1RUJB"
        ///        "Idrol": 2
        ///       
        ///     }
        ///
        /// </remarks>

        [HttpPut, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UsuarioUpdateRequest request)
        {
            var user = await _sender.Send(request);
            return Ok(user);
        }



        /// <summary>
        /// Metodo para la eliminar el usuario     
        /// </summary>

        [HttpDelete, Route("[action]/{id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete( int id)
        {
            var user = await _sender.Send(new UsuarioDeleteRequest { Id = id }  );
            return Ok(user);
        }




        /// <summary>
        /// Metodo para la obtener un usuario por id    
        /// </summary>

        [HttpGet, Route("[action]/{id}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _sender.Send(new UsuarioIdRequest { Id = id });
            return Ok(user);
        }


        /// <summary>
        /// Metodo para la obtener todos los usuario
        /// </summary>

        [HttpGet, Route("[action]")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var user = await _sender.Send(new UsuarioAll {  });
            return Ok(user);
        }


    }
}
