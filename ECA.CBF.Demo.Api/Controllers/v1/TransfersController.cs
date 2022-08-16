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
    public class TransfersController : BaseController
    {
        private readonly ITransferProcess _transferProcess;

        public TransfersController(ITransferProcess transferProcess, ILogger<TeamsController> logger) : base(logger)
        {
            _transferProcess = transferProcess;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TransferEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAll()
        {
            return await GetListAsync(async () => await _transferProcess.ListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransferEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await GetListAsync(async () => await _transferProcess.GetAsync(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TransferEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] TransferEntity transfer)
        {
            return await PostDataAsync<TransferEntity, int, InternalErrorException>(async () => await _transferProcess.InsertAsync(transfer), transfer);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] TransferEntity transfer)
        {
            return await PutDataAsync<TransferEntity, InternalErrorException>(async () => await _transferProcess.UpdateAsync(transfer), transfer);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _transferProcess.DeleteAsync(id), id);
        }
    }
}