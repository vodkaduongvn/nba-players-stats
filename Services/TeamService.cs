using Microsoft.EntityFrameworkCore;
using NBA.Players.Charts.Models;
using NBA.Players.Charts.PlayerDbContext;
using System.Net.Http;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NBA.Players.Charts.Services
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync();
        Task<List<ScoreLast5Game>> GetTeamStatsLast5GameByTeamIdAsync(Guid teamId);
    }

    public class TeamService: ITeamService
    {
        private readonly AppDbContext _dbContext;
        private readonly HttpClient _httpClient;

        public TeamService(AppDbContext dbContext, IHttpClientFactory httpClientFactory)
        {
            _dbContext = dbContext;
            _httpClient = httpClientFactory.CreateClient(); 
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            var dbTeams = await _dbContext.Teams.ToListAsync();

            return [.. dbTeams.Select(t => new Team
            {
                Id = t.Id,
                Name = t.Name,
                Logo = t.Logo?? string.Empty,
                Abbr = t.TeamCode
            }).OrderBy(x => x.Name)];
        }

        public async Task<List<ScoreLast5Game>> GetTeamStatsLast5GameByTeamIdAsync(Guid teamId)
        {
            var last5Games = new List<ScoreLast5Game>();

            var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId);

            string _url = $"https://vn.global.nba.com/stats2/team/schedule/last5.json?teamCode={team.TeamCode2}&locale=vn&countryCode=VN&state=SG";
            var response = await _httpClient.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var teamStats = JsonSerializer.Deserialize<TeamStats>(content);

                if (teamStats.payload != null)
                {
                    var monthGroup = teamStats.payload.monthGroups.FirstOrDefault();
                    foreach(var game in monthGroup.games)
                    {
                        if (team.TeamCode2 == game.homeTeam.profile.code)
                        {
                            last5Games.Add(new ScoreLast5Game
                            {
                                GameDate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(game.profile.utcMillis)).UtcDateTime,
                                TeamScore = game.boxscore.homeScore,
                                AbbrScore = game.boxscore.awayScore,
                                WinOrLose = game.winOrLoss,
                                TeamName = game.homeTeam.profile.name,
                                Abbr = game.awayTeam.profile.name,
                            });
                        }
                        else
                        {
                            last5Games.Add(new ScoreLast5Game
                            {
                                GameDate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(game.profile.utcMillis)).UtcDateTime,
                                TeamScore = game.boxscore.awayScore,
                                AbbrScore = game.boxscore.homeScore,
                                WinOrLose = game.winOrLoss,
                                TeamName= game.awayTeam.profile.name,
                                Abbr= game.homeTeam.profile.name,
                            });
                        }
                    }
                }
            }

            return last5Games;
        }
    }
}
