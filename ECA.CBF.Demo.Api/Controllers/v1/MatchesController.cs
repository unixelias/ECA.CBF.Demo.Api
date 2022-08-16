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
    public class MatchesController : BaseController
    {
        private readonly IMatchProcess _matchProcess;

        public MatchesController(IMatchProcess matchProcess, ILogger<TeamsController> logger) : base(logger)
        {
            _matchProcess = matchProcess;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MatchExtendedEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAll()
        {
            return await GetListAsync(async () => await _matchProcess.ListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MatchExtendedEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await GetListAsync(async () => await _matchProcess.GetAsync(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] MatchBaseEntity entity)
        {
            return await PostDataAsync<MatchBaseEntity, int, InternalErrorException>(async () => await _matchProcess.InsertAsync(entity), entity);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] MatchBaseEntity entity)
        {
            return await PutDataAsync<MatchBaseEntity, InternalErrorException>(async () => await _matchProcess.UpdateAsync(entity), entity);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _matchProcess.DeleteAsync(id), id);
        }
    }
}