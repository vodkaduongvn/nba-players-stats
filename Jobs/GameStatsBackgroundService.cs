using Microsoft.AspNetCore.SignalR;
using NBA.Players.Charts.Hubs;
using NBA.Players.Charts.Models;
using System.Text.Json;

namespace NBA.Players.Charts.Jobs
{
    public class GameStatsBackgroundService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly IHubContext<GameStatsHub> _hubContext;


        public GameStatsBackgroundService(
            IHttpClientFactory httpClientFactory, 
            IHubContext<GameStatsHub> hubContext)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(120);

            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var gameOnDateStats = new GameOnDateStats();

                string url = "https://vn.global.nba.com/stats2/scores/daily.json?locale=vn&tz=%2B7&countryCode=VN&state=SG";
                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                var gameStats = JsonSerializer.Deserialize<GamesOnDate>(content);
                if (gameStats.payload != null)
                {
                    gameOnDateStats.GameDate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(gameStats.payload.utcMillis)).UtcDateTime;
                    var games = gameStats.payload.date.games;
                    foreach (var game in games)
                    {
                        var homeTeam = game.homeTeam;
                        var awayTeam = game.awayTeam;
                        gameOnDateStats.TeamInfo.Add(new TeamInfo
                        {
                            Name = homeTeam.profile.code,
                            AssistLeader = homeTeam.assistGameLeader?.profile.code,
                            PointLeader = homeTeam.pointGameLeader?.profile.code,
                            ReboundLeader = homeTeam.reboundGameLeader?.profile.code,
                            Abbr = homeTeam.profile.abbr,
                            Position = homeTeam.pointGameLeader?.profile.position,
                            Mins = homeTeam.pointGameLeader?.statTotal.mins,
                            Points = homeTeam.pointGameLeader?.statTotal?.points,
                        });
                        gameOnDateStats.TeamInfo.Add(new TeamInfo
                        {
                            Name = awayTeam.profile.code,
                            AssistLeader = awayTeam.assistGameLeader?.profile.code,
                            PointLeader = awayTeam.pointGameLeader?.profile.code,
                            ReboundLeader = awayTeam.reboundGameLeader?.profile.code,
                           Abbr = awayTeam.profile.abbr,
                            Position = awayTeam.pointGameLeader?.profile.position,
                            Mins = awayTeam.pointGameLeader?.statTotal.mins,
                            Points = awayTeam.pointGameLeader?.statTotal?.points,
                        });
                    }

                }

                await _hubContext.Clients.All.SendAsync("ReceiveGameStats", gameOnDateStats);

                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }
    }
}
