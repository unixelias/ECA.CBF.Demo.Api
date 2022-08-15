using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Process.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TournmentController : BaseController
    {
        private readonly ITournmentProcess _tournmentProcess;

        public TournmentController(ITournmentProcess tournmentProcess, ILogger<TeamController> logger) : base(logger)
        {
            _tournmentProcess = tournmentProcess;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TournmentEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAll()
        {
            try
            {
                var result = await _tournmentProcess.ListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Erro encontrado: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TournmentEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var result = await _tournmentProcess.GetAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Erro encontrado: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TournmentEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] TournmentEntity entity)
        {
            try
            {
                var result = await _tournmentProcess.InsertAsync(entity);
                return Created(GetRoute(), result);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Erro encontrado: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.StackTrace);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] TournmentEntity entity)
        {
            try
            {
                await _tournmentProcess.UpdateAsync(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Erro encontrado: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _tournmentProcess.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Erro encontrado: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.StackTrace);
            }
        }
    }
}