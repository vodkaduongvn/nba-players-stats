using NBA.Players.Charts.Models;
using Microsoft.AspNetCore.SignalR;

namespace NBA.Players.Charts.Hubs
{
    public class GameStatsHub : Hub
    {
        public async Task SendGameStats(GameOnDateStats gameStats)
        {
            await Clients.All.SendAsync("ReceiveGameStats", gameStats);
        }
    }
}
