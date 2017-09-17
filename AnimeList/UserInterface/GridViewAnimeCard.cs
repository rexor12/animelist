using System.Drawing;
using System.Windows.Forms;

namespace AnimeList.UserInterface
{
    /// <summary>
    /// Grid view cell control that can display some of the data of an anime.
    /// </summary>
    public partial class GridViewAnimeCard : UserControl
    {
        /// <summary>
        /// The anime's cover image.
        /// </summary>
        public Image Image
        {
            get { return cardImageBox.Image; }
            set { cardImageBox.Image = value; }
        }

        /// <summary>
        /// The anime's title.
        /// </summary>
        public string Title
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public GridViewAnimeCard()
        {
            InitializeComponent();
        }
    }
}
