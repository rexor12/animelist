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

        public GridView AnimeGridView
        {
            get { return animeGridView; }
        }
    }
}
