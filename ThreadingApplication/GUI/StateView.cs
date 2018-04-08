using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ThreadingApplication
{
    abstract class StateView
    {
        private Api api;
        private Database db;
        public abstract Grid getView();

        protected void createColumns(Grid grid, int columnsNumber)
        {
            for (int i = 0; i < columnsNumber; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col);
            }
        }

        protected void createRows(Grid grid, int rowsNumber)
        {
            for (int i = 0; i < rowsNumber; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(row);
            }
        }
    }
}
