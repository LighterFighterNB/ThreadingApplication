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
        Dashboard dashboard;
        public DashboardView()
        {
            dashboard = db.loadDashboard("MyDashboard");
        }
     
        private void displayData(Grid grid)
        {

            dashboard = db.loadDashboard("MyDashboard");
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

            int i = 0;
            foreach(Chart chart in dashboard.getCharts())
            {
                Task t = new Task(async () => { 
                    TextBlock name = new TextBlock();
                    nameTitle.Text = chart.getName();
                    Grid.SetRow(nameTitle, 0);
                    Grid.SetColumn(nameTitle, 0);
                    grid.Children.Add(nameTitle);

                    TextBlock ownedTitle = new TextBlock();
                    ownedTitle.Text = chart.getFrom();
                    Grid.SetRow(ownedTitle, 0);
                    Grid.SetColumn(ownedTitle, 1);
                    grid.Children.Add(ownedTitle);
                    await chart.setStock();
                    TextBlock value = new TextBlock();
                    int p = 0;
                    while(chart.getLastStock() == null && p < 5)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        p++;
                    }
                    if (p < 4)
                    {
                        foreach (KeyValuePair<String, String> propriety in chart.getLastStock().Proprieties)
                        {
                            if (propriety.Key.Contains("close"))
                            {
                                value.Text = propriety.Value;
                                break;
                            }
                        }
                    }
                    else
                    {
                        createErrorMessage("Something went wrong with reading from API");
                    }
                    Grid.SetRow(value, 0);
                    Grid.SetColumn(value, 2);
                    grid.Children.Add(value);
                });
                t.Start();
            }
        }

        public override Grid getView(ViewManager viewer, ObjectPool objPool)
        {
            TextBlock title = new TextBlock();
            title.Text = "My Dashboard";
            title.FontSize = 23;
            Grid grid;
            Grid grid1 = new Grid();
            grid1.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 150, 180, 250));
            createColumns(grid1, 2);
            createRows(grid1, 25);
            createMenu(grid1, viewer, objPool);
            Grid.SetColumn(grid1, 0);
            grid = new Grid();
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
           // RowDefinition rowDefinition1 = new RowDefinition();
           // rowDefinition1.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition rowDefinition2 = new RowDefinition();
            rowDefinition2.Height = new GridLength(10, GridUnitType.Star);
            grid2.RowDefinitions.Add(rowDefinition);
           // grid2.RowDefinitions.Add(rowDefinition1);
            grid2.RowDefinitions.Add(rowDefinition2);

            Grid.SetColumn(grid2, 1);
            Grid.SetColumn(title, 2);
            Grid.SetRow(title, 0);
            Grid.SetColumnSpan(grid2, 3);
            grid2.Children.Add(title);

            ScrollViewer sv = new ScrollViewer();

            Grid data = new Grid();
            createColumns(data, 3);
            displayData(data);
            sv.Content = data;

            Grid.SetColumn(sv, 1);
            Grid.SetColumnSpan(sv, 3);
            Grid.SetRow(sv, 1);
            grid2.Children.Add(sv);


            grid.Children.Add(grid2);
            grid.Children.Add(grid1);
            objPool.setObjectState("Dashboard", grid);
            current = grid;
            return current;
        }
    }
}
