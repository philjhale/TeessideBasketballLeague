using System;
using System.Collections.Generic;
using System.Data.Entity;
using Basketball.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using Basketball.Data;
using System.Reflection;
using System.Data.Entity.Infrastructure;

/*
EF4.1 Balls. 
Fks = FieldName_Id
smallints are not allowed for some reason
*/
namespace Basketball.Tests.Data
{
    public partial class TestBasketballContext : IBasketballContext
	{
		public bool MustEagerLoadClosestNavigationProperties()
        {
            return false;
        }
		
		public IDbSet<Cup> Cups { get; set; }
		public IDbSet<CupLeague> CupLeagues { get; set; }
		public IDbSet<CupWinner> CupWinners { get; set; }
		public IDbSet<Basketball.Domain.Entities.DayOfWeek> DayOfWeeks { get; set; }
		public IDbSet<Error> Errors { get; set; }
		public IDbSet<Event> Events { get; set; }
		public IDbSet<Faq> Faqs { get; set; }
		public IDbSet<Fixture> Fixtures { get; set; }
		public IDbSet<FixtureHistory> FixtureHistories { get; set; }
		public IDbSet<League> Leagues { get; set; }
		public IDbSet<LeagueWinner> LeagueWinners { get; set; }
		public IDbSet<News> News { get; set; }
		public IDbSet<Option> Options { get; set; }
		public IDbSet<Penalty> Penalties { get; set; }
		public IDbSet<Player> Players { get; set; }
		public IDbSet<PlayerCareerStats> PlayerCareerStats { get; set; }
		public IDbSet<PlayerCupStats> PlayerCupStats { get; set; }
		public IDbSet<PlayerFixture> PlayerFixtures { get; set; }
		public IDbSet<PlayerLeagueStats> PlayerLeagueStats { get; set; }
		public IDbSet<PlayerSeasonStats> PlayerSeasonStats { get; set; }
		public IDbSet<Referee> Referees { get; set; }
		public IDbSet<Season> Seasons { get; set; }
		public IDbSet<Team> Teams { get; set; }
		public IDbSet<TeamLeague> TeamLeagues { get; set; }
		public IDbSet<User> Users { get; set; }

		public IDbSet<T> Set<T>() where T : class
	    {
	        foreach (PropertyInfo property in typeof(TestBasketballContext).GetProperties())
	        {
	            if (property.PropertyType == typeof(IDbSet<T>))
	                return property.GetValue(this, null) as IDbSet<T>;
	        }
	        throw new Exception("Type collection not found");
	    }
		
		public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
		{
			return null;
		}

	    public int SaveChanges()
	    {
	         // do nothing (probably set a variable as saved for testing)
			 return 1;
	    }
		
		/*public void Insert(TEntity entity)
		{
		
		}*/
		
		
	}
	
}