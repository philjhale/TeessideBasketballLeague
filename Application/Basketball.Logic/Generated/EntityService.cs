using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Data;
using Basketball.Domain.Entities;
using Basketball.Data.Interfaces;
using Basketball.Service.Interfaces;
using Basketball.Common.BaseTypes.Interfaces;
using Basketball.Common.BaseTypes;
using Basketball.Common.Extensions;


namespace Basketball.Service.Interfaces
{
	public partial interface ICupService : IBaseService<Cup> {}
public partial interface ICupLeagueService : IBaseService<CupLeague> {}
public partial interface ICupWinnerService : IBaseService<CupWinner> {}
public partial interface IDayOfWeekService : IBaseService<Basketball.Domain.Entities.DayOfWeek> {}
public partial interface IErrorService : IBaseService<Error> {}
public partial interface IEventService : IBaseService<Event> {}
public partial interface IFaqService : IBaseService<Faq> {}
public partial interface IFixtureService : IBaseService<Fixture> {}
public partial interface IFixtureHistoryService : IBaseService<FixtureHistory> {}
public partial interface ILeagueService : IBaseService<League> {}
public partial interface ILeagueWinnerService : IBaseService<LeagueWinner> {}
public partial interface INewsService : IBaseService<News> {}
public partial interface IOneOffVenueService : IBaseService<OneOffVenue> {}
public partial interface IOptionService : IBaseService<Option> {}
public partial interface IPenaltyService : IBaseService<Penalty> {}
public partial interface IPlayerService : IBaseService<Player> {}
public partial interface IPlayerCareerStatsService : IBaseService<PlayerCareerStats> {}
public partial interface IPlayerCupStatsService : IBaseService<PlayerCupStats> {}
public partial interface IPlayerFixtureService : IBaseService<PlayerFixture> {}
public partial interface IPlayerLeagueStatsService : IBaseService<PlayerLeagueStats> {}
public partial interface IPlayerSeasonStatsService : IBaseService<PlayerSeasonStats> {}
public partial interface IRefereeService : IBaseService<Referee> {}
public partial interface ISeasonService : IBaseService<Season> {}
public partial interface ITeamService : IBaseService<Team> {}
public partial interface ITeamLeagueService : IBaseService<TeamLeague> {}
public partial interface IUserService : IBaseService<User> {}
}

namespace Basketball.Service
{
	public partial class CupService : BaseService<Cup>, ICupService
{
		readonly ICupRepository cupRepository;
		public CupService(ICupRepository cupRepository) : base(cupRepository) {
		this.cupRepository = cupRepository;
		}
}
public partial class CupLeagueService : BaseService<CupLeague>, ICupLeagueService
{
		readonly ICupLeagueRepository cupLeagueRepository;
		public CupLeagueService(ICupLeagueRepository cupLeagueRepository) : base(cupLeagueRepository) {
		this.cupLeagueRepository = cupLeagueRepository;
		}
}
public partial class CupWinnerService : BaseService<CupWinner>, ICupWinnerService
{
		readonly ICupWinnerRepository cupWinnerRepository;
		public CupWinnerService(ICupWinnerRepository cupWinnerRepository) : base(cupWinnerRepository) {
		this.cupWinnerRepository = cupWinnerRepository;
		}
}
public partial class DayOfWeekService : BaseService<Basketball.Domain.Entities.DayOfWeek>, IDayOfWeekService
{
		readonly IDayOfWeekRepository dayOfWeekRepository;
		public DayOfWeekService(IDayOfWeekRepository dayOfWeekRepository) : base(dayOfWeekRepository) {
		this.dayOfWeekRepository = dayOfWeekRepository;
		}
}
public partial class ErrorService : BaseService<Error>, IErrorService
{
		readonly IErrorRepository errorRepository;
		public ErrorService(IErrorRepository errorRepository) : base(errorRepository) {
		this.errorRepository = errorRepository;
		}
}
public partial class EventService : BaseService<Event>, IEventService
{
		readonly IEventRepository eventRepository;
		public EventService(IEventRepository eventRepository) : base(eventRepository) {
		this.eventRepository = eventRepository;
		}
}
public partial class FaqService : BaseService<Faq>, IFaqService
{
		readonly IFaqRepository faqRepository;
		public FaqService(IFaqRepository faqRepository) : base(faqRepository) {
		this.faqRepository = faqRepository;
		}
}
public partial class FixtureService : BaseService<Fixture>, IFixtureService
{
		readonly IFixtureRepository fixtureRepository;
		public FixtureService(IFixtureRepository fixtureRepository) : base(fixtureRepository) {
		this.fixtureRepository = fixtureRepository;
		}
}
public partial class FixtureHistoryService : BaseService<FixtureHistory>, IFixtureHistoryService
{
		readonly IFixtureHistoryRepository fixtureHistoryRepository;
		public FixtureHistoryService(IFixtureHistoryRepository fixtureHistoryRepository) : base(fixtureHistoryRepository) {
		this.fixtureHistoryRepository = fixtureHistoryRepository;
		}
}
public partial class LeagueService : BaseService<League>, ILeagueService
{
		readonly ILeagueRepository leagueRepository;
		public LeagueService(ILeagueRepository leagueRepository) : base(leagueRepository) {
		this.leagueRepository = leagueRepository;
		}
}
public partial class LeagueWinnerService : BaseService<LeagueWinner>, ILeagueWinnerService
{
		readonly ILeagueWinnerRepository leagueWinnerRepository;
		public LeagueWinnerService(ILeagueWinnerRepository leagueWinnerRepository) : base(leagueWinnerRepository) {
		this.leagueWinnerRepository = leagueWinnerRepository;
		}
}
public partial class NewsService : BaseService<News>, INewsService
{
		readonly INewsRepository newsRepository;
		public NewsService(INewsRepository newsRepository) : base(newsRepository) {
		this.newsRepository = newsRepository;
		}
}
public partial class OneOffVenueService : BaseService<OneOffVenue>, IOneOffVenueService
{
		readonly IOneOffVenueRepository oneOffVenueRepository;
		public OneOffVenueService(IOneOffVenueRepository oneOffVenueRepository) : base(oneOffVenueRepository) {
		this.oneOffVenueRepository = oneOffVenueRepository;
		}
}
public partial class OptionService : BaseService<Option>, IOptionService
{
		readonly IOptionRepository optionRepository;
		public OptionService(IOptionRepository optionRepository) : base(optionRepository) {
		this.optionRepository = optionRepository;
		}
}
public partial class PenaltyService : BaseService<Penalty>, IPenaltyService
{
		readonly IPenaltyRepository penaltyRepository;
		public PenaltyService(IPenaltyRepository penaltyRepository) : base(penaltyRepository) {
		this.penaltyRepository = penaltyRepository;
		}
}
public partial class PlayerService : BaseService<Player>, IPlayerService
{
		readonly IPlayerRepository playerRepository;
		public PlayerService(IPlayerRepository playerRepository) : base(playerRepository) {
		this.playerRepository = playerRepository;
		}
}
public partial class PlayerCareerStatsService : BaseService<PlayerCareerStats>, IPlayerCareerStatsService
{
		readonly IPlayerCareerStatsRepository playerCareerStatsRepository;
		public PlayerCareerStatsService(IPlayerCareerStatsRepository playerCareerStatsRepository) : base(playerCareerStatsRepository) {
		this.playerCareerStatsRepository = playerCareerStatsRepository;
		}
}
public partial class PlayerCupStatsService : BaseService<PlayerCupStats>, IPlayerCupStatsService
{
		readonly IPlayerCupStatsRepository playerCupStatsRepository;
		public PlayerCupStatsService(IPlayerCupStatsRepository playerCupStatsRepository) : base(playerCupStatsRepository) {
		this.playerCupStatsRepository = playerCupStatsRepository;
		}
}
public partial class PlayerFixtureService : BaseService<PlayerFixture>, IPlayerFixtureService
{
		readonly IPlayerFixtureRepository playerFixtureRepository;
		public PlayerFixtureService(IPlayerFixtureRepository playerFixtureRepository) : base(playerFixtureRepository) {
		this.playerFixtureRepository = playerFixtureRepository;
		}
}
public partial class PlayerLeagueStatsService : BaseService<PlayerLeagueStats>, IPlayerLeagueStatsService
{
		readonly IPlayerLeagueStatsRepository playerLeagueStatsRepository;
		public PlayerLeagueStatsService(IPlayerLeagueStatsRepository playerLeagueStatsRepository) : base(playerLeagueStatsRepository) {
		this.playerLeagueStatsRepository = playerLeagueStatsRepository;
		}
}
public partial class PlayerSeasonStatsService : BaseService<PlayerSeasonStats>, IPlayerSeasonStatsService
{
		readonly IPlayerSeasonStatsRepository playerSeasonStatsRepository;
		public PlayerSeasonStatsService(IPlayerSeasonStatsRepository playerSeasonStatsRepository) : base(playerSeasonStatsRepository) {
		this.playerSeasonStatsRepository = playerSeasonStatsRepository;
		}
}
public partial class RefereeService : BaseService<Referee>, IRefereeService
{
		readonly IRefereeRepository refereeRepository;
		public RefereeService(IRefereeRepository refereeRepository) : base(refereeRepository) {
		this.refereeRepository = refereeRepository;
		}
}
public partial class SeasonService : BaseService<Season>, ISeasonService
{
		readonly ISeasonRepository seasonRepository;
		public SeasonService(ISeasonRepository seasonRepository) : base(seasonRepository) {
		this.seasonRepository = seasonRepository;
		}
}
public partial class TeamService : BaseService<Team>, ITeamService
{
		readonly ITeamRepository teamRepository;
		public TeamService(ITeamRepository teamRepository) : base(teamRepository) {
		this.teamRepository = teamRepository;
		}
}
public partial class TeamLeagueService : BaseService<TeamLeague>, ITeamLeagueService
{
		readonly ITeamLeagueRepository teamLeagueRepository;
		public TeamLeagueService(ITeamLeagueRepository teamLeagueRepository) : base(teamLeagueRepository) {
		this.teamLeagueRepository = teamLeagueRepository;
		}
}
public partial class UserService : BaseService<User>, IUserService
{
		readonly IUserRepository userRepository;
		public UserService(IUserRepository userRepository) : base(userRepository) {
		this.userRepository = userRepository;
		}
}
}


