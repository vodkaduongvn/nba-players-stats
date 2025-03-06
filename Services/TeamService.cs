using Microsoft.EntityFrameworkCore;
using NBA.Players.Charts.Models;
using NBA.Players.Charts.PlayerDbContext;

namespace NBA.Players.Charts.Services
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync();
    }

    public class TeamService: ITeamService
    {
        private readonly AppDbContext _dbContext;
        public TeamService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
