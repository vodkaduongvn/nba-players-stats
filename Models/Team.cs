using NBA.Players.Charts.PlayerDbContext.Entities;

namespace NBA.Players.Charts.Models
{
    public class Team: BaseEntity
    {
        public string Name { get; init; }
        public string Logo { get; init; }
    }
}
