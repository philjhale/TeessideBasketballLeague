using System;
using System.Web.Mvc;
using Basketball.Domain.Entities;
using System.Collections.Generic;
using System.Web.Security;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using System.Linq;
using Basketball.Service;
using Basketball.Service.Interfaces;
using Basketball.Common.BaseTypes;
using Basketball.Web.BaseTypes;



namespace Basketball.Web.Controllers
{
	[HandleError]
public partial class CupController : BaseController
{
		private readonly ICupService cupService;

		public CupController(ICupService cupService) {
				Check.Require(cupService != null, "cupService may not be null");
				this.cupService = cupService;
		}

		public ActionResult View(int id) {
				Cup @cup = cupService.Get(id);
				return View(@cup);
		}

		public ActionResult Index() {
				List<Cup> cupList = cupService.Get();
				return View(cupList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Cup @cup) {
				if (ModelState.IsValid) {
						cupService.Insert(@cup);
						cupService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@cup);
		}

		public ActionResult Edit(int id) {
				Cup @cup = cupService.Get(id);
				return View(@cup);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Cup @cup) {
				if (ModelState.IsValid) {
						cupService.Update(@cup);
						cupService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@cup);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				cupService.Delete(id);
				cupService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class CupLeagueController : BaseController
{
		private readonly ICupLeagueService cupLeagueService;

		public CupLeagueController(ICupLeagueService cupLeagueService) {
				Check.Require(cupLeagueService != null, "cupLeagueService may not be null");
				this.cupLeagueService = cupLeagueService;
		}

		public ActionResult View(int id) {
				CupLeague @cupLeague = cupLeagueService.Get(id);
				return View(@cupLeague);
		}

		public ActionResult Index() {
				List<CupLeague> cupLeagueList = cupLeagueService.Get();
				return View(cupLeagueList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(CupLeague @cupLeague) {
				if (ModelState.IsValid) {
						cupLeagueService.Insert(@cupLeague);
						cupLeagueService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@cupLeague);
		}

		public ActionResult Edit(int id) {
				CupLeague @cupLeague = cupLeagueService.Get(id);
				return View(@cupLeague);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(CupLeague @cupLeague) {
				if (ModelState.IsValid) {
						cupLeagueService.Update(@cupLeague);
						cupLeagueService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@cupLeague);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				cupLeagueService.Delete(id);
				cupLeagueService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class CupWinnerController : BaseController
{
		private readonly ICupWinnerService cupWinnerService;

		public CupWinnerController(ICupWinnerService cupWinnerService) {
				Check.Require(cupWinnerService != null, "cupWinnerService may not be null");
				this.cupWinnerService = cupWinnerService;
		}

		public ActionResult View(int id) {
				CupWinner @cupWinner = cupWinnerService.Get(id);
				return View(@cupWinner);
		}

		public ActionResult Index() {
				List<CupWinner> cupWinnerList = cupWinnerService.Get();
				return View(cupWinnerList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(CupWinner @cupWinner) {
				if (ModelState.IsValid) {
						cupWinnerService.Insert(@cupWinner);
						cupWinnerService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@cupWinner);
		}

		public ActionResult Edit(int id) {
				CupWinner @cupWinner = cupWinnerService.Get(id);
				return View(@cupWinner);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(CupWinner @cupWinner) {
				if (ModelState.IsValid) {
						cupWinnerService.Update(@cupWinner);
						cupWinnerService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@cupWinner);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				cupWinnerService.Delete(id);
				cupWinnerService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class DayOfWeekController : BaseController
{
		private readonly IDayOfWeekService dayOfWeekService;

		public DayOfWeekController(IDayOfWeekService dayOfWeekService) {
				Check.Require(dayOfWeekService != null, "dayOfWeekService may not be null");
				this.dayOfWeekService = dayOfWeekService;
		}

		public ActionResult View(int id) {
				Basketball.Domain.Entities.DayOfWeek @dayOfWeek = dayOfWeekService.Get(id);
				return View(@dayOfWeek);
		}

		public ActionResult Index() {
				List<Basketball.Domain.Entities.DayOfWeek> dayOfWeekList = dayOfWeekService.Get();
				return View(dayOfWeekList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Basketball.Domain.Entities.DayOfWeek @dayOfWeek) {
				if (ModelState.IsValid) {
						dayOfWeekService.Insert(@dayOfWeek);
						dayOfWeekService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@dayOfWeek);
		}

		public ActionResult Edit(int id) {
				Basketball.Domain.Entities.DayOfWeek @dayOfWeek = dayOfWeekService.Get(id);
				return View(@dayOfWeek);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Basketball.Domain.Entities.DayOfWeek @dayOfWeek) {
				if (ModelState.IsValid) {
						dayOfWeekService.Update(@dayOfWeek);
						dayOfWeekService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@dayOfWeek);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				dayOfWeekService.Delete(id);
				dayOfWeekService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class ErrorController : BaseController
{
		private readonly IErrorService errorService;

		public ErrorController(IErrorService errorService) {
				Check.Require(errorService != null, "errorService may not be null");
				this.errorService = errorService;
		}

		public ActionResult View(int id) {
				Error @error = errorService.Get(id);
				return View(@error);
		}

		public ActionResult Index() {
				List<Error> errorList = errorService.Get();
				return View(errorList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Error @error) {
				if (ModelState.IsValid) {
						errorService.Insert(@error);
						errorService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@error);
		}

		public ActionResult Edit(int id) {
				Error @error = errorService.Get(id);
				return View(@error);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Error @error) {
				if (ModelState.IsValid) {
						errorService.Update(@error);
						errorService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@error);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				errorService.Delete(id);
				errorService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class EventController : BaseController
{
		private readonly IEventService eventService;

		public EventController(IEventService eventService) {
				Check.Require(eventService != null, "eventService may not be null");
				this.eventService = eventService;
		}

		public ActionResult View(int id) {
				Event @event = eventService.Get(id);
				return View(@event);
		}

		public ActionResult Index() {
				List<Event> eventList = eventService.Get();
				return View(eventList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Event @event) {
				if (ModelState.IsValid) {
						eventService.Insert(@event);
						eventService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@event);
		}

		public ActionResult Edit(int id) {
				Event @event = eventService.Get(id);
				return View(@event);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Event @event) {
				if (ModelState.IsValid) {
						eventService.Update(@event);
						eventService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@event);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				eventService.Delete(id);
				eventService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class FaqController : BaseController
{
		private readonly IFaqService faqService;

		public FaqController(IFaqService faqService) {
				Check.Require(faqService != null, "faqService may not be null");
				this.faqService = faqService;
		}

		public ActionResult View(int id) {
				Faq @faq = faqService.Get(id);
				return View(@faq);
		}

		public ActionResult Index() {
				List<Faq> faqList = faqService.Get();
				return View(faqList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Faq @faq) {
				if (ModelState.IsValid) {
						faqService.Insert(@faq);
						faqService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@faq);
		}

		public ActionResult Edit(int id) {
				Faq @faq = faqService.Get(id);
				return View(@faq);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Faq @faq) {
				if (ModelState.IsValid) {
						faqService.Update(@faq);
						faqService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@faq);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				faqService.Delete(id);
				faqService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class FixtureController : BaseController
{
		private readonly IFixtureService fixtureService;

		public FixtureController(IFixtureService fixtureService) {
				Check.Require(fixtureService != null, "fixtureService may not be null");
				this.fixtureService = fixtureService;
		}

		public ActionResult View(int id) {
				Fixture @fixture = fixtureService.Get(id);
				return View(@fixture);
		}

		public ActionResult Index() {
				List<Fixture> fixtureList = fixtureService.Get();
				return View(fixtureList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Fixture @fixture) {
				if (ModelState.IsValid) {
						fixtureService.Insert(@fixture);
						fixtureService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@fixture);
		}

		public ActionResult Edit(int id) {
				Fixture @fixture = fixtureService.Get(id);
				return View(@fixture);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Fixture @fixture) {
				if (ModelState.IsValid) {
						fixtureService.Update(@fixture);
						fixtureService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@fixture);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				fixtureService.Delete(id);
				fixtureService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class LeagueController : BaseController
{
		private readonly ILeagueService leagueService;

		public LeagueController(ILeagueService leagueService) {
				Check.Require(leagueService != null, "leagueService may not be null");
				this.leagueService = leagueService;
		}

		public ActionResult View(int id) {
				League @league = leagueService.Get(id);
				return View(@league);
		}

		public ActionResult Index() {
				List<League> leagueList = leagueService.Get();
				return View(leagueList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(League @league) {
				if (ModelState.IsValid) {
						leagueService.Insert(@league);
						leagueService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@league);
		}

		public ActionResult Edit(int id) {
				League @league = leagueService.Get(id);
				return View(@league);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(League @league) {
				if (ModelState.IsValid) {
						leagueService.Update(@league);
						leagueService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@league);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				leagueService.Delete(id);
				leagueService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class LeagueWinnerController : BaseController
{
		private readonly ILeagueWinnerService leagueWinnerService;

		public LeagueWinnerController(ILeagueWinnerService leagueWinnerService) {
				Check.Require(leagueWinnerService != null, "leagueWinnerService may not be null");
				this.leagueWinnerService = leagueWinnerService;
		}

		public ActionResult View(int id) {
				LeagueWinner @leagueWinner = leagueWinnerService.Get(id);
				return View(@leagueWinner);
		}

		public ActionResult Index() {
				List<LeagueWinner> leagueWinnerList = leagueWinnerService.Get();
				return View(leagueWinnerList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(LeagueWinner @leagueWinner) {
				if (ModelState.IsValid) {
						leagueWinnerService.Insert(@leagueWinner);
						leagueWinnerService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@leagueWinner);
		}

		public ActionResult Edit(int id) {
				LeagueWinner @leagueWinner = leagueWinnerService.Get(id);
				return View(@leagueWinner);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(LeagueWinner @leagueWinner) {
				if (ModelState.IsValid) {
						leagueWinnerService.Update(@leagueWinner);
						leagueWinnerService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@leagueWinner);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				leagueWinnerService.Delete(id);
				leagueWinnerService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class NewsController : BaseController
{
		private readonly INewsService newsService;

		public NewsController(INewsService newsService) {
				Check.Require(newsService != null, "newsService may not be null");
				this.newsService = newsService;
		}

		public ActionResult View(int id) {
				News @news = newsService.Get(id);
				return View(@news);
		}

		public ActionResult Index() {
				List<News> newsList = newsService.Get();
				return View(newsList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(News @news) {
				if (ModelState.IsValid) {
						newsService.Insert(@news);
						newsService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@news);
		}

		public ActionResult Edit(int id) {
				News @news = newsService.Get(id);
				return View(@news);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(News @news) {
				if (ModelState.IsValid) {
						newsService.Update(@news);
						newsService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@news);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				newsService.Delete(id);
				newsService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class OptionController : BaseController
{
		private readonly IOptionService optionService;

		public OptionController(IOptionService optionService) {
				Check.Require(optionService != null, "optionService may not be null");
				this.optionService = optionService;
		}

		public ActionResult View(int id) {
				Option @option = optionService.Get(id);
				return View(@option);
		}

		public ActionResult Index() {
				List<Option> optionList = optionService.Get();
				return View(optionList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Option @option) {
				if (ModelState.IsValid) {
						optionService.Insert(@option);
						optionService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@option);
		}

		public ActionResult Edit(int id) {
				Option @option = optionService.Get(id);
				return View(@option);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Option @option) {
				if (ModelState.IsValid) {
						optionService.Update(@option);
						optionService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@option);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				optionService.Delete(id);
				optionService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class PenaltyController : BaseController
{
		private readonly IPenaltyService penaltyService;

		public PenaltyController(IPenaltyService penaltyService) {
				Check.Require(penaltyService != null, "penaltyService may not be null");
				this.penaltyService = penaltyService;
		}

		public ActionResult View(int id) {
				Penalty @penalty = penaltyService.Get(id);
				return View(@penalty);
		}

		public ActionResult Index() {
				List<Penalty> penaltyList = penaltyService.Get();
				return View(penaltyList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Penalty @penalty) {
				if (ModelState.IsValid) {
						penaltyService.Insert(@penalty);
						penaltyService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@penalty);
		}

		public ActionResult Edit(int id) {
				Penalty @penalty = penaltyService.Get(id);
				return View(@penalty);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Penalty @penalty) {
				if (ModelState.IsValid) {
						penaltyService.Update(@penalty);
						penaltyService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@penalty);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				penaltyService.Delete(id);
				penaltyService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class PlayerController : BaseController
{
		private readonly IPlayerService playerService;

		public PlayerController(IPlayerService playerService) {
				Check.Require(playerService != null, "playerService may not be null");
				this.playerService = playerService;
		}

		public ActionResult View(int id) {
				Player @player = playerService.Get(id);
				return View(@player);
		}

		public ActionResult Index() {
				List<Player> playerList = playerService.Get();
				return View(playerList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Player @player) {
				if (ModelState.IsValid) {
						playerService.Insert(@player);
						playerService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@player);
		}

		public ActionResult Edit(int id) {
				Player @player = playerService.Get(id);
				return View(@player);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Player @player) {
				if (ModelState.IsValid) {
						playerService.Update(@player);
						playerService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@player);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				playerService.Delete(id);
				playerService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class PlayerCareerStatsController : BaseController
{
		private readonly IPlayerCareerStatsService playerCareerStatsService;

		public PlayerCareerStatsController(IPlayerCareerStatsService playerCareerStatsService) {
				Check.Require(playerCareerStatsService != null, "playerCareerStatsService may not be null");
				this.playerCareerStatsService = playerCareerStatsService;
		}

		public ActionResult View(int id) {
				PlayerCareerStats @playerCareerStats = playerCareerStatsService.Get(id);
				return View(@playerCareerStats);
		}

		public ActionResult Index() {
				List<PlayerCareerStats> playerCareerStatsList = playerCareerStatsService.Get();
				return View(playerCareerStatsList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(PlayerCareerStats @playerCareerStats) {
				if (ModelState.IsValid) {
						playerCareerStatsService.Insert(@playerCareerStats);
						playerCareerStatsService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@playerCareerStats);
		}

		public ActionResult Edit(int id) {
				PlayerCareerStats @playerCareerStats = playerCareerStatsService.Get(id);
				return View(@playerCareerStats);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(PlayerCareerStats @playerCareerStats) {
				if (ModelState.IsValid) {
						playerCareerStatsService.Update(@playerCareerStats);
						playerCareerStatsService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@playerCareerStats);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				playerCareerStatsService.Delete(id);
				playerCareerStatsService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class PlayerFixtureController : BaseController
{
		private readonly IPlayerFixtureService playerFixtureService;

		public PlayerFixtureController(IPlayerFixtureService playerFixtureService) {
				Check.Require(playerFixtureService != null, "playerFixtureService may not be null");
				this.playerFixtureService = playerFixtureService;
		}

		public ActionResult View(int id) {
				PlayerFixture @playerFixture = playerFixtureService.Get(id);
				return View(@playerFixture);
		}

		public ActionResult Index() {
				List<PlayerFixture> playerFixtureList = playerFixtureService.Get();
				return View(playerFixtureList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(PlayerFixture @playerFixture) {
				if (ModelState.IsValid) {
						playerFixtureService.Insert(@playerFixture);
						playerFixtureService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@playerFixture);
		}

		public ActionResult Edit(int id) {
				PlayerFixture @playerFixture = playerFixtureService.Get(id);
				return View(@playerFixture);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(PlayerFixture @playerFixture) {
				if (ModelState.IsValid) {
						playerFixtureService.Update(@playerFixture);
						playerFixtureService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@playerFixture);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				playerFixtureService.Delete(id);
				playerFixtureService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class PlayerLeagueStatsController : BaseController
{
		private readonly IPlayerLeagueStatsService playerLeagueStatsService;

		public PlayerLeagueStatsController(IPlayerLeagueStatsService playerLeagueStatsService) {
				Check.Require(playerLeagueStatsService != null, "playerLeagueStatsService may not be null");
				this.playerLeagueStatsService = playerLeagueStatsService;
		}

		public ActionResult View(int id) {
				PlayerLeagueStats @playerLeagueStats = playerLeagueStatsService.Get(id);
				return View(@playerLeagueStats);
		}

		public ActionResult Index() {
				List<PlayerLeagueStats> playerLeagueStatsList = playerLeagueStatsService.Get();
				return View(playerLeagueStatsList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(PlayerLeagueStats @playerLeagueStats) {
				if (ModelState.IsValid) {
						playerLeagueStatsService.Insert(@playerLeagueStats);
						playerLeagueStatsService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@playerLeagueStats);
		}

		public ActionResult Edit(int id) {
				PlayerLeagueStats @playerLeagueStats = playerLeagueStatsService.Get(id);
				return View(@playerLeagueStats);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(PlayerLeagueStats @playerLeagueStats) {
				if (ModelState.IsValid) {
						playerLeagueStatsService.Update(@playerLeagueStats);
						playerLeagueStatsService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@playerLeagueStats);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				playerLeagueStatsService.Delete(id);
				playerLeagueStatsService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class PlayerSeasonStatsController : BaseController
{
		private readonly IPlayerSeasonStatsService playerSeasonStatsService;

		public PlayerSeasonStatsController(IPlayerSeasonStatsService playerSeasonStatsService) {
				Check.Require(playerSeasonStatsService != null, "playerSeasonStatsService may not be null");
				this.playerSeasonStatsService = playerSeasonStatsService;
		}

		public ActionResult View(int id) {
				PlayerSeasonStats @playerSeasonStats = playerSeasonStatsService.Get(id);
				return View(@playerSeasonStats);
		}

		public ActionResult Index() {
				List<PlayerSeasonStats> playerSeasonStatsList = playerSeasonStatsService.Get();
				return View(playerSeasonStatsList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(PlayerSeasonStats @playerSeasonStats) {
				if (ModelState.IsValid) {
						playerSeasonStatsService.Insert(@playerSeasonStats);
						playerSeasonStatsService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@playerSeasonStats);
		}

		public ActionResult Edit(int id) {
				PlayerSeasonStats @playerSeasonStats = playerSeasonStatsService.Get(id);
				return View(@playerSeasonStats);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(PlayerSeasonStats @playerSeasonStats) {
				if (ModelState.IsValid) {
						playerSeasonStatsService.Update(@playerSeasonStats);
						playerSeasonStatsService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@playerSeasonStats);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				playerSeasonStatsService.Delete(id);
				playerSeasonStatsService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class SeasonController : BaseController
{
		private readonly ISeasonService seasonService;

		public SeasonController(ISeasonService seasonService) {
				Check.Require(seasonService != null, "seasonService may not be null");
				this.seasonService = seasonService;
		}

		public ActionResult View(int id) {
				Season @season = seasonService.Get(id);
				return View(@season);
		}

		public ActionResult Index() {
				List<Season> seasonList = seasonService.Get();
				return View(seasonList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Season @season) {
				if (ModelState.IsValid) {
						seasonService.Insert(@season);
						seasonService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@season);
		}

		public ActionResult Edit(int id) {
				Season @season = seasonService.Get(id);
				return View(@season);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Season @season) {
				if (ModelState.IsValid) {
						seasonService.Update(@season);
						seasonService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@season);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				seasonService.Delete(id);
				seasonService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class TeamController : BaseController
{
		private readonly ITeamService teamService;

		public TeamController(ITeamService teamService) {
				Check.Require(teamService != null, "teamService may not be null");
				this.teamService = teamService;
		}

		public ActionResult View(int id) {
				Team @team = teamService.Get(id);
				return View(@team);
		}

		public ActionResult Index() {
				List<Team> teamList = teamService.Get();
				return View(teamList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Team @team) {
				if (ModelState.IsValid) {
						teamService.Insert(@team);
						teamService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@team);
		}

		public ActionResult Edit(int id) {
				Team @team = teamService.Get(id);
				return View(@team);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Team @team) {
				if (ModelState.IsValid) {
						teamService.Update(@team);
						teamService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@team);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				teamService.Delete(id);
				teamService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class TeamLeagueController : BaseController
{
		private readonly ITeamLeagueService teamLeagueService;

		public TeamLeagueController(ITeamLeagueService teamLeagueService) {
				Check.Require(teamLeagueService != null, "teamLeagueService may not be null");
				this.teamLeagueService = teamLeagueService;
		}

		public ActionResult View(int id) {
				TeamLeague @teamLeague = teamLeagueService.Get(id);
				return View(@teamLeague);
		}

		public ActionResult Index() {
				List<TeamLeague> teamLeagueList = teamLeagueService.Get();
				return View(teamLeagueList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(TeamLeague @teamLeague) {
				if (ModelState.IsValid) {
						teamLeagueService.Insert(@teamLeague);
						teamLeagueService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@teamLeague);
		}

		public ActionResult Edit(int id) {
				TeamLeague @teamLeague = teamLeagueService.Get(id);
				return View(@teamLeague);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(TeamLeague @teamLeague) {
				if (ModelState.IsValid) {
						teamLeagueService.Update(@teamLeague);
						teamLeagueService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@teamLeague);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				teamLeagueService.Delete(id);
				teamLeagueService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
[HandleError]
public partial class UserController : BaseController
{
		private readonly IUserService userService;

		public UserController(IUserService userService) {
				Check.Require(userService != null, "userService may not be null");
				this.userService = userService;
		}

		public ActionResult View(int id) {
				User @user = userService.Get(id);
				return View(@user);
		}

		public ActionResult Index() {
				List<User> userList = userService.Get();
				return View(userList);
		}

		public ActionResult Create() {
				return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(User @user) {
				if (ModelState.IsValid) {
						userService.Insert(@user);
						userService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@user);
		}

		public ActionResult Edit(int id) {
				User @user = userService.Get(id);
				return View(@user);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(User @user) {
				if (ModelState.IsValid) {
						userService.Update(@user);
						userService.Commit();
						SuccessMessage(FormMessages.SaveSuccess);
						return RedirectToAction("Index");
				}
				return View(@user);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Delete(int id) {
				userService.Delete(id);
				userService.Commit();
				SuccessMessage(FormMessages.DeleteSuccess);
				return RedirectToAction("Index");
		}
}
}


