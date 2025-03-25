namespace NBA.Players.Charts.PlayerDbContext.Entities
{
    public class Team : BaseObject
    {
        public string Name { get; set; } = string.Empty;
        public string TeamCode { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string TeamCode2 {  get; set; }=string.Empty;

        public virtual ICollection<Player>? Players { get; set; }
    }
}
