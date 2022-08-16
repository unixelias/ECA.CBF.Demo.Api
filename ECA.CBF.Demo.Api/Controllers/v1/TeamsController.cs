using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Entities.Exceptions;
using ECA.CBF.Demo.Process.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TeamsController : BaseController
    {
        private readonly ITeamProcess _teamProcess;

        public TeamsController(ITeamProcess teamProcess, ILogger<TeamsController> logger) : base(logger)
        {
            _teamProcess = teamProcess;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TeamEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAllTeams()
        {
            return await GetListAsync(async () => await _teamProcess.ListTeamsAsync());
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeam([FromRoute] int id)
        {
            return await GetListAsync(async () => await _teamProcess.GetTeamAsync(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TeamEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertTeam([FromBody] TeamEntity team)
        {
            return await PostDataAsync<TeamEntity, int, InternalErrorException>(async () => await _teamProcess.InsertTeamAsync(team), team);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTeam([FromBody] TeamEntity team)
        {
            return await PutDataAsync<TeamEntity, InternalErrorException>(async () => await _teamProcess.UpdateTeamAsync(team), team);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTeam([FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _teamProcess.DeleteTeamAsync(id), id);
        }
    }
}