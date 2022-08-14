using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Process.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamProcess _teamProcess;
        private readonly ILogger<TeamController> _logger;

        public TeamController(ITeamProcess teamProcess, ILogger<TeamController> logger)
        {
            _logger = logger;
            _teamProcess = teamProcess;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TeamEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAllTeams()
        {
            try
            {
                var result = await _teamProcess.ListTeamsAsync();
                _logger.LogInformation("Rota de times executada com sucesso");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro encontrado: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.StackTrace);
            }

        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeam([FromRoute] int id)
        {
            try
            {
                var result = await _teamProcess.GetTeamAsync(id);
                _logger.LogInformation("Rota de times executada com sucesso");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro encontrado: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.StackTrace);
            }

        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TeamEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertTeam([FromBody] TeamEntity team)
        {
            try
            {
                var result = await _teamProcess.InsertTeamAsync(team);
                _logger.LogInformation("Rota de times executada com sucesso");
                return Created(GetRoute(), result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro encontrado: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.StackTrace);
            }

        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTeam([FromBody] TeamEntity team)
        {
            try
            {
                await _teamProcess.UpdateTeamAsync(team);
                _logger.LogInformation("Rota de times executada com sucesso");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro encontrado: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.StackTrace);
            }

        }


        protected string GetRoute()
        {
            IHttpRequestFeature feature = HttpContext.Features.Get<IHttpRequestFeature>();
            return feature.RawTarget;
        }
    }
}