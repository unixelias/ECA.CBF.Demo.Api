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
    public class PlayersController : BaseController
    {
        private readonly IPlayerProcess _playerProcess;

        public PlayersController(IPlayerProcess playerProcess, ILogger<TeamsController> logger) : base(logger)
        {
            _playerProcess = playerProcess;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PlayerEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAllTeams()
        {
            try
            {
                var result = await _playerProcess.ListPlayerAsync();
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
        [ProducesResponseType(typeof(PlayerEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeam([FromRoute] int id)
        {
            try
            {
                var result = await _playerProcess.GetPlayerAsync(id);
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
        [ProducesResponseType(typeof(IEnumerable<PlayerEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertTeam([FromBody] PlayerEntity team)
        {
            try
            {
                var result = await _playerProcess.InsertPlayerAsync(team);
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
        public async Task<IActionResult> UpdateTeam([FromBody] PlayerEntity team)
        {
            try
            {
                await _playerProcess.UpdatePlayerAsync(team);
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
        public async Task<IActionResult> DeleteTeam([FromQuery] int id)
        {
            try
            {
                await _playerProcess.DeletePlayerAsync(id);
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