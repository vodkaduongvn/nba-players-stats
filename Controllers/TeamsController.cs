using Microsoft.AspNetCore.Mvc;
using NBA.Players.Charts.Models;
using NBA.Players.Charts.Services;

namespace NBA.Players.Charts.Controllers
{
    public class TeamsController : Controller
    {
        [HttpGet("teams", Name = "GetAllAsync")]
        public async Task<List<Team>> Get([FromServices] ITeamService teamService)
        {
            var teams = await teamService.GetTeamsAsync();
            return teams;
        }
    }
}
