using AnimeList.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AnimeList.Scrapers
{
    /// <summary>
    /// Used for downloading anime lists from https://www.anime-planet.com.
    /// </summary>
    public class AnimePlanetScraper : IAnimeDataScraper
    {
        public string Username { get; private set; }

        /// <summary>
        /// Starts downloading the entire anime list for the given user.<para/>
        /// Note: A password is not required for Anime-Planet.
        /// </summary>
        /// <param name="username">login name of the user</param>
        /// <param name="password">password of the user</param>
        /// <returns>the anime list of the given user</returns>
        public async Task<List<AnimeData>> Scrape(string username, string password = "")
        {
            Username = username;

            Debug.WriteLine("Downloading first page..");
            var page = await DownloadPage(1);
            Debug.WriteLine("Downloaded first page.");

            // figure out how many pages we have
            int pageCount = 0;
            int x = page.IndexOf("pagination");
            int y, z;
            int q = page.IndexOf("class='next", x);
            while (((x = page.IndexOf("&amp;page=", x + 1)) > -1) && (x < q))
            {
                y = page.IndexOf("'", x);
                z = int.Parse(page.Substring(x + 10, y - x - 10));
                if (z > pageCount)
                    pageCount = z;
            }
            Debug.WriteLine("Page count: {0}", pageCount);

            Debug.WriteLine("Getting data for each page..");
            var animeList = new List<AnimeData>();
            var temporaryList = await GetAnimeFromPage(page);
            animeList.AddRange(temporaryList);
            for (int i = 2; i < pageCount; i++)
            {
                page = await DownloadPage(i);
                temporaryList = await GetAnimeFromPage(page);
                animeList.AddRange(temporaryList);
            }
            Debug.WriteLine("Finished getting data.");

            return animeList;
        }

        /// <summary>
        /// Downloads the page with the given index. Pages on Anime-Planet start at 1.
        /// </summary>
        /// <param name="index">index of the page to download</param>
        /// <returns>the downloaded page's HTML code</returns>
        private async Task<string> DownloadPage(int index)
        {
            using (var client = new FollowingWebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                client.Headers[HttpRequestHeader.AcceptCharset] = "UTF-8";
                client.Headers[HttpRequestHeader.Host] = "www.anime-planet.com";
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36";
                var data = await client.DownloadDataTaskAsync("http://www.anime-planet.com/users/" + Username + "/anime?sort=title&page=" + index + "&mylist_view=list");
                return System.Text.Encoding.UTF8.GetString(data);
            }
        }

        /// <summary>
        /// Downloads an image at the given URL.
        /// </summary>
        /// <param name="url">the link to the image file</param>
        /// <returns>the downloaded image</returns>
        private async Task<Image> DownloadCoverImage(string url)
        {
            using (var client = new FollowingWebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                client.Headers[HttpRequestHeader.AcceptCharset] = "UTF-8";
                client.Headers[HttpRequestHeader.Host] = "www.anime-planet.com";
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36";
                var data = await client.DownloadDataTaskAsync(url);
                return new Bitmap(new MemoryStream(data));
            }
        }

        /// <summary>
        /// Extracts all the anime's data from the given page (HTML).
        /// </summary>
        /// <param name="page">the HTML code of the page</param>
        /// <returns>the list of the extracted anime data</returns>
        /// <seealso cref="DownloadPage"/>
        private async Task<List<AnimeData>> GetAnimeFromPage(string page)
        {
            var animeList = new List<AnimeData>();
            int x = -1, y;
            while ((x = page.IndexOf("tableTitle", x + 1)) > -1)
            {
                AnimeData adata = new AnimeData();

                // Title
                x = page.IndexOf("<h5>", x);
                y = page.IndexOf("</h5>", x);
                adata.Title = ReadData(page, x + 4, y);
                //Debug.WriteLine(adata.Title);

                // Cover image
                x = page.IndexOf("src='", x);
                y = page.IndexOf("'", x + 5);
                adata.CoverImage = await DownloadCoverImage("http://www.anime-planet.com" + ReadData(page, x + 5, y));
                //SaveImage(adata.CoverImage, adata.Id.ToString());

                // URL
                x = page.IndexOf("href=\"", x);
                y = page.IndexOf("\"", x + 6);
                adata.Url = ReadData(page, x + 6, y);
                //Debug.WriteLine(adata.Url);

                // Type
                x = page.IndexOf("tableType\">", x);
                y = page.IndexOf("</td>", x);
                adata.Type = ReadData(page, x + 11, y);
                //Debug.WriteLine(adata.Type);

                // Year
                x = page.IndexOf("tableYear\">", x);
                x = page.IndexOf("\">", x + 11);
                y = page.IndexOf("</a>", x);
                adata.Year = ReadData(page, x + 2, y);
                //Debug.WriteLine(adata.Year);

                // Average rating
                x = page.IndexOf("tableAverage\">", x);
                y = page.IndexOf("</td>", x);
                var averageRating = ReadData(page, x + 14, y);
                adata.AverageRating = averageRating == string.Empty ? 0.0f : float.Parse(averageRating, CultureInfo.InvariantCulture);
                //Debug.WriteLine(adata.AverageRating);

                // Status
                x = page.IndexOf("</span>", x);
                y = page.IndexOf("</td", x);
                var status = ReadData(page, x + 7, y);
                if (status.Contains("Watched"))
                {
                    adata.Status = AnimeData.AnimeStatus.Watched;
                }
                else if (status.Contains("Want to Watch"))
                {
                    adata.Status = AnimeData.AnimeStatus.WantToWatch;
                }
                else if (status.Contains("Watching"))
                {
                    adata.Status = AnimeData.AnimeStatus.Watching;
                }
                else if (status.Contains("Dropped"))
                {
                    adata.Status = AnimeData.AnimeStatus.Dropped;
                }
                else if (status.Contains("Won\'t Watch"))
                {
                    adata.Status = AnimeData.AnimeStatus.WontWatch;
                }
                else adata.Status = AnimeData.AnimeStatus.Unknown;
                //Debug.WriteLine(adata.Status);

                // Episodes
                x = page.IndexOf("tableEps\">", x);
                y = page.IndexOf("</td", x);
                var eps = ReadData(page, x + 10, y);
                adata.Episodes = eps == string.Empty ? 0 : int.Parse(eps);
                //Debug.WriteLine(adata.Episodes);

                // NumberOfTimes watched
                x = page.IndexOf("tableTimesWatched\">", x);
                y = page.IndexOf("</td>", x);
                var watchTimes = ReadData(page, x + 19, y).Replace("x", "");
                adata.Repeats = watchTimes == string.Empty ? 0 : int.Parse(watchTimes);
                //Debug.WriteLine(adata.Repeats);

                // Rating
                x = page.IndexOf("name", x);
                y = page.IndexOf("\"", x + 6);
                var rating = ReadData(page, x + 6, y);
                adata.Rating = rating == string.Empty ? 0 : float.Parse(rating, CultureInfo.InvariantCulture);
                //Debug.WriteLine(adata.Rating);

                animeList.Add(adata);

                Debug.WriteLine(string.Format("Saved {0}", adata.Title));
            }
            return animeList;
        }

        /// <summary>
        /// Convenience method for reading a set number of data and getting rid of unwanted whitespace.
        /// </summary>
        /// <param name="page">the HTML code of the page</param>
        /// <param name="start">starting offset</param>
        /// <param name="end">ending offset</param>
        /// <returns>the extracted text</returns>
        private string ReadData(string page, int start, int end)
        {
            int length = end - start;
            string data = page.Substring(start, length);
            data = data.Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("&nbsp;", "").Trim();
            return data;
        }
    }
}
