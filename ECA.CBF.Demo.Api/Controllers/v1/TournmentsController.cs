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
    public class TournmentsController : BaseController
    {
        private readonly ITournmentProcess _tournmentProcess;

        public TournmentsController(ITournmentProcess tournmentProcess, ILogger<TeamsController> logger) : base(logger)
        {
            _tournmentProcess = tournmentProcess;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TournmentEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAll()
        {
            return await GetListAsync(async () => await _tournmentProcess.ListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TournmentEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await GetListAsync(async () => await _tournmentProcess.GetAsync(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TournmentEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] TournmentEntity entity)
        {
            return await PostDataAsync<TournmentEntity, int, InternalErrorException>(async () => await _tournmentProcess.InsertAsync(entity), entity);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] TournmentEntity entity)
        {
            return await PutDataAsync<TournmentEntity, InternalErrorException>(async () => await _tournmentProcess.UpdateAsync(entity), entity);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _tournmentProcess.DeleteAsync(id), id);
        }
    }
}