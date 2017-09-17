using System.Drawing;

namespace AnimeList.UserInterface
{
    public class BorderInfo
    {
        public static readonly BorderInfo None = new BorderInfo();

        private Color _color = Color.Black;
        private uint _size = 0;

        /// <summary>
        /// Color of the border.
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                CreatePen();
            }
        }
        /// <summary>
        /// Border size in pixels.
        /// </summary>
        public uint Size
        {
            get { return _size; }
            set
            {
                _size = value;
                CreatePen();
            }
        }

        public Pen Pen { get; private set; }

        public BorderInfo()
        {
            CreatePen();
        }

        private void CreatePen()
        {
            Pen?.Dispose();
            Pen = new Pen(_color, _size);
        }
    }
}
