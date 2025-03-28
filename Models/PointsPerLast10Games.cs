namespace NBA.Players.Charts.Models
{
    public class PlayerStatsPerLast10Games
    {
        public string OppTeamNaem { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public string PlayerCode { get; set; } = string.Empty;
        public string ColorCode { get; set; } = string.Empty;
        public double PointsAvg { get; set; }
        public double ReboundsAvg { get; set; }
        public double AssistsAvg { get; set; }
        public int GameNumber { get; set; }
        public double Mins { get; set; }
        public List<PointsPerLast10Games>? PointsPerLast10Games { get; set; }
    }

    public class PointsPerLast10Games
    {
        public DateTime GameDate { get; set; }
        public int? Points { get; set; }
        public int? Rebounds { get; set; }
        public int? Assists { get; set; }
        public string WinOrLoss { get; set; }
        public int TeamScore { get; set; }
        public string OppTeamName { get; set; }
        public int OppTeamScore { get; set; }
        public int? Mins { get; set; }
    }
}
