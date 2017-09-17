using System;
using System.Drawing;

namespace AnimeList.UserInterface
{
    public class GridViewAnimeCard2 : IGridViewCell
    {
        public delegate void OnClickDelegate(GridViewAnimeCard2 button);
        public event OnClickDelegate OnClick;

        /// <summary>
        /// Identification number of this button. May or may not be unique.
        /// </summary>
        public int Id { get; set; }

        public Image Image { get; set; }

        public bool Enabled { get; set; }

        public BorderInfo Border { get; set; }

        public GridViewAnimeCard2()
        {
            Id = 0;
            Enabled = true;
            Border = BorderInfo.None;
        }

        public void Click()
        {
            OnClick?.Invoke(this);
        }

        public void Dispose()
        {
            Image?.Dispose();
        }

        public void OnPaint(Graphics g, RectangleF rect)
        {
            // draw the image
            if (Image != null)
                g.DrawImage(Image, rect);

            // draw the border
            if (Border.Size > 0)
            {
                g.DrawRectangle(Border.Pen, rect.X, rect.Y, rect.Width, rect.Height);
            }

            // draw the text
            //if (subtext != null && subtext != string.Empty)
            //{
            //    GraphicsPath p = new GraphicsPath();
            //    p.AddString(
            //        subtext,
            //        Font.FontFamily,
            //        (int)Font.Style,
            //        Font.Size,
            //        new PointF(rect.X, rect.Y),
            //        new StringFormat());
            //    g.DrawPath(Pens.Black, p);
            //    g.FillPath(Brushes.White, p);
            //}
        }
    }
}
