using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBA.Players.Charts.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamCode2Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamCode2",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
        UPDATE Teams SET TeamCode2 = CASE 
            WHEN TeamCode = 'LAC' THEN 'clippers'
            WHEN TeamCode = 'TOR' THEN 'raptors'
            WHEN TeamCode = 'MIA' THEN 'heat'
            WHEN TeamCode = 'WAS' THEN 'wizards'
            WHEN TeamCode = 'UTA' THEN 'jazz'
            WHEN TeamCode = 'CHA' THEN 'hornets'
            WHEN TeamCode = 'DEN' THEN 'nuggets'
            WHEN TeamCode = 'ATL' THEN 'hawks'
            WHEN TeamCode = 'POR' THEN 'blazers'
            WHEN TeamCode = 'ORL' THEN 'magic'
            WHEN TeamCode = 'NYK' THEN 'knicks'
            WHEN TeamCode = 'NOP' THEN 'pelicans'
            WHEN TeamCode = 'DET' THEN 'pistons'
            WHEN TeamCode = 'BOS' THEN 'celtics'
            WHEN TeamCode = 'PHI' THEN 'sixers'
            WHEN TeamCode = 'PHX' THEN 'suns'
            WHEN TeamCode = 'OKC' THEN 'thunder'
            WHEN TeamCode = 'MIN' THEN 'wolves'
            WHEN TeamCode = 'DAL' THEN 'mavericks'
            WHEN TeamCode = 'CLE' THEN 'cavaliers'
            WHEN TeamCode = 'GSW' THEN 'warriors'
            WHEN TeamCode = 'BKN' THEN 'nets'
            WHEN TeamCode = 'IND' THEN 'pacers'
            WHEN TeamCode = 'MEM' THEN 'grizzlies'
            WHEN TeamCode = 'HOU' THEN 'rockets'
            WHEN TeamCode = 'LAL' THEN 'lakers'
            WHEN TeamCode = 'SAC' THEN 'kings'
            WHEN TeamCode = 'SAS' THEN 'spurs'
            WHEN TeamCode = 'CHI' THEN 'bulls'
            WHEN TeamCode = 'MIL' THEN 'buck'
            ELSE ''
        END
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamCode2",
                table: "Teams");
        }
    }
}
