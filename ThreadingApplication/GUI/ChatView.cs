using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadingApplication.Elements;
using ThreadingApplication.GUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace ThreadingApplication
{
    class ChatView : StateView
    {
        private void showCharts(Grid grid)
        {

            List<Stock> listStock = new List<Stock>();
            for (int i = 0; i < 10; i++)
            {
                Stock stock = new Stock("2018-04-0" + i);
                stock.Proprieties.Add("2a. high (CNY)", "4661" + i + ".45295800");
                stock.Proprieties.Add("3a. low (CNY)", "4295" + i + ".27506900");
                stock.Proprieties.Add("4a. close (CNY)", "4300" + i + ".13619260");
                listStock.Add(stock);
            }
            LoadChartContents(listStock, grid);
        }

        private void LoadChartContents(List<Stock> listStock, Grid grid)
        {
            var chart = new WinRTXamlToolkit.Controls.DataVisualization.Charting.Chart();
            chart.Name = "LineChart";
            chart.Margin = new Thickness(100);

            var LineChart = new WinRTXamlToolkit.Controls.DataVisualization.Charting.LineSeries();
            LineChart.Name = "Chart";
            LineChart.Margin = new Thickness(2);
            LineChart.IndependentValuePath = "Date";
            LineChart.DependentValuePath = "Amount";
            LineChart.IsSelectionEnabled = true;

            chart.Series.Add(LineChart);

            List<Chart> Source = new List<Chart>();
            foreach (Stock item in listStock)
            {
                foreach (KeyValuePair<string, string> entry in item.Proprieties)
                {
                    if (entry.Key.Contains("close"))
                    {
                        Source.Add(new Chart() { Date = item.Date, Amount = Double.Parse(entry.Value) });
                    }
                }
            }

            (chart.Series[0] as LineSeries).ItemsSource = Source;
            grid.Children.Add(chart);
        }

        public override Grid getView(ViewManager viewer, ObjectPool objPool)
        {
            Grid grid;
            Grid grid1 = new Grid();
            grid1.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 150, 180, 250));
            createColumns(grid1, 2);
            createRows(grid1, 25);
            createMenu(grid1, viewer, objPool);
            Grid.SetColumn(grid1, 0);
            if (objPool.getState("Dashboard") == null)
            {
                grid = new Grid();
                grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col);

                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(20, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col1);

                Grid grid2 = new Grid();
                createColumns(grid2, 2);
                createRows(grid2, 1);
                showCharts(grid2);
                Grid.SetColumn(grid2, 1);

                grid.Children.Add(grid1);
                grid.Children.Add(grid2);

                objPool.setObjectState("Dashboard", grid);
            }
            else
            {
                grid = objPool.getState("Dashboard");
                grid.Children.Remove(grid1);
                grid.Children.Add(grid1);
            }
            current = grid;
            return grid;
        }
    }
}
