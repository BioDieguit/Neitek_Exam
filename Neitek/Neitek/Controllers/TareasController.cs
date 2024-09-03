using Microsoft.AspNetCore.Mvc;
using Neitek.IRepository;
using Neitek.Shared.Models;

namespace Neitek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TareasController : ControllerBase
    {
        private readonly ITareasRepository _repository;

        public TareasController(ITareasRepository category)
        {
            _repository = category;
        }

        [HttpGet("All")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<Tareas>>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<Tareas>>>> All(short metaId)
        {
            ApiResponse<IEnumerable<Tareas>> apiResponse = new ApiResponse<IEnumerable<Tareas>>();
            var result = await _repository.All(metaId);
            apiResponse.success = result.success;
            apiResponse.data = result.data;
            apiResponse.error = result.error;
            return Ok(result);
        }

        [HttpPost("Add")]
        [ProducesResponseType(typeof(ApiResponse<Mensaje>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(Tareas tareas)
        {
            ApiResponse<Mensaje> apiResponse = new ApiResponse<Mensaje>();

            var existeTarea = await _repository.GetByDsc(tareas);

            if (existeTarea.success)
            {
                if (existeTarea.data != null)
                {
                    apiResponse.success = false;
                    apiResponse.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Alerta, Texto = "La tarea ya existe" };
                    return Ok(apiResponse);
                }
                var resultado = await _repository.Add(tareas);
                apiResponse.success = resultado.success;
                apiResponse.data = resultado.data;
                apiResponse.error = resultado.error;
                return Ok(apiResponse);
            }
            else
            {
                apiResponse.success = existeTarea.success;
                apiResponse.error = existeTarea.error;
                return Ok(apiResponse);
            }
        }

        [HttpDelete("DeleteAll")]
        [ProducesResponseType(typeof(ApiResponse<Mensaje>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(short metaId)
        {
            ApiResponse<Mensaje> apiResponse = new ApiResponse<Mensaje>();
            var resultado = await _repository.DeleteAll(metaId);
            apiResponse.success = resultado.success;
            apiResponse.data = resultado.data;
            apiResponse.error = resultado.error;
            return Ok(apiResponse);
        }

        [HttpDelete("DeleteOne")]
        [ProducesResponseType(typeof(ApiResponse<Mensaje>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string tareaId, short metaId)
        {
            ApiResponse<Mensaje> apiResponse = new ApiResponse<Mensaje>();
            var resultado = await _repository.DeleteOne(tareaId, metaId);
            apiResponse.success = resultado.success;
            apiResponse.data = resultado.data;
            apiResponse.error = resultado.error;
            return Ok(apiResponse);
        }

        [HttpPut("UpdateTarea")]
        [ProducesResponseType(typeof(ApiResponse<Mensaje>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Tareas [] tareas)
        {
            ApiResponse<Mensaje> apiResponse = new();
            var resultado = await _repository.Update(tareas);
            apiResponse.success = resultado.success;
            apiResponse.data = resultado.data;
            apiResponse.error = resultado.error;
            return Ok(apiResponse);
        }

        [HttpPut("Complete")]
        [ProducesResponseType(typeof(ApiResponse<Mensaje>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Complete(Tareas tareas)
        {
            ApiResponse<Mensaje> apiResponse = new();
            var resultado = await _repository.Complete(tareas);
            apiResponse.success = resultado.success;
            apiResponse.data = resultado.data;
            apiResponse.error = resultado.error;
            return Ok(apiResponse);
        }

        [HttpPut("Importancia")]
        [ProducesResponseType(typeof(ApiResponse<Mensaje>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Importancia(Tareas tareas)
        {
            ApiResponse<Mensaje> apiResponse = new();
            var resultado = await _repository.Importancia(tareas);
            apiResponse.success = resultado.success;
            apiResponse.data = resultado.data;
            apiResponse.error = resultado.error;
            return Ok(apiResponse);
        }
    }
}
