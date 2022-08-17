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
        public async Task<IActionResult> InsertGoalAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromBody] GoalEntity entity)
        {
            return await PostDataAsync<GoalEntity, int, InternalErrorException>(async () => await _eventProcess.InsertGoalsAsync(tournmentId, matchId, entity), entity);
        }

        [HttpPut]
        [Route("goals")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGoalAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromBody] GoalEntity entity)
        {
            return await PutDataAsync<GoalEntity, InternalErrorException>(async () => await _eventProcess.UpdateGoalsAsync(tournmentId, matchId, entity), entity);
        }

        [HttpDelete]
        [Route("goals")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGoalAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _eventProcess.DeleteGoalAsync(tournmentId, matchId, id), id);
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
        public async Task<IActionResult> InsertCardAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromBody] CardEntity entity)
        {
            return await PostDataAsync<CardEntity, int, InternalErrorException>(async () => await _eventProcess.InsertCardAsync(tournmentId, matchId, entity), entity);
        }

        [HttpPut]
        [Route("cards")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCardAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromBody] CardEntity entity)
        {
            return await PutDataAsync<CardEntity, InternalErrorException>(async () => await _eventProcess.UpdateCardAsync(tournmentId, matchId, entity), entity);
        }

        [HttpDelete]
        [Route("cards")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCardAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _eventProcess.DeleteCardAsync(tournmentId, matchId, id), id);
        }

        [HttpGet]
        [Route("replacements")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ReplacementEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListReplacementsAsync([FromRoute] int matchId)
        {
            return await GetListAsync(async () => await _eventProcess.ListReplacementAsync(matchId));
        }

        [HttpPost]
        [Route("replacements")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ReplacementEntity>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertReplacementAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromBody] ReplacementEntity entity)
        {
            return await PostDataAsync<ReplacementEntity, int, InternalErrorException>(async () => await _eventProcess.InsertReplacementAsync(tournmentId, matchId, entity), entity);
        }

        [HttpPut]
        [Route("replacements")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateReplacementAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromBody] ReplacementEntity entity)
        {
            return await PutDataAsync<ReplacementEntity, InternalErrorException>(async () => await _eventProcess.UpdateReplacementAsync(tournmentId, matchId, entity), entity);
        }

        [HttpDelete]
        [Route("replacements")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReplacementAsync([FromRoute] int tournmentId, [FromRoute] int matchId, [FromQuery] int id)
        {
            return await PutDataAsync<int, InternalErrorException>(async () => await _eventProcess.DeleteReplacementAsync(tournmentId, matchId, id), id);
        }
    }
}