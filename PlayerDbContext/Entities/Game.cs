namespace NBA.Players.Charts.PlayerDbContext.Entities
{
    public class Game:BaseObject
    {
        public DateTime GameDate { get; set; }
        public string ExternalId { get; set; }
        public virtual ICollection<PlayerGame> PlayerGames { get; set; }
    }
}
