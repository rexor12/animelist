using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnimeList.Data;

namespace AnimeList.Scrapers
{
    public class MyAnimeListScraper : IAnimeDataScraper
    {
        public async Task<List<AnimeData>> Scrape(string username, string password = "")
        {
            throw new NotImplementedException();
        }
    }
}
