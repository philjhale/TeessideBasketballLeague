using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Basketball.Service;
using Basketball.Domain.Entities;
using Rhino.Mocks;
using Basketball.Data;

namespace Basketball.Tests.Data
{
    //[TestFixture]
    //public class NewsServiceTests
    //{
    //    private MockRepository mock;
    //    private BaseRepository<News> newsRepository;
    //    private BasketballContext context;
    //    private NewsService newsService;
        
    //    [SetUp]
    //    public void Setup()
    //    {
    //        mock = new MockRepository();
    //        context = mock.PartialMock<BasketballContext>();
    //        newsRepository = mock.PartialMock<BaseRepository<News>>(context);
    //        newsService = new NewsService(newsRepository);
    //    }

    //    [TearDown]
    //    public void Teardown()
    //    {
    //        mock = null;
    //        context = null;
    //        newsRepository = null;
    //        newsService = null;
    //    }

    //    [Test]
    //    public void Get()
    //    {
    //        using (mock.Record())
    //        {

    //            Expect.Call(newsRepository.Get()).Return(GetNewsList());
    //        }
            
    //        List<News> list = new List<News>();
    //        using (mock.Playback())
    //        {
    //            list = newsService.GetNews();   
    //        }
            
 
    //        Assert.That(list, Is.Not.Null);
    //        Assert.That(list.Count, Is.EqualTo(2));
    //        Assert.That(list[0].UserId, Is.EqualTo(1));
    //    }

    //    [Test]
    //    public void Insert()
    //    {
    //        News newItem = new News("s", "m", "10");

    //        using (mock.Record())
    //        {
    //            Expect.Call(delegate { newsRepository.Insert(newItem); });
    //        }

            
    //        using (mock.Playback())
    //        {
    //            newsService.InsertNews(newItem);
    //        }
    //    }

    //    private List<News> GetNewsList()
    //    {
    //        List<News> news = new List<News>();
    //        news.Add(new News("sub", "message", "1"));
    //        news.Add(new News("sub", "message", "2"));

    //        return news;
    //    }
    //}
}
