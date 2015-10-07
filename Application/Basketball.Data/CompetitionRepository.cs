using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;

namespace Basketball.Data
{
    public class CompetitionRepository : ICompetitionRepository
    {
        #region Init
        readonly ISeasonRepository seasonRepository;
        readonly ILeagueRepository leagueRepository;
        readonly ITeamLeagueRepository teamLeagueRepository;
        readonly IFixtureRepository fixtureRepository;

        public CompetitionRepository(ISeasonRepository seasonRepository,
            ILeagueRepository leagueRepository,
            ITeamLeagueRepository teamLeagueRepository,
            IFixtureRepository fixtureRepository)
        {
            this.seasonRepository     = seasonRepository;
            this.leagueRepository     = leagueRepository;
            this.teamLeagueRepository = teamLeagueRepository;
            this.fixtureRepository    = fixtureRepository;
        }
        #endregion

        public void Commit()
        {
            // Not sure this is the right thing to do here
            seasonRepository.Commit();
            leagueRepository.Commit();
            teamLeagueRepository.Commit();
            fixtureRepository.Commit();
        }


        #region Season
        public Season GetCurrentSeason()
        {
            return (from s in seasonRepository.Get()
                    orderby s.Id descending
                    select s)
                    .Take(1).SingleOrDefault();
        }

        public IQueryable<Season> GetSeasons(
            Expression<Func<Season, bool>> filter = null,
            Func<IQueryable<Season>, IOrderedQueryable<Season>> orderBy = null
           )
        {
            return seasonRepository.Get(filter, orderBy);
        }

        #endregion

        #region League
        public IQueryable<League> GetLeaguesForSeason(int seasonId)
        {
            return (from l in leagueRepository.Get()
                    where l.Season.Id == seasonId
                    orderby l.DisplayOrder
                    select l);
            //    ICriteria criteria = Session.CreateCriteria(typeof(League))
            //        .AddOrder(Order.Asc("DisplayOrder"))
            //        .CreateCriteria("Season")
            //            .Add(Expression.IdEq(seasonId))
            //        ;

            //    return criteria.List<League>() as List<League>;
        }
        #endregion

        #region TeamLeague
        public IQueryable<TeamLeague> GetTeamLeaguesForCurrentSeason()
        {
            return GetTeamLeaguesForSeason(GetCurrentSeason().Id);
        }

        public IQueryable<TeamLeague> GetTeamLeaguesForSeason(int seasonId) //string orderByProperty
        {
            return (from tl in teamLeagueRepository.Get()
                    where tl.League.Season.Id == seasonId
                    orderby tl.League.DivisionNo, tl.TeamName
                    select tl);
        }

        public TeamLeague GetTeamLeagueByTeamIdInCurrentSeason(int teamId)
        {
            List<TeamLeague> teamLeagueCurrentSeasonList = GetTeamLeaguesForSeason(GetCurrentSeason().Id).ToList();

            return teamLeagueCurrentSeasonList.Single(x => x.Team.Id == teamId);
        }

        public TeamLeague GetTeamLeagueByTeamIdAndLeagueId(int teamId, int leagueId)
        {
            return (from tl in teamLeagueRepository.Get()
                    where tl.League.Id == leagueId
                        && tl.Team.Id == teamId
                    select tl).Single();
        }

        public IQueryable<TeamLeague> GetBasicStandingsForLeague(int leagueId)
        {
            return (from tl in teamLeagueRepository.Get()
                    where tl.League.Id == leagueId
                    orderby tl.PointsLeague descending//,
                        //tl.PointsScoredDifference descending,
                        //tl.TeamName
                    select tl);
        }

        public TeamLeague GetTeamLeague(int id)
        {
            return teamLeagueRepository.Get(id);
        }
        #endregion

        public List<Fixture> GetFixturesForTeamLeagues(List<int> teamLeagueIds)
        {
            return (from f in  fixtureRepository.Get()
                        where teamLeagueIds.Contains(f.HomeTeamLeague.Id)
                            || teamLeagueIds.Contains(f.AwayTeamLeague.Id)
                        select f).ToList();
        }

        

        #region CRUD
        public League GetLeague(int id)
        {
            return leagueRepository.Get(id);
        }

        public void InsertSeason(Season season)
        {
            seasonRepository.Insert(season);
        }

        public void UpdateTeamLeague(TeamLeague teamLeague)
        {
            teamLeagueRepository.Update(teamLeague);
        }
        #endregion
    }
}
