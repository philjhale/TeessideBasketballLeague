using System;
using System.Collections.Generic;
using System.Data.Entity;
using Basketball.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using Basketball.Common.BaseTypes.Interfaces;
using Basketball.Common.Extensions;

/*
EF4.1 Balls. 
Fks = FieldName_Id
smallints are not allowed for some reason
*/
namespace Basketball.Data
{
	public class WcfBasketballContext : BasketballContext, IBasketballContext
	{
		public WcfBasketballContext()
        {
        	base.Configuration.ProxyCreationEnabled = false;    
        }
		
		new public bool MustEagerLoadClosestNavigationProperties()
        {
            return true; // WCF cannot lazy load. Must force eager load
        }
	}

    public class BasketballContext : DbContext, IBasketballContext
	{
		public bool MustEagerLoadClosestNavigationProperties()
        {
            return false; // Fine for web. Will lazy load
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
		//protected override void OnModelCreating(DbModelBuilder modelBuilder) {
    		//modelBuilder.IncludeMetadataInDatabase = false;
			//modelBuilder.Entity<Team>()
    		//	.Property(c => c.GameDay)
    		//	.HasColumnName("GameDayFk");
			//modelBuilder.Conventions.Remove<NavigationPropertyNameForeignKeyDiscoveryConvention>();
		//}
				
		new public IDbSet<TEntity> Set<TEntity>() where TEntity : class
	    {
	        return base.Set<TEntity>();
	    }

	}
	
	
	
	public interface IBasketballContext : IDbContext
	{
		IDbSet<Cup> Cups { get; set; }
		IDbSet<CupLeague> CupLeagues { get; set; }
		IDbSet<CupWinner> CupWinners { get; set; }
		IDbSet<Basketball.Domain.Entities.DayOfWeek> DayOfWeeks { get; set; }
		IDbSet<Error> Errors { get; set; }
		IDbSet<Event> Events { get; set; }
		IDbSet<Faq> Faqs { get; set; }
		IDbSet<Fixture> Fixtures { get; set; }
		IDbSet<FixtureHistory> FixtureHistories { get; set; }
		IDbSet<League> Leagues { get; set; }
		IDbSet<LeagueWinner> LeagueWinners { get; set; }
		IDbSet<News> News { get; set; }
		IDbSet<Option> Options { get; set; }
		IDbSet<Penalty> Penalties { get; set; }
		IDbSet<Player> Players { get; set; }
		IDbSet<PlayerCareerStats> PlayerCareerStats { get; set; }
		IDbSet<PlayerCupStats> PlayerCupStats { get; set; }
		IDbSet<PlayerFixture> PlayerFixtures { get; set; }
		IDbSet<PlayerLeagueStats> PlayerLeagueStats { get; set; }
		IDbSet<PlayerSeasonStats> PlayerSeasonStats { get; set; }
		IDbSet<Referee> Referees { get; set; }
		IDbSet<Season> Seasons { get; set; }
		IDbSet<Team> Teams { get; set; }
		IDbSet<TeamLeague> TeamLeagues { get; set; }
		IDbSet<User> Users { get; set; }
	}
}