namespace NBA.Players.Charts.Models
{
    public class GamesOnDate
    {
        public Context context { get; set; }
        public Error error { get; set; }
        public GameDatePayload payload { get; set; }
    }
  

    public class Date
    {
        public string dateMillis { get; set; }
        public string gameCount { get; set; }
        public List<GameOnDate> games { get; set; }
        }

    public class GameOnDate
    {
        public GameDateTeam awayTeam { get; set; }
        public GameDateTeam homeTeam { get; set; }
    }


    public class GameDateTeam
    {
        public Leader assistGameLeader { get; set; }
        public Matchup matchup { get; set; }
        public Leader pointGameLeader { get; set; }
        public TeamProfile profile { get; set; }
        public Leader reboundGameLeader { get; set; }
    }

    public class Leader
    {
        public PlayerProfile profile { get; set; }
        public StatTotal statTotal { get; set; }
    }

    public class Matchup
    {
        public string confRank { get; set; }
        public string divRank { get; set; }
        public string losses { get; set; }
        public string seriesText { get; set; }
        public string wins { get; set; }
    }
    public class GameDatePayload
    {
        public Date date { get; set; }
        public string nextAvailableDateMillis { get; set; }
        public Season season { get; set; }
        public string utcMillis { get; set; }
    }

    public class GameOnDateStats
    {
        public DateTime GameDate { get; set; }

        public List<TeamInfo> TeamInfo { get; set; } = [];
    }
    public class TeamInfo
    {
        public string Name { get; set; }
        public string AssistLeader { get; set; }
        public string PointLeader { get; set; }
        public string ReboundLeader { get; set; }
        public string Abbr { get; set; }
      
    }
}
