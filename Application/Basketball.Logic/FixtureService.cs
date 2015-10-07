using System;
using System.Collections.Generic;
using System.Linq;
using Basketball.Common.BaseTypes;
using Basketball.Data.Connection;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;

namespace Basketball.Service
{
    public partial class FixtureService : BaseService<Fixture>, IFixtureService
    {
        private readonly ICompetitionService competitionService;
        private readonly IOptionService optionService;

        public FixtureService(IFixtureRepository fixtureRepository,
            ICompetitionService competitionService,
            IOptionService optionService) : base(fixtureRepository)
        {
            this.fixtureRepository = fixtureRepository;
            this.competitionService = competitionService;
            this.optionService = optionService;
        }

        public List<Fixture> GetHomeTeamFixturesForCurrentSeason(int teamId)
        {
            return fixtureRepository.GetTeamHomeFixturesForTeamLeagues(teamId, competitionService.GetTeamLeagueIdsForCurrentSeason()).ToList();
        }

        public List<Fixture> GetLatestMatchResults(int? numMatchResults = null)
        {
            if(!numMatchResults.HasValue)
                return fixtureRepository.GetLatestMatchResults(int.Parse(optionService.GetByName(Option.HOME_NUM_MATCH_REPORTS))).ToList();
            else
                return fixtureRepository.GetLatestMatchResults(numMatchResults.Value).ToList();
        }

        public List<Fixture> GetAllFixturesForCurrentSeason()
        {
            return this.GetFixturesForCurrentSeasonFilter(-1, null, null);
        }

        public List<Fixture> GetFixturesForCurrentSeasonFilter(int teamId, string isPlayed, bool isLeague, bool isCup, int? leagueOrCupId)
        {
            return fixtureRepository.GetFixturesForTeamLeaguesFilter(teamId, competitionService.GetTeamLeagueIdsForCurrentSeason(), isPlayed, isLeague, isCup, leagueOrCupId).ToList();
        }

        /// <param name="teamId">0 or less for all teams</param>
        /// <param name="isPlayed">Y/N - Null or blank for all</param>
        /// <param name="isCup">Y/N - Null or blank for all</param>
        /// <returns></returns>
        public List<Fixture> GetFixturesForCurrentSeasonFilter(int teamId, string isPlayed, bool? isCup)
        {
            return fixtureRepository.GetFixturesForTeamLeaguesFilter(teamId, competitionService.GetTeamLeagueIdsForCurrentSeason(), isPlayed, isCup).ToList();
        }

        public List<Fixture> GetFixturesForCurrentSeasonFilter()
        {
            return fixtureRepository.GetFixturesForTeamLeaguesFilter(-1, competitionService.GetTeamLeagueIdsForCurrentSeason(), "N", null).ToList();
        }

        public List<Fixture> GetPlayedFixturesForTeamInReverseDateOrder(int teamLeagueId)
        {
            return fixtureRepository.GetPlayedFixturesForTeamInReverseDateOrder(teamLeagueId).ToList();
        }

        /// <summary>
        /// Returns fixtures with a late result (i.e. time between match and match result being entered is less than 2 days)
        /// and that can be penalised (i.e. Fixture.IsPenaltyAllowed = true)
        /// </summary>
        /// <param name="seasonId"></param>
        /// <returns></returns>
        public List<Fixture> GetFixturesThatCanBePenalised(int seasonId)
        {
            return fixtureRepository.GetFixturesThatCanBePenalised(seasonId, Int32.Parse(optionService.GetByName(Option.SCHEDULE_LATE_MATCH_RESULT_DAYS))).ToList();
        }


        /// <summary>
        /// Same as usual get but ensures the fixture exists for the specified team
        /// </summary>
        /// <param name="fixtureId"></param>
        /// <param name="homeTeamId"></param>
        /// <returns></returns>
        public Fixture Get(int fixtureId, int homeTeamId)
        {
            return (from p in fixtureRepository.Get()
                    where p.Id == fixtureId && p.HomeTeamLeague.Team.Id == homeTeamId
                    select p).SingleOrDefault();
        }

        public int CountFixturesForTeam(int teamId)
        {
            return fixtureRepository.GetFixturesForTeam(teamId).Count();
        }

        /// <summary>
        /// Returns fixtures between specified teams, excluding the fixture currently being looked at
        /// </summary>
        /// <param name="homeTeamId"></param>
        /// <param name="awayTeamId"></param>
        /// <param name="currentFixtureId"></param>
        /// <returns></returns>
        public List<Fixture> GetHistoricFixturesBetweenTeams(int homeTeamId, int awayTeamId, int currentFixtureId)
        {
            return fixtureRepository.GetHistoricFixturesBetweenTeams(homeTeamId, awayTeamId, currentFixtureId).ToList();
        }
    }
}
