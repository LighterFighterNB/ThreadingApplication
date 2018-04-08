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
using Windows.UI.Xaml.Media.Imaging;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace ThreadingApplication
{
    class DashboardView : StateView
    {
        public DashboardView()
        {
        }
        
        private void createMenu(Grid grid, ViewManager viewer)
        {
            //List<string> imgs = new List<string>();
            //imgs.Add("home.png");
            //imgs.Add("coinstack.png");
            //imgs.Add("person3.png");
            //imgs.Add("settings.png");
            Dictionary<string, string> imgs = new Dictionary<string, string>();
            imgs.Add("Dashboard", "home.png");
            imgs.Add("Converter", "coinstack.png");
            imgs.Add("Profile", "person3.png");
            imgs.Add("Settings", "settings.png");
            int i = 0;
            foreach(KeyValuePair<String, String> img in imgs)
            {
                Button button = new Button();
                button.Name = img.Key;
                SolidColorBrush scb = new SolidColorBrush();
                scb.Opacity = 0;
                button.Content = new Image
                {
                    Source = new BitmapImage(new Uri("ms-appx:///Assets/" + img.Value)),
                    Stretch = Stretch.Fill
                };
                button.RequestedTheme = ElementTheme.Default;
                button.Background = scb;
                button.Click += delegate (object sender, RoutedEventArgs e)
                {
                    switch(img.Key)
                    {
                        case "Dashboard":
                            viewer.setCurrentView(new DashboardView());
                            current = viewer.getCurrentView().getView(viewer);
                            viewer.updateMain();
                            break;
                        case "Converter":
                            viewer.setCurrentView(new ConverterView());
                            current = viewer.getCurrentView().getView(viewer);
                            viewer.updateMain();
                            break;
                        case "Profile":
                            viewer.setCurrentView(new PortfolioView());
                            current = viewer.getCurrentView().getView(viewer);
                            viewer.updateMain();
                            break;
                        case "Settings":
                            viewer.setCurrentView(new SettingsView());
                            current = viewer.getCurrentView().getView(viewer);
                            viewer.updateMain();
                            break;
                    }
                };
                Grid.SetColumn(button, 0);
                Grid.SetRow(button, i * 3);
                Grid.SetRowSpan(button, 2);
                Grid.SetColumnSpan(button, 2);
                grid.Children.Add(button);
                i++;
            }
            //for (int i = 0; i < imgs.Count; i++)
            //{
            //    Button button = new Button();
            //    SolidColorBrush scb = new SolidColorBrush();
            //    scb.Opacity = 0;
            //    button.Content = new Image
            //    {
            //        Source = new BitmapImage(new Uri("ms-appx:///Assets/" + imgs[i])),
            //        Stretch = Stretch.Fill
            //    };
            //    button.RequestedTheme = ElementTheme.Default;
            //    button.Background = scb;
            //    Grid.SetColumn(button, 0);
            //    Grid.SetRow(button, i * 3);
            //    Grid.SetRowSpan(button, 2);
            //    Grid.SetColumnSpan(button, 2);
            //    grid.Children.Add(button);
            //}
        }

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

        public override Grid getView(ViewManager viewer)
        {

            Grid grid = new Grid();
            ColumnDefinition col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(20, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col1);

            Grid grid1 = new Grid();
            grid1.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
            createColumns(grid1, 2);
            createRows(grid1, 25);
            createMenu(grid1, viewer);
            grid.Children.Add(grid1);
            Grid.SetColumn(grid1, 0);

            Grid grid2 = new Grid();
            createColumns(grid2, 2);
            createRows(grid2, 1);
            showCharts(grid2);
            grid.Children.Add(grid2);
            Grid.SetColumn(grid2, 1);
            current = grid;
            return current;
        }
    }
}
