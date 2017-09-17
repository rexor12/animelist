using System.Windows.Forms;
using AnimeList.UserInterface;

namespace AnimeList.Layouts
{
    public partial class AnimeListLayout : UserControl
    {
        public AnimeListLayout()
        {
            InitializeComponent();
        }

        public BetterGridView AnimeGridView
        {
            get { return animeGridView; }
        }
    }
}
