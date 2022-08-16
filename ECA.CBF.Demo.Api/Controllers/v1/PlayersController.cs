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
            return await GetListAsync(async () => await _playerProcess.ListPlayerAsync());
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlayerEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeam([FromRoute] int id)
        {
            return await GetListAsync(async () => await _playerProcess.GetPlayerAsync(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PlayerEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertTeam([FromBody] PlayerEntity team)
        {
            return await PostDataAsync<PlayerEntity, int, InternalErrorException>(async () => await _playerProcess.InsertPlayerAsync(team), team);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTeam([FromBody] PlayerEntity team)
        {
            return await PutDataAsync<PlayerEntity, InternalErrorException>(async () => await _playerProcess.UpdatePlayerAsync(team), team);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTeam([FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _playerProcess.DeletePlayerAsync(id), id);
        }
    }
}