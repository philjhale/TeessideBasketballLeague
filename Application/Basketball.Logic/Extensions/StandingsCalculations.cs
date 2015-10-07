using System.Collections.Generic;
using System.Linq;
using Basketball.Common.Mapping;
using Basketball.Domain.Entities;

namespace Basketball.Service.Extensions
{
    public static class StandingsCalculations
    {
        public const int LeaguePointsWin      = 3;
        public const int LeaguePointsLoss     = 1;
        public const int ForfeitWinScore      = 20;
        public const int ForfeitLossScore     = 0;

        public static bool TiesExist(this List<TeamLeague> standings)
        {
            return (from tl1 in standings 
                    from tl2 in standings 
                    where tl1.Id != tl2.Id && tl1.PointsLeague == tl2.PointsLeague 
                    select tl1).Any();
        }

        public static int GetHomeWins(this TeamLeague teamLeague, List<Fixture> teamFixtures)
        {
            return teamFixtures.Count(x => (x.IsHomeTeam(teamLeague) && x.IsHomeWin()));
        }

        public static int GetAwayWins(this TeamLeague teamLeague, List<Fixture> teamFixtures)
        {
            return teamFixtures.Count(x => (x.IsAwayTeam(teamLeague) && x.IsAwayWin()));
        }

        public static int GetHomeLosses(this TeamLeague teamLeague, List<Fixture> teamFixtures, bool includeForfeitedFixtures = true)
        {
            if(includeForfeitedFixtures)
                return teamFixtures.Count(f => (f.IsHomeTeam(teamLeague) && (!f.IsHomeWin() || f.IsHomeForfeit())));
            else
                return teamFixtures.Count(f => (f.IsHomeTeam(teamLeague) && !f.IsHomeWin() && !f.IsHomeForfeit()));
        }

        public static int GetAwayLosses(this TeamLeague teamLeague, List<Fixture> teamFixtures, bool includeForfeitedFixtures = true)
        {
            if(includeForfeitedFixtures)
                return teamFixtures.Count(f => (f.IsAwayTeam(teamLeague) && (!f.IsAwayWin() || f.IsAwayForfeit())));
            else
                return teamFixtures.Count(f => (f.IsAwayTeam(teamLeague) && !f.IsAwayWin() && !f.IsAwayForfeit()));
        }

        public static int GetTotalWins(this TeamLeague teamLeague, List<Fixture> teamFixtures)
        {
            return teamLeague.GetHomeWins(teamFixtures) + teamLeague.GetAwayWins(teamFixtures);
        }

        public static int GetTotalLosses(this TeamLeague teamLeague, List<Fixture> teamFixtures, bool includeForfeitedFixtures = true)
        {
            return teamLeague.GetHomeLosses(teamFixtures, includeForfeitedFixtures) + teamLeague.GetAwayLosses(teamFixtures, includeForfeitedFixtures);
        }


        public static int GetLeaguePointsFromWins(this TeamLeague teamLeague, List<Fixture> teamFixtures)
        {
            return teamLeague.GetTotalWins(teamFixtures) * LeaguePointsWin;
        }
        
        public static int GetLeaguePointsFromLosses(this TeamLeague teamLeague, List<Fixture> teamFixtures)
        {
            return teamLeague.GetTotalLosses(teamFixtures, false) * LeaguePointsLoss;
        }

        public static int GetPointsScoredFor(this TeamLeague teamLeague, List<Fixture> teamFixtures)
        {
            return teamFixtures.Where(f => f.IsHomeTeam(teamLeague)).Sum(x => x.HomeTeamScore).Value +
                   teamFixtures.Where(f => f.IsAwayTeam(teamLeague)).Sum(x => x.AwayTeamScore).Value;
        }
        
        public static int GetPointsScoredAgainst(this TeamLeague teamLeague, List<Fixture> teamFixtures)
        {
            return teamFixtures.Where(f => f.IsHomeTeam(teamLeague)).Sum(x => x.AwayTeamScore).Value +
                   teamFixtures.Where(f => f.IsAwayTeam(teamLeague)).Sum(x => x.HomeTeamScore).Value;
        }

        public static int GetGamesPlayed(this TeamLeague teamLeague, List<Fixture> fixtures)
        {
            return fixtures.Count(f => f.IsPlayed.YesNoToBool() && (f.IsHomeTeam(teamLeague) || f.IsAwayTeam(teamLeague)));
        }

        public static int GetForfeitedGames(this TeamLeague teamLeague, List<Fixture> fixtures)
        {
            return fixtures.Count(f => (f.IsHomeTeam(teamLeague) && f.IsHomeForfeit()) || (f.IsAwayTeam(teamLeague) && f.IsAwayForfeit()));
        }
    }
}
