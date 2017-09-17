using System.Drawing;
using System.Windows.Forms;

namespace AnimeList.UserInterface
{
    public partial class GridViewAnimeCard : UserControl, IGridViewCell
    {
        public Image Image
        {
            get { return cardImageBox.Image; }
            set { cardImageBox.Image = value; }
        }

        public string Title
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public GridViewAnimeCard()
        {
            InitializeComponent();
        }

        public void OnPaint(Graphics g, RectangleF rect)
        {
            if (this.Size.Width != (int)rect.Width ||
                this.Size.Height != (int)rect.Height)
                this.Size = new Size((int)rect.Width, (int)rect.Height);
            base.OnPaint(new PaintEventArgs(
                g,
                new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height)));
        }
    }
}
