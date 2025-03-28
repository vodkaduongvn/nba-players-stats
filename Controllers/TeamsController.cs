using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NBA.Players.Charts.Models;
using NBA.Players.Charts.Services;

namespace NBA.Players.Charts.Controllers
{
    //[Authorize]
    public class TeamsController : Controller
    {
        [HttpGet("teams", Name = "GetAllAsync")]
        public async Task<List<Team>> Get([FromServices] ITeamService teamService)
        {
            var teams = await teamService.GetTeamsAsync();
            return teams;
        }

        [HttpGet("team-stats/{teamId:guid}", Name = "GetTeamStatById")]
        public async Task<TeamGameStats> Get([FromRoute] Guid teamId, [FromServices] ITeamService teamService)
        {
            var last5Games = await teamService.GetTeamStatsLast5GameByTeamIdAsync(teamId);
            return last5Games;
        }
    }
}
