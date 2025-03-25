using NBA.Players.Charts.PlayerDbContext.Entities;
using Newtonsoft.Json;

namespace NBA.Players.Charts.Models
{
    public class Team: BaseEntity
    {
        public string Name { get; init; }
        public string Logo { get; init; }
        public string Abbr { get; init; }
    }

    public class TeamStats
    {
        public Context context { get; set; }
        public Error error { get; set; }
        public Payload payload { get; set; }
    }

    public class Boxscore
    {
        public string attendance { get; set; }

        public int awayScore { get; set; }

        public string gameLength { get; set; }

        public int homeScore { get; set; }

        public int leadChanges { get; set; }

        public string officialsDisplayName1 { get; set; }

        public string officialsDisplayName2 { get; set; }

        public string officialsDisplayName3 { get; set; }

        public string period { get; set; }

        public string periodClock { get; set; }

        public string status { get; set; }

        public string statusDesc { get; set; }

        public string ties { get; set; }
    }

    public class Url
    {
        public string DisplayText { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class HomeTeam
    {
        public TeamProfile profile { get; set; }
        public Matchup matchup { get; set; }
    }

    public class AwayTeam
    {
        public TeamProfile profile { get; set; }
        public Matchup matchup { get; set; }
    }

    public class GameProfile
    {
        public string arenaLocation { get; set; }

        public string arenaName { get; set; }

        public string awayTeamId { get; set; }

        public string dateTimeEt { get; set; }

        public string gameId { get; set; }

        public string homeTeamId { get; set; }

        public string number { get; set; }

        public string scheduleCode { get; set; }

        public string seasonType { get; set; }

        public string sequence { get; set; }

        public string subType { get; set; }

        public string utcMillis { get; set; }

    }

    public class GameByTeam
    {
        public GameProfile profile { get; set; }
        public Boxscore boxscore { get; set; }
        public List<Url> urls { get; set; }
        public List<object> broadcasters { get; set; }
        public HomeTeam homeTeam { get; set; }
        public AwayTeam awayTeam { get; set; }
        public bool ifNecessary { get; set; }
        public string isHome { get; set; }
        public int oppTeamScore { get; set; }
        public int teamScore { get; set; }
        public string winOrLoss { get; set; }
    }

    public class MonthGroup
    {
        public List<GameByTeam> games { get; set; }
        public string name { get; set; }
        public int number { get; set; }
    }

    public class ScoreLast5Game
    {
        public int TeamScore { get; set; }
        public string TeamName { get; set; }
        public string Abbr { get; set; }// đối thủ
        public int AbbrScore { get; set;}
        public DateTime GameDate { get; set; }
        public string WinOrLose { get; set; }
    }
}
