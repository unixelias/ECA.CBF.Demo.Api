using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Entities.Exceptions;
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
    [Route("api/v{version:apiVersion}/tournments/{tournmentId}/matches/{matchId}/[controller]")]
    public class EventsController : BaseController
    {
        private readonly IMatchEventsProcess _eventProcess;

        public EventsController(IMatchEventsProcess eventProcess, ILogger<TeamsController> logger) : base(logger)
        {
            _eventProcess = eventProcess;
        }

        [HttpPut]
        [Route("start")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MatchEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetStartAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromQuery] DateTime? dateStart)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _eventProcess.SetStartAsync(tournmentId, matchId, dateStart), matchId);
        }

        [HttpPut]
        [Route("end")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MatchEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetEndAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromQuery] DateTime? dateEnd)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _eventProcess.SetEndAsync(tournmentId, matchId, dateEnd), matchId);
        }

        [HttpPut]
        [Route("break")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MatchEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetStartBreakAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromQuery] DateTime? date, [FromQuery] bool start, [FromQuery] bool end)
        {
            if (start)
            {
                return await PutDataAsync<int, InternalErrorException>(async () => await _eventProcess.SetStartBreakAsync(tournmentId, matchId, date), matchId);
            } else if (end)
            {
                return await PutDataAsync<int, InternalErrorException>(async () => await _eventProcess.SetEndBreakAsync(tournmentId, matchId, date), matchId);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("goals")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GoalEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListGoalsAsync([FromRoute] int matchId)
        {
            return await GetListAsync(async () => await _eventProcess.ListGoalsAsync(matchId));
        }

        [HttpPost]
        [Route("goals")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GoalEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertGoalAsync([FromBody] GoalEntity entity)
        {
            return await PostDataAsync<GoalEntity, int, InternalErrorException>(async () => await _eventProcess.InsertGoalsAsync(entity), entity);
        }

        [HttpPut]
        [Route("goals")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGoalAsync([FromBody] GoalEntity entity)
        {
            return await PutDataAsync<GoalEntity, InternalErrorException>(async () => await _eventProcess.UpdateGoalsAsync(entity), entity);
        }

        [HttpDelete]
        [Route("goals")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGoalAsync([FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _eventProcess.DeleteGoalAsync(id), id);
        }

        [HttpGet]
        [Route("cards")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CardEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListCardsAsync([FromRoute] int matchId)
        {
            return await GetListAsync(async () => await _eventProcess.ListCardAsync(matchId));
        }

        [HttpPost]
        [Route("cards")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CardEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertCardsAsync([FromBody] CardEntity entity)
        {
            return await PostDataAsync<CardEntity, int, InternalErrorException>(async () => await _eventProcess.InsertCardAsync(entity), entity);
        }

        [HttpPut]
        [Route("cards")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCardsAsync([FromBody] CardEntity entity)
        {
            return await PutDataAsync<CardEntity, InternalErrorException>(async () => await _eventProcess.UpdateCardAsync(entity), entity);
        }

        [HttpDelete]
        [Route("cards")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCardsAsync([FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _eventProcess.DeleteCardAsync(id), id);
        }
    }
}