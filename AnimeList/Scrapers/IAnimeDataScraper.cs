using AnimeList.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimeList.Scrapers
{
    /// <summary>
    /// Represents a scraper that can be used to download anime lists from a site.
    /// </summary>
    public interface IAnimeDataScraper
    {
        /// <summary>
        /// Starts downloading the entire anime list for the given user with the given password.<para/>
        /// Note: The username and password may not be required. It depends on the site in question.
        /// </summary>
        /// <param name="username">login name of the user</param>
        /// <param name="password">password of the user</param>
        /// <returns>the anime list of the given user</returns>
        Task<List<AnimeData>> Scrape(string username, string password = "");
    }
}
