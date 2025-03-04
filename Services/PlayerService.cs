using Microsoft.EntityFrameworkCore;
using NBA.Players.Charts.Models;
using NBA.Players.Charts.PlayerDbContext;
using System.Text.Json;

namespace NBA.Players.Charts.Services
{
    public interface IPlayerService
    {
        Task<List<PlayerStatsPerLast10Games>> GetPlayerStatsLast10GameByTeamIdAsync(Guid teamId);
        Task<List<PlayerStatsPerLast10Games>> GetDBStatsLast10GameByTeamIdAsync(Guid teamId);
    }
    public class PlayerService: IPlayerService
    {
        private readonly AppDbContext _dbContext;
        private readonly HttpClient _httpClient;

        public PlayerService(AppDbContext dbContext, IHttpClientFactory httpClientFactory)
        {
            _dbContext = dbContext;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<PlayerStatsPerLast10Games>> GetPlayerStatsLast10GameByTeamIdAsync(Guid teamId)
        {
            var last10Games = new List<PlayerStatsPerLast10Games>();

            var allGameDates = new HashSet<DateTime>();

            var players = await _dbContext.Players.Where(x=>x.TeamId == teamId).ToListAsync();
            foreach (var player in players)
            {
                string _url = $"https://vn.global.nba.com/stats2/player/stats.json?playerCode={player.PlayerCode}&ds=profile&locale=vn";
                var response = await _httpClient.GetAsync(_url);
                var playerGamesDict = new List<PointsPerLast10Games>();
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var playerStats = JsonSerializer.Deserialize<PlayerStats>(content);

                    if (playerStats.payload != null)
                    {
                        playerGamesDict.AddRange(
                            playerStats.payload.player.stats.seasonGames
                                .OrderByDescending(x => x.GameDate)
                                .Take(10)
                                .Select(x => new PointsPerLast10Games
                                {
                                    GameDate = x.GameDate,
                                    Points = x.statTotal.points,
                                    Rebounds = x.statTotal.rebs,
                                    Assists = x.statTotal.assists,
                                    WinOrLoss = x.profile.winOrLoss,
                                    TeamScore = x.profile.teamScore,
                                    OppTeamName = x.profile.oppTeamProfile.name
                                })
                                );

                        var pointsAvg = playerStats.payload.player.stats.regularSeasonStat.playerTeams
                          .LastOrDefault()?.statAverage.pointsPg ?? 0;

                        var reboundsAvg = playerStats.payload.player.stats.regularSeasonStat.playerTeams
                          .LastOrDefault()?.statAverage.rebsPg ?? 0;

                        var assistsAvg = playerStats.payload.player.stats.regularSeasonStat.playerTeams
                            .LastOrDefault()?.statAverage.assistsPg ?? 0;

                        var teamName = playerStats.payload.player.stats.regularSeasonStat.playerTeams
                          .LastOrDefault()?.profile?.name ?? string.Empty;

                        var gameNumber = playerStats.payload.player.stats.regularSeasonStat.playerTeams
                            .LastOrDefault()?.statAverage.games ?? 0;

                        last10Games.Add(new PlayerStatsPerLast10Games
                        {
                            TeamName = teamName,
                            PlayerCode = player.PlayerCode,
                            ColorCode = players.First(x => x.PlayerCode == player.PlayerCode).ColorCode,
                            PointsAvg = pointsAvg,
                            ReboundsAvg = reboundsAvg,
                            AssistsAvg = assistsAvg,
                            GameNumber = gameNumber,
                            PointsPerLast10Games = playerGamesDict.OrderBy(x => x.GameDate).ToList()
                        });
                    }
                }
            }

            return last10Games;
        }

        public async Task<List<PlayerStatsPerLast10Games>> GetDBStatsLast10GameByTeamIdAsync(Guid teamId)
        {
            var last10Games = new List<PlayerStatsPerLast10Games>();

            var players = await _dbContext.Players
                .Include(p => p.Team)
                .Where(x => x.TeamId == teamId)
                .ToListAsync();

            foreach (var player in players)
            {
                var playerGames = await _dbContext.PlayerGames
                    .Include(x=>x.Player)
                    .Include(x => x.Game)
                    .Where(x => x.PlayerId == player.Id)
                    .OrderByDescending(x => x.Game.GameDate)
                    .Take(10)
                    .ToListAsync();

                var playerGamesDict = playerGames
                    .Select(x => new PointsPerLast10Games
                    {
                        GameDate = x.Game.GameDate,
                        Points = x.Points
                    })
                    .OrderBy(x => x.GameDate)
                    .ToList();

                //var pointAvg = playerGamesDict.Average(x => x.Points);

                last10Games.Add(new PlayerStatsPerLast10Games
                {
                    TeamName = player.Team.Name,
                    PlayerCode = player.PlayerCode,
                    ColorCode = player.ColorCode,
                    PointsAvg = player.PointsPerGame ?? 0,
                    AssistsAvg = player.AssistsPerGame??0,
                    ReboundsAvg = player.ReboundsPerGame??0,
                    PointsPerLast10Games = playerGamesDict
                });
            }
            return last10Games;
        }
    }
}
