
namespace NBA.Players.Charts.PlayerDbContext.Entities
{
    public class Player: BaseObject
    {
        public string Name { get; set; }=string.Empty;
        public string PlayerCode { get; set; } = string.Empty;

        // average points per game
        public double? PointsPerGame { get; set; }
        public double? AssistsPerGame { get; set; }
        public double? ReboundsPerGame { get; set; }
        public double? StealsPerGame { get; set; }
        public double? BlocksPerGame { get; set; }
        public double? TurnoversPerGame { get; set; }
        public string? Avatar { get; set; }

        public string ColorCode { get; set; } = string.Empty;

        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<PlayerGame> PlayerGames { get; set; }
    }
}
