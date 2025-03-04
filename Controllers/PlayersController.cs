using Microsoft.AspNetCore.Mvc;
using NBA.Players.Charts.Models;
using NBA.Players.Charts.Services;

namespace NBA.Players.Charts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        
        [HttpGet("players-stats/{teamId:guid}", Name = "GetPlayersStatByTeam")]
        public async Task<List<PlayerStatsPerLast10Games>> Get([FromRoute] Guid teamId, [FromServices] IPlayerService playerService)
        {
            var statsLast10Games =  await playerService.GetPlayerStatsLast10GameByTeamIdAsync(teamId);
            //var statsLast10Games = await playerService.GetDBStatsLast10GameByTeamIdAsync(teamId);

            return statsLast10Games;
        }
    }
}
