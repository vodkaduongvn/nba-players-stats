namespace NBA.Players.Charts.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class PlayerStats
    {
        public Context context { get; set; }
        public Error error { get; set; }
        public Payload payload { get; set; }
    }

    public class AllStarStat
    {
        public List<object> playerTeams { get; set; }
    }

    public class Context
    {
        public User user { get; set; }
        public Device device { get; set; }
    }

    public class CurrentSeasonTypePlayerTeamStat
    {
        public Profile profile { get; set; }
        public StatAverage statAverage { get; set; }
        public StatTotal statTotal { get; set; }
        public object round { get; set; }
        public object roundText { get; set; }
    }

    public class CurrentSeasonTypeStat
    {
        public Season season { get; set; }
        public List<CurrentSeasonTypePlayerTeamStat> currentSeasonTypePlayerTeamStats { get; set; }
    }

    public class Device
    {
        public object clazz { get; set; }
    }

    public class Error
    {
        public object detail { get; set; }
        public string isError { get; set; }
        public object message { get; set; }
    }

    public class Last5Games
    {
        public StatAverage statAverage { get; set; }
        public StatTotal statTotal { get; set; }
    }

    public class League
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class LeagueSeasonAverage
    {
        public double? assistsPg { get; set; }
        public double? blocksPg { get; set; }
        public double? defRebsPg { get; set; }
        public double? efficiency { get; set; }
        public double? fgaPg { get; set; }
        public double? fgmPg { get; set; }
        public double? fgpct { get; set; }
        public double? foulsPg { get; set; }
        public double? ftaPg { get; set; }
        public double? ftmPg { get; set; }
        public double? ftpct { get; set; }
        public int games { get; set; }
        public object gamesStarted { get; set; }
        public double? minsPg { get; set; }
        public double? offRebsPg { get; set; }
        public double? pointsPg { get; set; }
        public double? rebsPg { get; set; }
        public double? stealsPg { get; set; }
        public double? tpaPg { get; set; }
        public double? tpmPg { get; set; }
        public double? tppct { get; set; }
        public double? turnoversPg { get; set; }
    }

    public class OppTeamProfile
    {
        public string abbr { get; set; }
        public string city { get; set; }
        public string cityEn { get; set; }
        public string code { get; set; }
        public string conference { get; set; }
        public string displayAbbr { get; set; }
        public string displayConference { get; set; }
        public string division { get; set; }
        public string id { get; set; }
        public bool isAllStarTeam { get; set; }
        public bool isLeagueTeam { get; set; }
        public string leagueId { get; set; }
        public string name { get; set; }
        public string nameEn { get; set; }
    }

    public class Payload
    {
        public League league { get; set; }
        public Season season { get; set; }
        public LeagueSeasonAverage leagueSeasonAverage { get; set; }
        public Player player { get; set; }
    }

    public class Player
    {
        public PlayerProfile playerProfile { get; set; }
        public TeamProfile teamProfile { get; set; }
        public Stats stats { get; set; }
    }

    public class PlayerProfile
    {
        public string code { get; set; }
        public string country { get; set; }
        public string countryEn { get; set; }
        public string displayAffiliation { get; set; }
        public string displayName { get; set; }
        public string displayNameEn { get; set; }
        public string dob { get; set; }
        public string draftYear { get; set; }
        public string experience { get; set; }
        public string firstInitial { get; set; }
        public string firstName { get; set; }
        public string firstNameEn { get; set; }
        public string height { get; set; }
        public string jerseyNo { get; set; }
        public string lastName { get; set; }
        public string lastNameEn { get; set; }
        public string leagueId { get; set; }
        public string playerId { get; set; }
        public string position { get; set; }
        public string schoolType { get; set; }
        public string weight { get; set; }
    }

    public class PlayerSplit
    {
        public List<Split> splits { get; set; }
        public Last5Games last5Games { get; set; }
    }

    public class PlayerTeam
    {
        public Profile profile { get; set; }
        public StatAverage statAverage { get; set; }
        public StatTotal statTotal { get; set; }
        public string season { get; set; }
        public string seasonDisplay { get; set; }
    }

    public class PlayinStat
    {
        public List<PlayerTeam> playerTeams { get; set; }
    }

    public class PlayoffCareerStat
    {
        public StatAverage? statAverage { get; set; }
        public StatTotal? statTotal { get; set; }
    }

    public class PlayoffStat
    {
        public List<PlayerTeam> playerTeams { get; set; }
    }

    public class Profile
    {
        public string abbr { get; set; }
        public string city { get; set; }
        public string cityEn { get; set; }
        public string code { get; set; }
        public string conference { get; set; }
        public string displayAbbr { get; set; }
        public string displayConference { get; set; }
        public string division { get; set; }
        public string id { get; set; }
        public bool isAllStarTeam { get; set; }
        public bool isLeagueTeam { get; set; }
        public string leagueId { get; set; }
        public string name { get; set; }
        public string nameEn { get; set; }
        public TeamProfile teamProfile { get; set; }
        public OppTeamProfile oppTeamProfile { get; set; }
        public string gameId { get; set; }
        public string isHome { get; set; }
        public int oppTeamScore { get; set; }
        public int teamScore { get; set; }
        public string utcMillis { get; set; }
        public string winOrLoss { get; set; }
    }

    public class RegularSeasonCareerStat
    {
        public StatAverage statAverage { get; set; }
        public StatTotal statTotal { get; set; }
    }

    public class RegularSeasonStat
    {
        public List<PlayerTeam> playerTeams { get; set; }
    }

    public class Root
    {
        public Context context { get; set; }
        public Error error { get; set; }
        public Payload payload { get; set; }
        public string timestamp { get; set; }
    }

    public class Season
    {
        public string isCurrent { get; set; }
        public int rosterSeasonType { get; set; }
        public string rosterSeasonYear { get; set; }
        public string rosterSeasonYearDisplay { get; set; }
        public int scheduleSeasonType { get; set; }
        public string scheduleSeasonYear { get; set; }
        public string scheduleYearDisplay { get; set; }
        public int statsSeasonType { get; set; }
        public string statsSeasonYear { get; set; }
        public string statsSeasonYearDisplay { get; set; }
        public string year { get; set; }
        public string yearDisplay { get; set; }
        public string type { get; set; }
        public string typeDisplay { get; set; }
    }

    public class SeasonGame
    {
        public Profile profile { get; set; }
        public StatTotal statTotal { get; set; }

        public DateTime GameDate => DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(profile.utcMillis)).UtcDateTime;

    }

    public class Split
    {
        public StatAverage statAverage { get; set; }
        public StatTotal statTotal { get; set; }
        public string displayName { get; set; }
        public string name { get; set; }
    }

    public class StatAverage
    {
        public double? assistsPg { get; set; }
        public double? blocksPg { get; set; }
        public double? defRebsPg { get; set; }
        public double? efficiency { get; set; }
        public double? fgaPg { get; set; }
        public double? fgmPg { get; set; }
        public double? fgpct { get; set; }
        public double? foulsPg { get; set; }
        public double? ftaPg { get; set; }
        public double? ftmPg { get; set; }
        public double? ftpct { get; set; }
        public int? games { get; set; }
        public int? gamesStarted { get; set; }
        public double? minsPg { get; set; }
        public double? offRebsPg { get; set; }
        public double? pointsPg { get; set; }
        public double? rebsPg { get; set; }
        public double? stealsPg { get; set; }
        public double? tpaPg { get; set; }
        public double? tpmPg { get; set; }
        public double? tppct { get; set; }
        public double? turnoversPg { get; set; }
    }

    public class Stats
    {
        public CurrentSeasonTypeStat currentSeasonTypeStat { get; set; }
        public RegularSeasonStat regularSeasonStat { get; set; }
        public PlayoffStat playoffStat { get; set; }
        public PlayinStat playinStat { get; set; }
        public AllStarStat allStarStat { get; set; }
        public List<SeasonGame> seasonGames { get; set; }
        public RegularSeasonCareerStat regularSeasonCareerStat { get; set; }
        public PlayoffCareerStat? playoffCareerStat { get; set; }
        public PlayerSplit playerSplit { get; set; }
    }

    public class StatTotal
    {
        public int? assists { get; set; }
        public int? blocks { get; set; }
        public int? defRebs { get; set; }
        public double? efficiency { get; set; }
        public int? fga { get; set; }
        public int? fgm { get; set; }
        public double? fgpct { get; set; }
        public int? fouls { get; set; }
        public int? fta { get; set; }
        public int? ftm { get; set; }
        public double? ftpct { get; set; }
        public int? mins { get; set; }
        public int? offRebs { get; set; }
        public int? points { get; set; }
        public int? rebs { get; set; }
        public int? secs { get; set; }
        public int? steals { get; set; }
        public object technicalFouls { get; set; }
        public int? tpa { get; set; }
        public int? tpm { get; set; }
        public double? tppct { get; set; }
        public int? turnovers { get; set; }
    }

    public class TeamProfile
    {
        public string abbr { get; set; }
        public string city { get; set; }
        public string cityEn { get; set; }
        public string code { get; set; }
        public string conference { get; set; }
        public string displayAbbr { get; set; }
        public string displayConference { get; set; }
        public string division { get; set; }
        public string id { get; set; }
        public bool isAllStarTeam { get; set; }
        public bool isLeagueTeam { get; set; }
        public string leagueId { get; set; }
        public string name { get; set; }
        public string nameEn { get; set; }
    }

    public class User
    {
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string locale { get; set; }
        public string timeZone { get; set; }
        public string timeZoneCity { get; set; }
    }


}
