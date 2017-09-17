using System.Windows.Forms;

namespace AnimeList.Layouts
{
    public partial class LoginLayout : UserControl
    {
        private Panel _layoutContainer;

        public LoginLayout(Panel layoutContainer)
        {
            InitializeComponent();

            _layoutContainer = layoutContainer;
        }
    }
}
