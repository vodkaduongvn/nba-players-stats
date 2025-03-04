using NBA.Players.Charts.Models;
using NBA.Players.Charts.PlayerDbContext.Entities;
using NBA.Players.Charts.PlayerDbContext;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace NBA.Players.Charts.Jobs
{
    public class PlayerStatsBackgroundService : BackgroundService
    {
        private readonly ILogger<PlayerStatsBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _httpClientFactory;

        public PlayerStatsBackgroundService(ILogger<PlayerStatsBackgroundService> logger, IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("PlayerStatsBackgroundService running at: {time}", DateTimeOffset.Now);

                //await Task.Delay(TimeSpan.FromHours(1), stoppingToken);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    await InsertPlayerStatsAsync(dbContext);
                }
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task InsertPlayerStatsAsync(AppDbContext dbContext)
        {
            var players = dbContext.Players.ToList();
            var httpClient = _httpClientFactory.CreateClient();

            foreach (var player in players)
            {
                string _url = $"https://vn.global.nba.com/stats2/player/stats.json?playerCode={player.PlayerCode}&ds=profile&locale=vn";
                var response = await httpClient.GetAsync(_url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var playerStats = JsonSerializer.Deserialize<PlayerStats>(content);
                    if (playerStats.payload != null)
                    {
                        var pointAvg = playerStats.payload.player.stats.regularSeasonStat.playerTeams
                          .LastOrDefault()?.statAverage.pointsPg ?? 0;
                        // update average points per game
                        player.PointsPerGame = pointAvg;
                        await dbContext.SaveChangesAsync();

                        // add player stats in last 10 games
                        var last10Games = playerStats.payload.player.stats.seasonGames.Take(10);
                        foreach (var lastGame in last10Games)
                        {
                            var game = await dbContext.Games.FirstOrDefaultAsync(x => x.ExternalId == lastGame.profile.gameId);
                            if (game == null)
                            {
                                game = new Game
                                {
                                    GameDate = lastGame.GameDate,
                                    ExternalId = lastGame.profile.gameId,
                                    IsActive = true,
                                    CreatedById = "system",
                                    CreatedByUserName = "system",
                                    DateCreated = DateTime.Now,
                                    ModifiedById = "system",
                                    ModifiedByUserName = "system",
                                    DateModified = DateTime.Now
                                };
                                dbContext.Games.Add(game);
                                await dbContext.SaveChangesAsync();
                            }
                            var playerGames = await dbContext.PlayerGames.Where(x => x.Game.ExternalId == lastGame.profile.gameId).ToListAsync();
                            var newPlayerGame = new PlayerGame();

                            if (playerGames != null && playerGames.Count != 0)
                            {
                                if (!playerGames.Exists(x => x.PlayerId == player.Id))
                                {
                                    newPlayerGame = new PlayerGame
                                    {
                                        PlayerId = player.Id,
                                        GameId = playerGames.First().GameId,
                                        Points = lastGame.statTotal.points ?? 0,
                                        CreatedById = "system",
                                        CreatedByUserName = "system",
                                        DateCreated = DateTime.Now,
                                        ModifiedById = "system",
                                        ModifiedByUserName = "system",
                                        DateModified = DateTime.Now
                                    };
                                    dbContext.PlayerGames.Add(newPlayerGame);
                                    await dbContext.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                newPlayerGame = new PlayerGame
                                {
                                    PlayerId = player.Id,
                                    GameId = game.Id,
                                    Points = lastGame.statTotal.points ?? 0,
                                    CreatedById = "system",
                                    CreatedByUserName = "system",
                                    DateCreated = DateTime.Now,
                                    ModifiedById = "system",
                                    ModifiedByUserName = "system",
                                    DateModified = DateTime.Now
                                };
                                dbContext.PlayerGames.Add(newPlayerGame);
                                await dbContext.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}
