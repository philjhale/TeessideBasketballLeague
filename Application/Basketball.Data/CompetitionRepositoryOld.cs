using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Domain;
using Basketball.Data.Interfaces;
using System.Linq.Expressions;


namespace Basketball.Data
{
    // TODO Sort methods
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
            this.seasonRepository = seasonRepository;
            this.leagueRepository = leagueRepository;
            this.teamLeagueRepository = teamLeagueRepository;
            this.fixtureRepository = fixtureRepository;
        }
        #endregion

        public void Commit()
        {
            // Since only one context is used for each request this SHOULD commit everything. Hope it does
            seasonRepository.Commit();
        }

        //public Season GetCurrentSeason()
        //{
        //    return (from s in seasonRepository.GetQueryable()
        //            orderby s.Id descending
        //            select s)
        //            .Take(1).SingleOrDefault<Season>();
        //}

        //public List<League> GetLeaguesForSeason(int seasonId)
        //{
        //    return (from l in leagueRepository.GetQueryable()
        //                where l.Season.Id == seasonId
        //                orderby l.DisplayOrder
        //                select l).ToList<League>();
        ////    ICriteria criteria = Session.CreateCriteria(typeof(League))
        ////        .AddOrder(Order.Asc("DisplayOrder"))
        ////        .CreateCriteria("Season")
        ////            .Add(Expression.IdEq(seasonId))
        ////        ;

        ////    return criteria.List<League>() as List<League>;
        //}

        #region TeamLeague DONE
        //TODO Not sure about param
        public List<TeamLeague> GetTeamLeaguesForCurrentSeason() //string orderByProperty
        {
            Season currentSeason = GetCurrentSeason();
            List<TeamLeague> teamLeagues = new List<TeamLeague>();

            if (currentSeason == null)
                return teamLeagues;

            return (from tl in teamLeagueRepository.GetQueryable()
                    where tl.League.Season.Id == currentSeason.Id
                    orderby tl.TeamName
                    select tl).ToList<TeamLeague>();
            //    Season currentSeason = GetCurrentSeason();
            //    List<TeamLeague> teamLeagueList = new List<TeamLeague>();

            //    if (currentSeason != null)
            //    {
            //        teamLeagueList = Session.CreateCriteria(typeof(TeamLeague))
            //            .AddOrder(Order.Asc(orderByProperty != null && orderByProperty != string.Empty ? orderByProperty : "TeamName"))
            //            .CreateCriteria("League")
            //                .CreateCriteria("Season")
            //                    .Add(Expression.IdEq(currentSeason.Id))
            //            .List<TeamLeague>();
            //    }

            //    return teamLeagueList;
        }

        public TeamLeague GetTeamLeagueInCurrentSeason(int teamId)
        {
            List<TeamLeague> teamLeagueCurrentSeasonList = GetTeamLeaguesForCurrentSeason();

            foreach (TeamLeague tl in teamLeagueCurrentSeasonList)
            {
                if (tl.Team.Id == teamId)
                    return tl;
            }

            return null;
        }

        public TeamLeague GetByTeamIdAndLeagueId(int teamId, int leagueId)
        {
            return (from tl in teamLeagueRepository.GetQueryable()
                    where tl.League.Id == leagueId
                        && tl.Team.Id == teamId
                    select tl)
                        .SingleOrDefault<TeamLeague>();
            //ICriteria criteria = Session.CreateCriteria(typeof(TeamLeague));

            //criteria.CreateCriteria("League")
            //    .Add(Expression.IdEq(leagueId));
            //criteria.CreateCriteria("Team")
            //    .Add(Expression.IdEq(teamId));

            //return criteria.UniqueResult<TeamLeague>();
        }

        public List<TeamLeague> GetStandingsForLeague(int leagueId)
        {
            return (from tl in teamLeagueRepository.GetQueryable()
                    where tl.League.Id == leagueId
                    orderby tl.PointsLeague descending,
                        tl.PointsScoredDifference descending,
                        tl.TeamName
                    select tl).ToList<TeamLeague>();
            //ICriteria criteria = Session.CreateCriteria(typeof(TeamLeague))
            //    .AddOrder(Order.Desc("PointsLeague"))
            //    .AddOrder(Order.Desc("PointsScoredDifference"))
            //    .AddOrder(Order.Asc("TeamName"))
            //    .CreateCriteria("League")
            //    .Add(Expression.IdEq(leagueId));

            //return criteria.List<TeamLeague>();
        }
        #endregion

        #region Fixture DONE
        public List<Fixture> GetTeamHomeFixturesForCurrentSeason(int teamId)
        {
            if (teamId <= 0)
                throw new InvalidOperationException("Parameter teamId must be greater than or equal to zero");

            List<int> teamLeagueIdsCurrentSeasonList = GetTeamLeagueIdsForCurrentSeason();

            return (from f in fixtureRepository.GetQueryable()
                    where teamLeagueIdsCurrentSeasonList.Contains(f.HomeTeamLeague.Id)
                    && f.HomeTeamLeague.Team.Id == teamId
                    orderby f.FixtureDate
                    select f).ToList<Fixture>();

            //    List<TeamLeague> teamLeagueCurrentSeasonList = GetTeamLeaguesForCurrentSeason();
            //    List teamLeagueIdList = new ArrayList();

            //    foreach (TeamLeague tl in teamLeagueCurrentSeasonList)
            //        teamLeagueIdList.Add(tl.Id.ToString());

            //    ICriteria criteria = Session.CreateCriteria(typeof(Fixture))
            //        .AddOrder(Order.Asc("FixtureDate"))
            //        .CreateAlias("HomeTeamLeague", "htl")
            //        .Add(Expression.In("htl.Id", teamLeagueIdList))
            //        .Add(Expression.Eq("htl.Team.Id", teamId));

            //    return criteria.List<Fixture>();
        }


        public List<Fixture> GetFixturesForCurrentSeasonFilter(int teamId,
            string isPlayed,
            string isCup)
        {
            IQueryable<Fixture> fixtures = _GetFixturesForCurrentSeason();

            if (teamId > 0)
            {
                fixtures = from f in fixtures where f.HomeTeamLeague.Team.Id == teamId || f.AwayTeamLeague.Team.Id == teamId select f;
            }

            // TODO Y/N enum?
            if (!string.IsNullOrEmpty(isPlayed))
                fixtures = from f in fixtures where f.IsPlayed == isPlayed select f;
            if (!string.IsNullOrEmpty(isCup))
                fixtures = from f in fixtures where f.IsCupFixture == isCup select f;


            return fixtures.ToList<Fixture>();
            //    // TODO Projection for id?
            //    List<TeamLeague> teamLeagueCurrentSeasonList = GetTeamLeaguesForCurrentSeason();
            //    List teamLeagueIdList = new ArrayList();

            //    foreach (TeamLeague tl in teamLeagueCurrentSeasonList)
            //        teamLeagueIdList.Add(tl.Id.ToString());

            //    ICriteria criteria = Session.CreateCriteria(typeof(Fixture))
            //        .AddOrder(Order.Asc("FixtureDate"))
            //        .CreateAlias("HomeTeamLeague", "htl")
            //        .CreateAlias("AwayTeamLeague", "atl")
            //        .Add(Expression.Or(
            //            Expression.In("htl.Id", teamLeagueIdList),
            //            Expression.In("atl.Id", teamLeagueIdList)
            //            ));
            //    if(teamId > 0 ) 
            //    {
            //        criteria.Add(Expression.Or(
            //            Expression.Eq("htl.Team.Id", teamId),
            //            Expression.Eq("atl.Team.Id", teamId))
            //        );
            //    }

            //    if (!string.IsNullOrEmpty(isPlayed))
            //        criteria.Add(Expression.Like("IsPlayed", isPlayed));
            //    if (!string.IsNullOrEmpty(isCup))
            //        criteria.Add(Expression.Like("IsCupFixture", isCup));

            //    return criteria.List<Fixture>();
        }

        private IQueryable<Fixture> _GetFixturesForCurrentSeason()
        {
            List<int> teamLeagueIdCurrentSeasonList = GetTeamLeagueIdsForCurrentSeason();

            return (from f in fixtureRepository.GetQueryable()
                    where teamLeagueIdCurrentSeasonList.Contains(f.HomeTeamLeague.Id)
                        || teamLeagueIdCurrentSeasonList.Contains(f.AwayTeamLeague.Id)
                    orderby f.FixtureDate
                    select f);

            //    // TODO Projection for id?
            //    List<TeamLeague> teamLeagueCurrentSeasonList = GetTeamLeaguesForCurrentSeason();
            //    List teamLeagueIdList = new ArrayList();

            //    foreach (TeamLeague tl in teamLeagueCurrentSeasonList)
            //        teamLeagueIdList.Add(tl.Id.ToString());

            //    ICriteria criteria = Session.CreateCriteria(typeof(Fixture))
            //        .AddOrder(Order.Asc("FixtureDate"))
            //        .CreateAlias("HomeTeamLeague", "htl")
            //        .CreateAlias("AwayTeamLeague", "atl")
            //        .Add(Expression.Or(
            //            Expression.In("htl.Id", teamLeagueIdList),
            //            Expression.In("atl.Id", teamLeagueIdList)
            //            ));

            //    return criteria.List<Fixture>();
        } 

        public List<Fixture> GetPlayedFixturesForTeamInReverseDateOrder(int teamLeagueId)
        {
            return (from f in fixtureRepository.GetQueryable()
                        where (f.HomeTeamLeague.Id == teamLeagueId || f.AwayTeamLeague.Id == teamLeagueId)
                            && f.IsPlayed == "Y"
                        orderby f.FixtureDate descending
                        select f).ToList<Fixture>();
            // TODO Add projection for fixture
            // Get all played fixtures for team in fixture reverse date order
            //ICriteria criteria = Session.CreateCriteria(typeof(Fixture))
            //    .CreateAlias("HomeTeamLeague", "htl")
            //    .CreateAlias("AwayTeamLeague", "atl")
            //    .AddOrder(Order.Desc("FixtureDate"))
            //    .Add(Expression.Eq("IsPlayed", "Y"))
            //    .Add(Expression.Or(
            //        Expression.Eq("htl.Id", teamLeagueId),
            //        Expression.Eq("atl.Id", teamLeagueId)));
        }

        public List<Fixture> GetLatestMatchResults(int numResults)
        {
            return (from fixtures in fixtureRepository.GetQueryable()
                    where fixtures.IsPlayed == "Y"
                    orderby fixtures.ResultAddedDate descending
                    select fixtures)
                    .Take<Fixture>(numResults)
                    .ToList<Fixture>();
        }

        public List<Fixture> GetFixturesWithLateResult(int seasonId, int maxHours)
        {
            // Get fixtures for the current season that have been played
            // and
            //      ResultAddedDate is not null and ResultAddedDate more than 2 days after FixtureDate DON'T KNOW HOW TO DO THIS RIGHT NOW
            //      or
            //      ResultAddedDate is null and Today is more than 2 days after fixture
            var query = from fixture in fixtureRepository.GetQueryable()
                        where fixture.HomeTeamLeague.League.Season.Id == seasonId && fixture.IsCancelled == "N"
                         &&
                             (fixture.ResultAddedDate != null && fixture.ResultAddedDate > fixture.FixtureDate.AddDays(2))
                             ||
                             (fixture.ResultAddedDate == null && DateTime.Now.Date > fixture.FixtureDate.AddDays(2))
                        select fixture;

            return query.ToList<Fixture>();

            //ICriteria criteria = Session.CreateCriteria<Fixture>()
            //    .CreateAlias("HomeTeamLeague", "htl")
            //    .CreateAlias("htl.League", "l")
            //    .CreateAlias("l.Season", "s")
            //    .Add(Restrictions.Eq("s.Id", seasonId))
            //    .Add(Restrictions.Eq("IsCancelled", "N")) // Ignore cancelled fixtures
            //    .Add(Restrictions.Or(
            //        Restrictions.And(
            //            Restrictions.IsNotNull("ResultAddedDate"),
            //    //Restrictions.EqProperty(Projections.SqlFunction("dateadd", NHibernateUtil.DateTime, Projections.Property("ResultAddedDate")),Projections.Property("FixtureDate") )
            //            Expression.Sql("FixtureDate < DateAdd(Day, -2, DateDiff(dd, 0, ResultAddedDate))") // Compare fixtureDate to ResultAddedDate minus 2 days and truncated to remove the time value
            //        ),
            //        Restrictions.And(
            //            Restrictions.IsNull("ResultAddedDate"),
            //            Restrictions.Lt("FixtureDate", DateTime.Now.Date.AddDays(-2))
            //        )
            //    ));

            //return criteria.List<Fixture>() as IList<Fixture>;
        }
        #endregion

        #region Team TODO
        public List<Team> GetTeamsForCurrentSeason()
        {
            List<TeamLeague> teamLeagueList = GetTeamLeaguesForCurrentSeason();

            List<Team> teamList = new List<Team>();

            foreach (TeamLeague tl in teamLeagueList)
                teamList.Add(tl.Team);

            return teamList;
        }
        #endregion

        #region CRUD
        public TeamLeague GetTeamLeague(int id)
        {
            return teamLeagueRepository.Get(id);
        }

        public Fixture GetFixture(int id)
        {
            return fixtureRepository.Get(id);
        }

        public List<Season> GetSeasons(
            Expression<Func<Season, bool>> filter = null,
            Func<IQueryable<Season>, IOrderedQueryable<Season>> orderBy = null
           )
        {
            return seasonRepository.Get(filter, orderBy);
        }

        public void UpdateFixture(Fixture fixture)
        {
            fixtureRepository.Update(fixture);
        }
        #endregion

        #region Misc
        private List<int> GetTeamLeagueIdsForCurrentSeason()
        {
            return (from tl in GetTeamLeaguesForCurrentSeason() select tl.Id).ToList<int>();
        }
        #endregion

        #region Cup and CupLeague methods plus anything I'm not sure what to do with right now
        // TODO Move to teamleague repo? Can I do this?
        //public List<TeamLeague> GetTeamLeaguesForCurrentSeason()
        //{
        //    return GetTeamLeaguesForCurrentSeason("TeamName");
        //}

        // TODO CupLeague
        //public List<Cup> GetCupsForCurrentSeason(Season currentSeason)
        //{
        //    //List<CupLeague> cupLeaguesForSeasonList = Session.CreateCriteria(typeof(CupLeague))
        //    //    .CreateAlias("League", "l")
        //    //    .Add(Expression.Eq("l.Season.Id", GetCurrentSeason().Id))
        //    //    .List<CupLeague>();

        //    //// TODO This is shit. Make it better
        //    //List<Cup> cupList = new List<Cup>();
        //    //foreach (CupLeague cl in cupLeaguesForSeasonList)
        //    //{
        //    //    if (!cupList.Contains(cl.Cup))
        //    //        cupList.Add(cl.Cup);
        //    //}

        //    //return cupList;
        //}

        // TODO Move to LeagueRepository
        //public IList<League> GetLeaguesForCup(int cupId)
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof(CupLeague))
        //        .CreateCriteria("Cup")
        //            .Add(Expression.IdEq(cupId));

        //    IList<CupLeague> cupLeagueList = criteria.List<CupLeague>();
        //    IList<League> assignedLeagueList = new List<League>();

        //    foreach (CupLeague cl in cupLeagueList)
        //        assignedLeagueList.Add(cl.League);

        //    return assignedLeagueList;
        //}

        // Move to CupLeagueRepository
        //public CupLeague GetCupLeague(int cupId, int leagueId)
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof(CupLeague))
        //        .CreateAlias("Cup", "c")
        //        .CreateAlias("League", "l")
        //        .Add(Expression.Eq("c.Id", cupId))
        //        .Add(Expression.Eq("l.Id", leagueId));

        //    return criteria.UniqueResult<CupLeague>();
        //}

        // TODO Move to LeagueRepository
        //public void DeleteLeaguesForCup(int cupId)
        //{
        //    IList<CupLeague> cupLeaguesToDelete = Session.CreateCriteria(typeof(CupLeague))
        //        .CreateCriteria("Cup")
        //        .Add(Expression.IdEq(cupId))
        //        .List<CupLeague>();

        //    foreach (CupLeague cl in cupLeaguesToDelete)
        //        DeleteCupLeague(cl);
        //}

        // TODO Not sure what to do with this
        //public List<Team> GetTeamsForCupCurrentSeason(int cupId)
        //{
        //    // TODO THIS IS THE BIGGEST PILE OF SHIT EVER! CHANGE IT OR DIE!!!
        //    // Get the cupleagues for the specified cup which are assocated with league from
        //    // the current season
        //    List<CupLeague> cupLeaguesForCupAndCurrentSeasonList = Session.CreateCriteria(typeof(CupLeague))
        //        .CreateAlias("League", "l")
        //        .Add(Expression.Eq("Cup.Id", cupId))
        //        .Add(Expression.Eq("l.Season.Id", GetCurrentSeason().Id))
        //        .List<CupLeague>();

        //    List leagueIdList = new ArrayList();

        //    foreach (CupLeague cl in cupLeaguesForCupAndCurrentSeasonList)
        //        leagueIdList.Add(cl.League.Id);

        //    // Get all teamleague which match the found leagueIds
        //    List<TeamLeague> teamLeagueListForLeagues = Session.CreateCriteria(typeof(TeamLeague))
        //        .AddOrder(Order.Asc("TeamName"))
        //        .CreateCriteria("League")
        //            .Add(Expression.In("Id", leagueIdList))
        //        .List<TeamLeague>();

        //    List<Team> teamList = new List<Team>();

        //    foreach (TeamLeague tl in teamLeagueListForLeagues)
        //        teamList.Add(tl.Team);

        //    return teamList;
        //} 
        #endregion
    }
}
