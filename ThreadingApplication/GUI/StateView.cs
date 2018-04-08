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

namespace ThreadingApplication
{
    abstract class StateView
    {
        private AlphaManager api;
        private Database db;
        protected ViewManager manager;
        public abstract Grid getView(ViewManager viewer);
        protected Grid current;

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

        protected void createMenu(Grid grid, ViewManager viewer)
        {
            Dictionary<string, string> imgs = new Dictionary<string, string>();
            imgs.Add("Dashboard", "home.png");
            imgs.Add("Converter", "coinstack.png");
            imgs.Add("Profile", "person3.png");
            imgs.Add("Settings", "settings.png");
            int i = 0;
            foreach (KeyValuePair<String, String> img in imgs)
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
                    grid.Children.Clear();
                    switch (img.Key)
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
        }
    }
}
