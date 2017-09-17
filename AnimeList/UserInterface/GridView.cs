using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AnimeList.UserInterface
{
    public class GridView : Panel
    {
        //public delegate void ItemClickedDelegate(GridViewImageButton item);
        //public event ItemClickedDelegate ItemClicked;

        private List<IGridViewCell> _buttons;
        private float _buttonWidth = 100.0f;
        private float _buttonHeight = 200.0f;
        private float _buttonMargin = 20.0f;
        private int _columns = 0;
        private int _rows = 0;

        /// <summary>
        /// Width of a button in pixels.
        /// </summary>
        public float ButtonWidth
        {
            get { return _buttonWidth; }
            set
            {
                if (value <= 0)
                    throw new Exception("Button width must be a positive number");

                _buttonWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Height of a button in pixels.
        /// </summary>
        public float ButtonHeight
        {
            get { return _buttonHeight; }
            set
            {
                if (value <= 0)
                    throw new Exception("Button height must be a positive number");

                _buttonHeight = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Extra space around a button.
        /// </summary>
        public float ButtonMargin
        {
            get { return _buttonMargin; }
            set
            {
                if (value <= 0)
                    throw new Exception("Button spacing must be a positive number");

                _buttonMargin = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The number of buttons in this grid view.
        /// </summary>
        public int ButtonCount
        {
            get { return _buttons.Count; }
        }

        public int Coulmns
        {
            get { return _columns; }
        }

        public int Rows
        {
            get { return _rows; }
        }

        public GridView(List<IGridViewCell> buttons)
            : base()
        {
            DoubleBuffered = true;

            _buttons = buttons;

            RefreshButtons();

            MouseClick += ButtonGridView_MouseClick;
        }

        public GridView()
            : this(new List<IGridViewCell>())
        {

        }

        public GridView(IGridViewCell[] buttons)
            : this()
        {
            _buttons.AddRange(buttons);
        }

        public void RemoveButtons()
        {
            _buttons.Clear();
            RefreshButtons();
        }

        public void AddButton(IGridViewCell button)
        {
            _buttons.Add(button);

            RefreshButtons();
        }

        public void AddButtons(IGridViewCell[] buttons)
        {
            _buttons.AddRange(buttons);

            RefreshButtons();
        }

        private void RefreshButtons()
        {
            // calculate how many columns we can have
            _columns = (int)((base.Width - 25.0f) / (ButtonWidth + ButtonMargin * 2.0f));
            _rows = (int)Math.Ceiling(_buttons.Count / (float)_columns);

            // calculate the needed size for the buttons
            int width = base.Width - 25; // the width is obvious, minus the scrollbar width(-ish)
            int height = (int)Math.Ceiling(_rows * (ButtonHeight + ButtonMargin) + ButtonMargin);
            base.AutoScrollMinSize = new Size(width, height);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            RefreshButtons();
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RefreshButtons();
            Invalidate();
        }

        private RectangleF paintRect = new RectangleF();
        protected override void OnPaint(PaintEventArgs e)
        {
            // first, we translate the canvas
            e.Graphics.TranslateTransform(base.AutoScrollPosition.X, base.AutoScrollPosition.Y);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;

            // clear the canvas
            e.Graphics.Clear(BackColor);

            // now we draw the buttons
            for (int i = 0; i < _buttons.Count; i++)
            {
                paintRect.X = i % _columns * (ButtonWidth + ButtonMargin * 2) + ButtonMargin;
                paintRect.Y = i / _columns * (ButtonHeight + ButtonMargin) + ButtonMargin;
                paintRect.Width = ButtonWidth;
                paintRect.Height = ButtonHeight;
                _buttons[i].OnPaint(e.Graphics, paintRect);
            }
        }

        private void ButtonGridView_MouseClick(object sender, MouseEventArgs e)
        {
            //int column = (int)((e.X - base.AutoScrollPosition.X) / ButtonWidth);
            //int row = (int)((e.Y - base.AutoScrollPosition.Y) / ButtonHeight);
            //if (column < _columns && row < Math.Ceiling((double)_buttons.Count / _columns))
            //{
            //    _buttons[row * _columns + column].Click();
            //}
        }

        public RectangleF PositionOf(GridViewImageButton btn)
        {
            // find the index of this button
            int idx;
            for (idx = 0; idx < _buttons.Count; idx++)
            {
                if (_buttons[idx] == btn)
                    break;
            }

            if (idx == _buttons.Count) // not in the gridview
                return new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);

            // calculate the bounding rectangle
            return new RectangleF(
                idx % _columns * ButtonWidth + base.AutoScrollPosition.X,
                idx / _columns * ButtonHeight + base.AutoScrollPosition.Y,
                ButtonWidth,
                ButtonHeight
            );
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            foreach (var button in _buttons)
            {
                button.Dispose();
            }
        }
    }
}
