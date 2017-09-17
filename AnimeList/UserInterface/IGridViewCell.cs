using System;
using System.Drawing;

namespace AnimeList.UserInterface
{
    /// <summary>
    /// Defines methods for a cell that a grid view can display.
    /// </summary>
    public interface IGridViewCell : IDisposable
    {
        void OnPaint(Graphics g, RectangleF rect);
    }
}
