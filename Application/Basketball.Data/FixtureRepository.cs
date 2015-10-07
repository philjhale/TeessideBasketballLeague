using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

using Basketball.Data.Connection;
using Basketball.Domain.Entities;
using Basketball.Data.Interfaces;
using Basketball.Common.BaseTypes;

namespace Basketball.Data
{
    public partial class FixtureRepository : BaseRepository<Fixture>, IFixtureRepository
    {
        public IQueryable<Fixture> GetTeamHomeFixturesForTeamLeagues(int teamId, List<int> teamLeagueIds)
        {
            if (teamId <= 0)
                throw new InvalidOperationException("Parameter teamId must be greater than or equal to zero");

            return (from f in Get()
                    where teamLeagueIds.Contains(f.HomeTeamLeague.Id)
                    && f.HomeTeamLeague.Team.Id == teamId
                    orderby f.FixtureDate
                    select f);
        }

        /// <param name="teamId">0 or less for all teams</param>
        /// <param name="teamLeagueIds"></param>
        /// <param name="isPlayed">Y/N - Null or blank for all</param>
        /// <param name="isLeague"></param>
        /// <param name="isCup"></param>
        /// <param name="leagueOrCupId">Only required if filtered by league or cup</param>
        public IQueryable<Fixture> GetFixturesForTeamLeaguesFilter(int teamId, List<int> teamLeagueIds, string isPlayed, bool isLeague, bool isCup, int? leagueOrCupId)
        {
            IQueryable<Fixture> fixtures = GetFixturesForTeamLeagues(teamLeagueIds);
            fixtures = FilterFixturesByTeamId(teamId, fixtures);
            fixtures = FilterFixturesByIsPlayed(isPlayed, fixtures);
            
            if(isLeague)
                fixtures = from f in fixtures where !f.IsCupFixture && f.HomeTeamLeague.League.Id == leagueOrCupId.Value select f;
            else if(isCup)
                fixtures = from f in fixtures where f.IsCupFixture && f.Cup.Id == leagueOrCupId.Value select f;

            return fixtures;
        }

        /// <param name="teamId">0 or less for all teams</param>
        /// <param name="teamLeagueIds"></param>
        /// <param name="isPlayed">Y/N - Null or blank for all</param>
        /// <param name="isCup">Y/N - Null or blank for all</param>
        /// <returns></returns>
        public IQueryable<Fixture> GetFixturesForTeamLeaguesFilter(int teamId, List<int> teamLeagueIds, string isPlayed, bool? isCup)
        {
            IQueryable<Fixture> fixtures = GetFixturesForTeamLeagues(teamLeagueIds);

            fixtures = this.FilterFixturesByTeamId(teamId, fixtures);

            fixtures = this.FilterFixturesByIsPlayed(isPlayed, fixtures);
            if (isCup.HasValue)
                fixtures = from f in fixtures where f.IsCupFixture == isCup select f;


            return fixtures;
            //    // TODO Projection for id?

        }

        private IQueryable<Fixture> FilterFixturesByIsPlayed(string isPlayed, IQueryable<Fixture> fixtures)
        {
            // TODO Y/N enum?
            if (!string.IsNullOrEmpty(isPlayed))
            {
                fixtures = from f in fixtures where f.IsPlayed == isPlayed select f;
            }
            return fixtures;
        }

        private IQueryable<Fixture> FilterFixturesByTeamId(int teamId, IQueryable<Fixture> fixtures)
        {
            if (teamId > 0)
            {
                fixtures = from f in fixtures
                           where f.HomeTeamLeague.Team.Id == teamId || f.AwayTeamLeague.Team.Id == teamId
                           select f;
            }
            return fixtures;
        }

        public IQueryable<Fixture> GetFixturesForTeamLeagues(List<int> teamLeagueIds)
        {
            return (from f in Get()
                    where teamLeagueIds.Contains(f.HomeTeamLeague.Id)
                        || teamLeagueIds.Contains(f.AwayTeamLeague.Id)
                    orderby f.FixtureDate
                    select f);

            //    // TODO Projection for id?
        }

        public IQueryable<Fixture> GetPlayedFixturesForTeamInReverseDateOrder(int teamLeagueId)
        {
            return (from f in Get()
                    where (f.HomeTeamLeague.Id == teamLeagueId || f.AwayTeamLeague.Id == teamLeagueId)
                        && f.IsPlayed == "Y"
                    orderby f.FixtureDate descending
                    select f);
            // TODO Add projection for fixture
        }

        public IQueryable<Fixture> GetLatestMatchResults(int numResults)
        {
            return (from fixtures in Get()
                    where fixtures.IsPlayed == "Y"
                    orderby fixtures.ResultAddedDate descending
                    select fixtures)
                    .Take<Fixture>(numResults);
        }

        public IQueryable<Fixture> GetFixturesThatCanBePenalised(int seasonId, int numberOfDays)
        {
            DateTime now = DateTime.Now.Date;
            // Get fixtures for the current season that have been played and can be penalised
            // and
            //      ResultAddedDate is not null and ResultAddedDate more than 2 days after FixtureDate DON'T KNOW HOW TO DO THIS RIGHT NOW
            //      or
            //      ResultAddedDate is null and Today is more than 2 days after fixture

            var query = from fixture in Get()
                        where fixture.HomeTeamLeague.League.Season.Id == seasonId 
                        && fixture.IsCancelled == "N"
                        && fixture.IsPenaltyAllowed == true
                         && (
                             (fixture.ResultAddedDate != null && EntityFunctions.DiffDays(fixture.FixtureDate, fixture.ResultAddedDate) > numberOfDays)
                             ||
                             (fixture.ResultAddedDate == null && EntityFunctions.DiffDays(fixture.FixtureDate, now) > numberOfDays)
                         )
                        select fixture;
            
            return query;

        }

        public IQueryable<Fixture> GetFixturesForTeam(int teamId)
        {
            return (from f in Get()
                   where f.HomeTeamLeague.Team.Id == teamId || f.AwayTeamLeague.Team.Id == teamId
                   select f);
        }

        public IQueryable<Fixture> GetHistoricFixturesBetweenTeams(int homeTeamId, int awayTeamId, int currentFixtureId)
        {
            return (from f in Get()
                        where (f.HomeTeamLeague.Team.Id == homeTeamId && f.AwayTeamLeague.Team.Id == awayTeamId)
                            ||
                            (f.AwayTeamLeague.Team.Id == homeTeamId && f.HomeTeamLeague.Team.Id == awayTeamId)
                            && f.Id != currentFixtureId
                        orderby f.FixtureDate descending 
                        select f);
        }
    }
}
