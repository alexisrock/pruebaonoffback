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
    public class AsignarTareaController : ControllerBase
    {
        private readonly ISender _sender;

        /// <summary>
        /// Constructor
        /// </summary>


        public AsignarTareaController(ISender _sender)
        {
            this._sender = _sender;
        }




        /// <summary>
        /// Metodo para la creacion asignacion de la tarea      
        /// </summary>
        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AsignarTareaRequest request)
        {
            var user = await _sender.Send(request);
            return Ok(user);
        }


        /// <summary>
        /// Metodo para la eliminar asignacion de tarea     
        /// </summary>
        [HttpDelete, Route("[action]/{id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _sender.Send(new AsignarTareaDeleteRequest { Id = id });
            return Ok(user);
        }





        /// <summary>
        /// Metodo para la tareas asignadas por usuario    
        /// </summary>

        [HttpGet, Route("[action]/{id}")]
        [ProducesResponseType(typeof(List<AsignarTareaIdUsuarioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _sender.Send(new AsignarTareaIdUserRequest { IdUsuario = id });
            return Ok(user);
        }


        /// <summary>
        /// Metodo para la obtener todas las tareas asignadas
        /// </summary>

        [HttpGet, Route("[action]")]
        [ProducesResponseType(typeof(List<TareaAsignadasResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTareasAsignadas()
        {
            var user = await _sender.Send(new TareaAsignadasrequest { });
            return Ok(user);
        }




        /// <summary>
        /// Metodo para la obtener todas las tareas sin asignar
        /// </summary>

        [HttpGet, Route("[action]")]
        [ProducesResponseType(typeof(List<TareasSinAsignarResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTareasSinAsignar()
        {
            var user = await _sender.Send(new TareasSinAsignadasrequest { });
            return Ok(user);
        }
    }
}
