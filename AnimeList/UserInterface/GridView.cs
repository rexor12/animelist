using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AnimeList.UserInterface
{
    /// <summary>
    /// A cool grid view that can display cells in rows and columns with configurable sizes.
    /// </summary>
    public partial class GridView : UserControl
    {
        private List<UserControl> _cells;
        private float _cellWidth = 100.0f;
        private float _cellHeight = 200.0f;
        private float _cellMargin = 20.0f;
        private int _columns = 0;
        private int _rows = 0;

        /// <summary>
        /// Width of a cell in pixels.
        /// </summary>
        public float CellWidth
        {
            get { return _cellWidth; }
            set
            {
                if (value <= 0)
                    throw new Exception("Button width must be a positive number");

                _cellWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Height of a cell in pixels.
        /// </summary>
        public float CellHeight
        {
            get { return _cellHeight; }
            set
            {
                if (value <= 0)
                    throw new Exception("Button height must be a positive number");

                _cellHeight = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Extra space around a cell.
        /// </summary>
        public float CellMargin
        {
            get { return _cellMargin; }
            set
            {
                if (value <= 0)
                    throw new Exception("Button spacing must be a positive number");

                _cellMargin = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The number of cells in this grid view.
        /// </summary>
        public int CellCount
        {
            get { return _cells.Count; }
        }
        
        /// <summary>
        /// The number of columns this grid view has.
        /// </summary>
        public int Coulmns
        {
            get { return _columns; }
        }

        /// <summary>
        /// The number of rows this grid view has.
        /// </summary>
        public int Rows
        {
            get { return _rows; }
        }

        public void Initialize()
        {
            InitializeComponent();

            RefreshCells();
        }

        public GridView(List<UserControl> cells)
            : base()
        {
            DoubleBuffered = true;

            _cells = cells;

            Initialize();
        }

        public GridView()
            : this(new List<UserControl>())
        {

        }

        public GridView(UserControl[] cells)
            : this()
        {
            _cells.AddRange(cells);
        }

        /// <summary>
        /// Removes all the cells from this grid view.
        /// </summary>
        public void RemoveCells()
        {
            _cells.Clear();
            RefreshCells();
        }

        /// <summary>
        /// Adds the given cell to the grid view.
        /// </summary>
        /// <param name="cell">the cell to add</param>
        public void AddCell(UserControl cell)
        {
            _cells.Add(cell);

            RefreshCells();
        }

        /// <summary>
        /// Adds the array of cells to the grid view.
        /// </summary>
        /// <param name="cells">the cells to add</param>
        public void AddCells(UserControl[] cells)
        {
            _cells.AddRange(cells);

            RefreshCells();
        }
        
        /// <summary>
        /// Removes the cells that should be removed, adds the new cells and updates each cell with its new position and size.
        /// </summary>
        private void RefreshCells()
        {
            // calculate how many columns we can have
            _columns = (int)((base.Width - 25.0f) / (CellWidth + CellMargin * 2.0f));
            _rows = (int)Math.Ceiling(_cells.Count / (float)_columns);

            // calculate the needed size for the cells
            int width = base.Width - 25; // the width is obvious, minus the scrollbar width(-ish)
            int height = (int)Math.Ceiling(_rows * (CellHeight + CellMargin) + CellMargin);
            base.AutoScrollMinSize = new Size(width, height);

            // set each button's size, remove cells that have been removed, add cells that are new
            var controlsToRemove = new List<UserControl>();
            Controls.Each(_ =>
            {
                var cell = _ as UserControl;

                // the control is old, so remove it
                if (!_cells.Contains(cell))
                {
                    controlsToRemove.Add(cell);
                    return;
                }

                // set the new size of the control
                cell.Size = new Size((int)_cellWidth, (int)_cellHeight);
            });

            // remove the old controls
            controlsToRemove.ForEach(_ => Controls.Remove(_));

            // add the new cells
            int cellIndex = 0;
            _cells.ForEach(_ =>
            {
                var cell = _ as UserControl;

                // if the control is new, add it
                if (!Controls.Contains(cell))
                {
                    cell.Size = new Size((int)_cellWidth, (int)_cellHeight);
                    Controls.Add(cell);
                }

                RefreshCellPosition(cell, cellIndex);

                cellIndex++;
            });
        }

        /// <summary>
        /// Updates the given cell with the given index to its index based position.
        /// </summary>
        /// <param name="cell">the cell to update</param>
        /// <param name="i">the index of the cell</param>
        private void RefreshCellPosition(UserControl cell, int i)
        {
            if (_columns == 0) // for example, when the window is minimized
                return;

            var X = i % _columns * (CellWidth + CellMargin * 2) + CellMargin + AutoScrollPosition.X;
            var Y = i / _columns * (CellHeight + CellMargin) + CellMargin + AutoScrollPosition.Y;
            cell.Location = new Point((int)X, (int)Y);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            RefreshCells();
            //Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RefreshCells();
            //Invalidate();
        }

        protected override Point ScrollToControl(Control activeControl)
        {
            return this.AutoScrollPosition;
        }
    }
}
