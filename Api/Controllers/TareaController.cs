using ApiRest.Helpers;
using Domain.Dto.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Controlador de Tarea
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TareaController : ControllerBase
    {

        private readonly ISender _sender;


        /// <summary>
        /// Constructor
        /// </summary>
        public TareaController(ISender _sender)
        {
            this._sender = _sender;
        }

        /// <summary>
        /// Metodo para que el usuario guarde todas sus tareas
        /// </summary>
        [HttpPost, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> Create([FromBody] TareaRequest request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }



        /// <summary>
        /// Metodo para que el usuario actualice el estado de una tarea
        /// </summary>
        [HttpPatch, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> Update([FromBody] TareaupdateRequest request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }



        /// <summary>
        /// Metodo para que el usuario elimine una tarea
        /// </summary>
        [HttpDelete, Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> Delete(int  id)
        {
            var response = await _sender.Send(new TareaDeleteRequest() { IdTarea = id });
            return Ok(response);
        }





        /// <summary>
        /// Metodo para que el usuario obtenga todas las tareas
        /// </summary>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> GetAll()
        {
            var response = await _sender.Send(new TareaGetRequest() {  });
            return Ok(response);
        }




       



    }
}
