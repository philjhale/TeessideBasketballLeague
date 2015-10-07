using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class NewsController : BaseController
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            Check.Require(newsService != null, "newsService may not be null");
            this.newsService = newsService;
        }

        public ActionResult Index()
        {
            List<News> newsList = newsService.Get(orderBy: q => q.OrderByDescending(n => n.Id));
            return View(newsList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                newsService.Insert(news);
                newsService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(news);
        }

        public ActionResult Edit(int id)
        {
            News news = newsService.Get(id);
            return View(news);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                newsService.Update(news);
                newsService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(news);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            newsService.Delete(id);
            newsService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        }
    }
}
