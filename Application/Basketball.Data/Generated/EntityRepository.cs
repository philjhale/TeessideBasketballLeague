using Basketball.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using Basketball.Data.Interfaces;
using Basketball.Common.BaseTypes;
using Basketball.Common.BaseTypes.Interfaces;

// TODO Tidy up
namespace Basketball.Data.Interfaces
{
	public partial interface ICupRepository : IBaseRepository<Cup> {}
public partial interface ICupLeagueRepository : IBaseRepository<CupLeague> {}
public partial interface ICupWinnerRepository : IBaseRepository<CupWinner> {}
public partial interface IDayOfWeekRepository : IBaseRepository<Basketball.Domain.Entities.DayOfWeek> {}
public partial interface IErrorRepository : IBaseRepository<Error> {}
public partial interface IEventRepository : IBaseRepository<Event> {}
public partial interface IFaqRepository : IBaseRepository<Faq> {}
public partial interface IFixtureRepository : IBaseRepository<Fixture> {}
public partial interface IFixtureHistoryRepository : IBaseRepository<FixtureHistory> {}
public partial interface ILeagueRepository : IBaseRepository<League> {}
public partial interface ILeagueWinnerRepository : IBaseRepository<LeagueWinner> {}
public partial interface INewsRepository : IBaseRepository<News> {}
public partial interface IOneOffVenueRepository : IBaseRepository<OneOffVenue> {}
public partial interface IOptionRepository : IBaseRepository<Option> {}
public partial interface IPenaltyRepository : IBaseRepository<Penalty> {}
public partial interface IPlayerRepository : IBaseRepository<Player> {}
public partial interface IPlayerCareerStatsRepository : IBaseRepository<PlayerCareerStats> {}
public partial interface IPlayerCupStatsRepository : IBaseRepository<PlayerCupStats> {}
public partial interface IPlayerFixtureRepository : IBaseRepository<PlayerFixture> {}
public partial interface IPlayerLeagueStatsRepository : IBaseRepository<PlayerLeagueStats> {}
public partial interface IPlayerSeasonStatsRepository : IBaseRepository<PlayerSeasonStats> {}
public partial interface IRefereeRepository : IBaseRepository<Referee> {}
public partial interface ISeasonRepository : IBaseRepository<Season> {}
public partial interface ITeamRepository : IBaseRepository<Team> {}
public partial interface ITeamLeagueRepository : IBaseRepository<TeamLeague> {}
public partial interface IUserRepository : IBaseRepository<User> {}
}

namespace Basketball.Data
{
	public partial class CupRepository : BaseRepository<Cup>, ICupRepository {
public CupRepository(IDbContext context) : base(context) {}
}
public partial class CupLeagueRepository : BaseRepository<CupLeague>, ICupLeagueRepository {
public CupLeagueRepository(IDbContext context) : base(context) {}
}
public partial class CupWinnerRepository : BaseRepository<CupWinner>, ICupWinnerRepository {
public CupWinnerRepository(IDbContext context) : base(context) {}
}
public partial class DayOfWeekRepository : BaseRepository<Basketball.Domain.Entities.DayOfWeek>, IDayOfWeekRepository {
public DayOfWeekRepository(IDbContext context) : base(context) {}
}
public partial class ErrorRepository : BaseRepository<Error>, IErrorRepository {
public ErrorRepository(IDbContext context) : base(context) {}
}
public partial class EventRepository : BaseRepository<Event>, IEventRepository {
public EventRepository(IDbContext context) : base(context) {}
}
public partial class FaqRepository : BaseRepository<Faq>, IFaqRepository {
public FaqRepository(IDbContext context) : base(context) {}
}
public partial class FixtureRepository : BaseRepository<Fixture>, IFixtureRepository {
public FixtureRepository(IDbContext context) : base(context) {}
}
public partial class FixtureHistoryRepository : BaseRepository<FixtureHistory>, IFixtureHistoryRepository {
public FixtureHistoryRepository(IDbContext context) : base(context) {}
}
public partial class LeagueRepository : BaseRepository<League>, ILeagueRepository {
public LeagueRepository(IDbContext context) : base(context) {}
}
public partial class LeagueWinnerRepository : BaseRepository<LeagueWinner>, ILeagueWinnerRepository {
public LeagueWinnerRepository(IDbContext context) : base(context) {}
}
public partial class NewsRepository : BaseRepository<News>, INewsRepository {
public NewsRepository(IDbContext context) : base(context) {}
}
public partial class OneOffVenueRepository : BaseRepository<OneOffVenue>, IOneOffVenueRepository {
public OneOffVenueRepository(IDbContext context) : base(context) {}
}
public partial class OptionRepository : BaseRepository<Option>, IOptionRepository {
public OptionRepository(IDbContext context) : base(context) {}
}
public partial class PenaltyRepository : BaseRepository<Penalty>, IPenaltyRepository {
public PenaltyRepository(IDbContext context) : base(context) {}
}
public partial class PlayerRepository : BaseRepository<Player>, IPlayerRepository {
public PlayerRepository(IDbContext context) : base(context) {}
}
public partial class PlayerCareerStatsRepository : BaseRepository<PlayerCareerStats>, IPlayerCareerStatsRepository {
public PlayerCareerStatsRepository(IDbContext context) : base(context) {}
}
public partial class PlayerCupStatsRepository : BaseRepository<PlayerCupStats>, IPlayerCupStatsRepository {
public PlayerCupStatsRepository(IDbContext context) : base(context) {}
}
public partial class PlayerFixtureRepository : BaseRepository<PlayerFixture>, IPlayerFixtureRepository {
public PlayerFixtureRepository(IDbContext context) : base(context) {}
}
public partial class PlayerLeagueStatsRepository : BaseRepository<PlayerLeagueStats>, IPlayerLeagueStatsRepository {
public PlayerLeagueStatsRepository(IDbContext context) : base(context) {}
}
public partial class PlayerSeasonStatsRepository : BaseRepository<PlayerSeasonStats>, IPlayerSeasonStatsRepository {
public PlayerSeasonStatsRepository(IDbContext context) : base(context) {}
}
public partial class RefereeRepository : BaseRepository<Referee>, IRefereeRepository {
public RefereeRepository(IDbContext context) : base(context) {}
}
public partial class SeasonRepository : BaseRepository<Season>, ISeasonRepository {
public SeasonRepository(IDbContext context) : base(context) {}
}
public partial class TeamRepository : BaseRepository<Team>, ITeamRepository {
public TeamRepository(IDbContext context) : base(context) {}
}
public partial class TeamLeagueRepository : BaseRepository<TeamLeague>, ITeamLeagueRepository {
public TeamLeagueRepository(IDbContext context) : base(context) {}
}
public partial class UserRepository : BaseRepository<User>, IUserRepository {
public UserRepository(IDbContext context) : base(context) {}
}
}


