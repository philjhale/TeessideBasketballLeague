using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Service.Extensions;
using Basketball.Service.Interfaces;

namespace Basketball.Service
{
    /// <summary>
    /// Encapsulates Season, League and TeamLeague
    /// </summary>
    public class CompetitionService : ICompetitionService
    {
        #region Init

        private readonly ICompetitionRepository competitionRepository;

        public CompetitionService(ICompetitionRepository competitionRepository)
        {
            this.competitionRepository       = competitionRepository;
        } 

        #endregion

        public void Commit()
        {
            competitionRepository.Commit();
        }

        #region Season
        public Season GetCurrentSeason()
        {
            return competitionRepository.GetCurrentSeason();
        }

        public List<Season> GetSeasons()
        {
            return competitionRepository.GetSeasons(null, null).ToList();
        }

        public List<Season> GetSeasons(Expression<Func<Season, bool>> filter = null,
            Func<IQueryable<Season>, IOrderedQueryable<Season>> orderBy = null)
        {
            return competitionRepository.GetSeasons(filter, orderBy).ToList();
        }

        public Season CreateNextSeason(Season currentSeason)
        {
            if (currentSeason != null && currentSeason.StartYear == DateTime.Today.Year)
                return null;
            else
                return new Season(DateTime.Today.Year, DateTime.Today.Year + 1);
        } 
        #endregion

        #region League
        public List<League> GetLeaguesForSeason(int seasonId)
        {
            return competitionRepository.GetLeaguesForSeason(seasonId).ToList();
        }

        public List<League> GetLeaguesForCurrentSeason()
        {
            return competitionRepository.GetLeaguesForSeason(GetCurrentSeason().Id).ToList();
        }

        // TODO Need to check this is cached in some way
        // TODO Could probably refactor this more
        public List<TeamLeague> GetStandingsForLeague(int leagueId)
        {
            List<TeamLeague> basicStandings = competitionRepository.GetBasicStandingsForLeague(leagueId).ToList();

            if(!basicStandings.TiesExist())
                return basicStandings;

            // For tied teams
                // Get the fixtures between those teams and check league points
                // If equal check points difference in those fixtures
                // If equal check points difference in all fixtures

            // Get league points where more than one team has the same number of points
            List<int> tiedLeaguePoints = (from b in basicStandings 
                    group b by b.PointsLeague into g
                    where g.Count() > 1
                    select g.Key).ToList();

            foreach (int tiedLeaguePointValue in tiedLeaguePoints)
            {
                // Get values where equal to points
                int position =  basicStandings.IndexOf(basicStandings.First(x => x.PointsLeague == tiedLeaguePointValue));
                List<TeamLeague> tiedTeamLeagues = basicStandings.Where(x => x.PointsLeague == tiedLeaguePointValue).ToList();
                
                // Get fixture details for the tied teams
                var fixturesBetweenTeams = competitionRepository.GetFixturesForTeamLeagues(tiedTeamLeagues.Select(x => x.Id).ToList());

                var tiedTeamDetails = new[]
                    {
                        new { 
                            Id                   = 0,
                            LeaguePoints         = 0,
                            ScoreDifference      = 0,
                            TotalScoreDifference = 0
                        }
                    }.ToList();
                tiedTeamDetails.Clear();
                // Loop through each team and work out league points, points difference in fixtures and do something with total points difference
                foreach (var tiedTeamLeague in tiedTeamLeagues)
                {
                    var teamFixtures = fixturesBetweenTeams.Where(f => f.IsHomeTeam(tiedTeamLeague) || f.IsAwayTeam(tiedTeamLeague)).ToList();

                    // Win points
                    int leaguePoints = tiedTeamLeague.GetLeaguePointsFromWins(teamFixtures); 

                    // Loss points assuming zero for forfeit
                    leaguePoints += tiedTeamLeague.GetLeaguePointsFromLosses(teamFixtures);

                    int forScore = tiedTeamLeague.GetPointsScoredFor(teamFixtures);

                    int againstScore = tiedTeamLeague.GetPointsScoredAgainst(teamFixtures);

                    tiedTeamDetails.Add(new { 
                            Id                   = tiedTeamLeague.Id,
                            LeaguePoints         = leaguePoints,
                            ScoreDifference      = forScore - againstScore,
                            TotalScoreDifference = tiedTeamLeague.PointsScoredDifference
                    });
                }

                // Now we have everything we need to sort the teams
                tiedTeamDetails = tiedTeamDetails.OrderByDescending(x => x.LeaguePoints).ThenByDescending(x => x.ScoreDifference).ThenByDescending(x => x.TotalScoreDifference).ToList();

                // Put tied teams in correct order                    
                List<TeamLeague> reorderedTeamsLeagues = new List<TeamLeague>();
                foreach (var tiedTeamDetail in tiedTeamDetails)
                {
                    reorderedTeamsLeagues.Add(tiedTeamLeagues.Single(x => x.Id == tiedTeamDetail.Id));
                }

                // Now we need to put the reorderd teams back into the right place
                basicStandings.RemoveAll(x => x.PointsLeague == tiedLeaguePointValue);
                basicStandings.InsertRange(position, reorderedTeamsLeagues);
            }

            return basicStandings;
        }

        #endregion

        #region TeamLeague
        
        public List<TeamLeague> GetTeamLeaguesForCurrentSeason()
        {
            return competitionRepository.GetTeamLeaguesForCurrentSeason().ToList();
        }

        public void UpdateTeamLeague(TeamLeague teamLeague)
        {
            competitionRepository.UpdateTeamLeague(teamLeague);
        }

        public List<int> GetTeamLeagueIdsForCurrentSeason()
        {
            return (from tl in GetTeamLeaguesForCurrentSeason() select tl.Id).ToList<int>();
        }

        public TeamLeague GetTeamLeague(int id)
        {
            return competitionRepository.GetTeamLeague(id);
        }

        public TeamLeague GetTeamLeagueByTeamIdAndLeagueId(int teamId, int leagueId)
        {
            return competitionRepository.GetTeamLeagueByTeamIdAndLeagueId(teamId, leagueId);
        }

        public TeamLeague GetTeamLeagueByTeamIdInCurrentSeason(int teamId)
        {
            return competitionRepository.GetTeamLeagueByTeamIdInCurrentSeason(teamId);
        }
        #endregion

        #region CRUD
        public League GetLeague(int id)
        {
            return competitionRepository.GetLeague(id);
        }

        // Will only ever insert
        public void SaveSeason(Season season)
        {
            competitionRepository.InsertSeason(season);
        }

        #endregion
    }
}
