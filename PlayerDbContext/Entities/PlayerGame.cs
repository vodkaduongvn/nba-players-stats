namespace NBA.Players.Charts.PlayerDbContext.Entities
{
    public class PlayerGame: BaseObject
    {
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public int Points { get; set; }
    }
}
