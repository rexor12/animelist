using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AnimeList.Data;
using AnimeList.Layouts;
using AnimeList.UserInterface;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using AnimeList.Scrapers;

namespace AnimeList
{
    public partial class Form1 : Form
    {
        private AnimeListLayout AnimeListLayout;
        private List<AnimeData> AnimeList;

        public Form1()
        {
            InitializeComponent();

            // set the default layout
            AnimeListLayout = new AnimeListLayout();
            AnimeListLayout.Dock = DockStyle.Fill;
            layout.Controls.Add(AnimeListLayout);

            // load the list of animes
            AnimeList = LoadAnimeList();

            // display the anime list
            DisplayAnimeList(AnimeList);

            // set the grid view cells' size
            AnimeListLayout.AnimeGridView.CellWidth = 190; // 190
            AnimeListLayout.AnimeGridView.CellHeight = 350; // 285
        }

        /// <summary>
        /// Displays the anime from the given anime data list in the anime list layout's grid view.
        /// </summary>
        /// <param name="animeList">the list of anime to display</param>
        private void DisplayAnimeList(List<AnimeData> animeList)
        {
            // clear the list first
            AnimeListLayout.AnimeGridView.RemoveCells();

            // add the new items
            foreach (var animeData in AnimeList)
            {
                AnimeListLayout.AnimeGridView.AddCell(new GridViewAnimeCard()
                {
                    Image = LoadImage(animeData.Id.ToString()),
                    Title = animeData.Title
                });
            }
        }

        private async void downloadAnimePlanetButton_Click(object sender, EventArgs e)
        {
            // download the anime list
            Debug.WriteLine("Getting data for Anime-Planet..");
            var scraper = new AnimePlanetScraper();
            AnimeList = await scraper.Scrape("Rexor");
            Debug.WriteLine("Finished getting data for Anime-Planet.");

            // save the downloaded anime list
            SaveAnimeList(AnimeList);

            // display the downloaded anime list in the app
            DisplayAnimeList(AnimeList);
        }

        private async void downloadMyAnimeListButton_Click(object sender, EventArgs e)
        {
            // download the anime list
            Debug.WriteLine("Getting data for MyAnimeList..");
            var scraper = new MyAnimeListScraper();
            AnimeList = await scraper.Scrape("Unknown");
            Debug.WriteLine("Finished getting data for MyAnimeList.");

            // save the downloaded anime list
            SaveAnimeList(AnimeList);

            // display the downloaded anime list in the app
            DisplayAnimeList(AnimeList);
        }

        /// <summary>
        /// Loads the JPG image with the given name from the covers directory.
        /// </summary>
        /// <param name="name">name of the image file</param>
        /// <returns>the loaded image, or null if not found</returns>
        private Image LoadImage(string name)
        {
            if (!File.Exists("covers/" + name + ".jpg"))
            {
                return null;
            }

            using (var stream = new FileStream($"covers/{name}.jpg", FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(stream);
            }
        }

        /// <summary>
        /// Saves the given image with the given name to a JPG file in the covers directory.
        /// </summary>
        /// <param name="image">the image to save</param>
        /// <param name="name">the name of the image file (without file extension)</param>
        private void SaveImage(Image image, string name)
        {
            if (!Directory.Exists("covers"))
            {
                Directory.CreateDirectory("covers");
            }

            if (image != null)
            {
                image.Save("covers/" + name + ".jpg", ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// Loads the list of the anime data from the anime list save file.
        /// </summary>
        /// <returns>the anime list</returns>
        private List<AnimeData> LoadAnimeList()
        {
            // if the file doesn't exist, return an empty list
            if (!File.Exists("animelist.xml"))
                return new List<AnimeData>();

            // load the anime list from the file
            using (var fs = new FileStream("animelist.xml", FileMode.Open, FileAccess.Read))
            {
                var root = XElement.Load(fs);
                var animeList = root.XPathSelectElements("./anime").Select(_ => AnimeData.FromXml(_)).ToList();

                return animeList;
            }
        }

        /// <summary>
        /// Creates a backup of the current anime list, if there is one, then saves the new anime list.<para/>
        /// Note: A backup is created for the images as well.
        /// </summary>
        /// <param name="animeList">the anime list to save</param>
        private void SaveAnimeList(List<AnimeData> animeList)
        {
            // remove the old backup if exists
            if (File.Exists("animelist.xml.backup"))
            {
                File.Delete("animelist.xml.backup");
            }
            if (Directory.Exists("covers_backup"))
            {
                Directory.Delete("covers_backup", true);
            }

            // if we have an animelist, rename it to be the backup
            if (File.Exists("animelist.xml"))
            {
                File.Move("animelist.xml", "animelist.xml.backup");
            }
            if (Directory.Exists("covers"))
            {
                Directory.Move("covers", "covers_backup");
            }

            // create the xml list
            var rootElement = new XElement("animelist");
            foreach (var anime in animeList)
            {
                rootElement.Add(anime.ToXml());

                // save the image
                SaveImage(anime.CoverImage, anime.Id.ToString());
            }

            // create our new list
            using (var writer = new StreamWriter(new FileStream("animelist.xml", FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                writer.WriteLine(rootElement);
                writer.Flush();
            }
        }
    }
}
