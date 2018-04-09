using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadingApplication.Elements;
using ThreadingApplication.GUI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace ThreadingApplication
{
    class DashboardView : StateView
    {
        Dashboard dashboard;
        public DashboardView()
        {
            dashboard = db.loadDashboard("MyDashboard");
        }

        private async void displayData(Grid grid, ViewManager viewer)
        {
            dashboard = db.loadDashboard("MyDashboard");
            if (dashboard.getCharts().Count < 5)
            {
                for(int k = 0; k < 15; k++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(20);
                    grid.RowDefinitions.Add(row);
                }
            }
            else
            {
                createRows(grid, (dashboard.getCharts().Count + 1) * 3);

                for (int k = 0; k < (dashboard.getCharts().Count + 1) * 3; k++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(20);
                    grid.RowDefinitions.Add(row);
                }
            }
            TextBlock nameTitle = new TextBlock();
            nameTitle.Text = "Chart Name";
            Grid.SetRow(nameTitle, 0);
            Grid.SetColumn(nameTitle, 0);
            grid.Children.Add(nameTitle);

            TextBlock cryptoTitle = new TextBlock();
            cryptoTitle.Text = "Currency";
            Grid.SetRow(cryptoTitle, 0);
            Grid.SetColumn(cryptoTitle, 1);
            grid.Children.Add(cryptoTitle);

            TextBlock valueTitle = new TextBlock();
            valueTitle.Text = "Currency value";
            Grid.SetRow(valueTitle, 0);
            Grid.SetColumn(valueTitle, 2);
            grid.Children.Add(valueTitle);

            Debug.WriteLine(dashboard.getCharts().Count);

            dashboard.update();
            int i = 1;
            foreach (Chart chart in dashboard.getCharts())
            {
                Button name = new Button();
                name.Content = chart.getName();
                Grid.SetRow(name, i * 3);
                Grid.SetColumn(name, 0);
                grid.Children.Add(name);
                name.VerticalAlignment = VerticalAlignment.Top;
                name.Click += delegate (object sender, RoutedEventArgs e)
                {
                    viewer.setCurrentView(new ChartView(chart));
                    //current = viewer.getCurrentView().getView(viewer, objPool);
                    viewer.updateMain();
                };

                TextBlock ownedTitle = new TextBlock();
                ownedTitle.Text = chart.getFrom();
                Grid.SetRow(ownedTitle, i * 3);
                Grid.SetColumn(ownedTitle, 1);
                grid.Children.Add(ownedTitle);
                ownedTitle.VerticalAlignment = VerticalAlignment.Top;
                TextBlock value = new TextBlock();

                int p = 0;
                while(chart.getLastStock() == null && p < 20)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    p++;
                }

                if (chart.getLastStock() != null)
                {
                    foreach (KeyValuePair<String, String> propriety in chart.getLastStock().Proprieties)
                    {
                        if (propriety.Key.Contains("close"))
                        {
                            value.Text = propriety.Value;
                            chart.resetChart();
                            break;
                        }
                    }
                }
                else
                {
                    //createErrorMessage("Something went wrong with reading from API");
                }
                Grid.SetRow(value, i * 3);
                Grid.SetColumn(value, 2);
                value.VerticalAlignment = VerticalAlignment.Top;
                grid.Children.Add(value);
                i++;
            }
        }

        public override Grid getView(ViewManager viewer, ObjectPool objPool)
        {
            dashboard = db.loadDashboard("MyDashboard");
            TextBlock title = new TextBlock();
            title.Text = "My Dashboard";
            title.FontSize = 23;
            Grid grid = new Grid();
            Grid grid1 = new Grid();
            grid1.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 150, 180, 250));
            createColumns(grid1, 2);
            createRows(grid1, 25);
            createMenu(grid1, viewer, objPool);
            Grid.SetColumn(grid1, 0);
            grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
            ColumnDefinition col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(20, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col1);

            Grid grid2 = new Grid();
            createColumns(grid2, 5);
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition rowDefinition2 = new RowDefinition();
            rowDefinition2.Height = new GridLength(10, GridUnitType.Star);
            RowDefinition rowDefinition1 = new RowDefinition();
            rowDefinition1.Height = new GridLength(1, GridUnitType.Star);
            grid2.RowDefinitions.Add(rowDefinition);
            grid2.RowDefinitions.Add(rowDefinition2);
            grid2.RowDefinitions.Add(rowDefinition1);

            Grid.SetColumn(grid2, 1);
            Grid.SetColumn(title, 2);
            Grid.SetRow(title, 0);
            Grid.SetColumnSpan(grid2, 3);
            grid2.Children.Add(title);

            ScrollViewer sv = new ScrollViewer();

            Grid data = new Grid();
            createColumns(data, 3);

            //Task t = new Task(() =>
            //{
            //    displayData(data);
            //});
            //t.Start();
            displayData(data,viewer);
            sv.Content = data;

            Grid.SetColumn(sv, 1);
            Grid.SetColumnSpan(sv, 3);
            Grid.SetRow(sv, 1);
            grid2.Children.Add(sv);
            SolidColorBrush scb = new SolidColorBrush();
            scb.Opacity = 50;
            Button plus = new Button();
            plus.Content = new Image
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/add.png")),
                Stretch = Stretch.Fill
            };
            plus.Background = scb;
            plus.Margin = new Thickness(75, -5, 75, -5);
            plus.HorizontalAlignment = HorizontalAlignment.Center;
            plus.VerticalAlignment = VerticalAlignment.Center;
            plus.Click += delegate (object sender, RoutedEventArgs e)
            {
                viewer.setCurrentView(new DashboardItemView());
                current = viewer.getCurrentView().getView(viewer, objPool);
                viewer.updateMain();
            };
            Grid.SetRow(plus, 2);
            Grid.SetColumn(plus, 3);

            grid2.Children.Add(plus);

            grid.Children.Add(grid2);
            grid.Children.Add(grid1);
            current = grid;
            return current;
        }
    }
}
