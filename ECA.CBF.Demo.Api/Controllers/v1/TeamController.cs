using ECA.CBF.Demo.Process.Interface;
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

        [HttpGet(Name = "GetAllTeams")]
        public async Task<IActionResult> ListAllTeams()
        {
            var result = await _teamProcess.ListTeamsAsync();
            _logger.LogInformation("Rota de times");
            return Ok(result);
        }
    }
}