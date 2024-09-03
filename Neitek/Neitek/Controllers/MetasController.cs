using Microsoft.AspNetCore.Mvc;
using Neitek.IRepository;
using Neitek.Shared.Models;

namespace Neitek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]        
    
    public class MetasController : ControllerBase
    {
        private readonly IMetasRepository _repository;

        public MetasController(IMetasRepository category)
        {
            _repository = category;
        }

        [HttpGet("All")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<Metas>>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<Metas>>>> All()
        {
            ApiResponse<IEnumerable<Metas>> apiResponse = new ApiResponse<IEnumerable<Metas>>();
            var result = await _repository.All();
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
        public async Task<IActionResult> Add(Metas metas)
        {
            ApiResponse<Mensaje> apiResponse = new ApiResponse<Mensaje>();
            var existeMeta = await _repository.GetByDsc(metas.Meta_Dsc);

            if (existeMeta.success)
            {
                if (existeMeta.data != null)
                {
                    apiResponse.success = false;
                    apiResponse.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Alerta, Texto = "La meta ya existe"};
                    return Ok(apiResponse);
                }
                var resultado = await _repository.Add(metas);
                apiResponse.success = resultado.success;
                apiResponse.data = resultado.data;
                apiResponse.error = resultado.error;
                return Ok(apiResponse);
            }
            else
            {
                apiResponse.success = existeMeta.success;
                apiResponse.error = existeMeta.error;
                return Ok(apiResponse);
            }
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(ApiResponse<Mensaje>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(short id)
        {
            ApiResponse<Mensaje> apiResponse = new ApiResponse<Mensaje>();
            var resultado = await _repository.Delete(id);
            apiResponse.success = resultado.success;
            apiResponse.data = resultado.data;
            apiResponse.error = resultado.error;
            return Ok(apiResponse);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(ApiResponse<Mensaje>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Metas metas)
        {
            ApiResponse<Mensaje> apiResponse = new();
            var resultado = await _repository.Update(metas);
            apiResponse.success = resultado.success;
            apiResponse.data = resultado.data;
            apiResponse.error = resultado.error;
            return Ok(apiResponse);
        }

        [HttpPut("Progreso")]
        [ProducesResponseType(typeof(ApiResponse<Mensaje>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProgreso(Metas metas)
        {
            ApiResponse<Mensaje> apiResponse = new();
            var resultado = await _repository.UpdateProgreso(metas);
            apiResponse.success = resultado.success;
            apiResponse.data = resultado.data;
            apiResponse.error = resultado.error;
            return Ok(apiResponse);
        }
    }  
}
