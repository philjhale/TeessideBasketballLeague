using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Domain;
using Basketball.Data;


namespace Basketball.Logic
{
    public class NewsService
    {
        BaseRepository<News> newsRepository;

        public NewsService(BaseRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public List<News> GetNews()
        {
            return newsRepository.Get().ToList<News>();
        }

        public void InsertNews(News news)
        {
            newsRepository.Insert(news);
        }
    }
}
