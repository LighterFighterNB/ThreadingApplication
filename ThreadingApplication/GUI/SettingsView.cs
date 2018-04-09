using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ThreadingApplication.GUI
{
    class SettingsView : StateView
    {
        Preference preference;
        public SettingsView()
        {
            preference = db.loadPreferences();
        }
        public override Grid getView(ViewManager viewer, ObjectPool objPool, ChartObjectPool chartPool)
        {
            Grid grid;

            Grid settingsGrid = new Grid();
            settingsGrid.Name = "settingsGrid";
            settingsGrid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 150, 180, 250));
            createColumns(settingsGrid, 2);
            createRows(settingsGrid, 25);
            createMenu(settingsGrid, viewer, objPool);
            Grid.SetColumn(settingsGrid, 0);

            if (!isCreated && objPool.getState("Settings") == null)
            {
                isCreated = !isCreated;
                grid = new Grid();
                grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col);

                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(20, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col1);

                Grid g = new Grid();
                createColumns(g, 20);
                createRows(g, 20);

                TextBlock title = new TextBlock();
                title.Text = "Settings";
                FontFamily f = new FontFamily("Arial");
                title.FontFamily = f;
                title.FontSize = 24;
                Grid.SetColumn(title, 4);
                Grid.SetRow(title, 2);
                Grid.SetColumnSpan(title, 7);
                Grid.SetRowSpan(title, 2);
                TextBlock blockCurrency = new TextBlock();
                blockCurrency.Text = "Prefered Currency: ";
                Grid.SetColumn(blockCurrency, 4);
                Grid.SetRow(blockCurrency, 6);
                Grid.SetColumnSpan(blockCurrency, 6);
                Grid.SetRowSpan(blockCurrency, 2);
                ComboBox preferedCurrency = new ComboBox();
                preferedCurrency.Items.Add("EUR");
                preferedCurrency.Items.Add("USD");
                preferedCurrency.Items.Add("RON");
                preferedCurrency.Items.Add("GBP");
                preferedCurrency.Items.Add("SGD");
                preferedCurrency.Items.Add("ZAR");
                preferedCurrency.Items.Add("CNY");
                preferedCurrency.SelectedItem = preference.getPreference("currency");
                Grid.SetColumn(preferedCurrency, 12);
                Grid.SetRow(preferedCurrency, 6);
                Grid.SetColumnSpan(preferedCurrency, 6);
                Grid.SetRowSpan(preferedCurrency, 2);

                TextBlock blockTheme = new TextBlock();
                blockTheme.Text = "Prefered Theme";
                Grid.SetColumn(blockTheme, 4);
                Grid.SetRow(blockTheme, 9);
                Grid.SetColumnSpan(blockTheme, 6);
                Grid.SetRowSpan(blockTheme, 2);

                ComboBox preferedTheme = new ComboBox();
                preferedTheme.Items.Add("Light");
                preferedTheme.Items.Add("Classic");
                preferedTheme.Items.Add("Dark");
                preferedTheme.SelectedItem = preference.getPreference("displayType");
                Grid.SetColumn(preferedTheme, 12);
                Grid.SetRow(preferedTheme, 9);
                Grid.SetColumnSpan(preferedTheme, 6);
                Grid.SetRowSpan(preferedTheme, 2);
                g.Children.Add(title);
                g.Children.Add(blockCurrency);
                g.Children.Add(preferedCurrency);
                g.Children.Add(blockTheme);
                g.Children.Add(preferedTheme);
                SolidColorBrush brush = new SolidColorBrush();
                brush.Opacity = 0;
                Button button3 = new Button();
                button3.Content = new Image
                {
                    Source = new BitmapImage(new Uri("ms-appx:///Assets/save.png")),
                    Stretch = Stretch.Fill
                }; ;
                button3.FontSize = 15;
                button3.Background = brush;
                button3.Click += delegate (object sender, RoutedEventArgs e)
                {
                    preference.changePreference("currency", preferedCurrency.SelectedItem.ToString());
                    preference.changePreference("displayType", preferedTheme.SelectedItem.ToString());
                    db.savePreferences(preference);
                    viewer.setCurrentView(viewer.getNextView());
                    //current = viewer.getCurrentView().getView(viewer, objPool);
                    viewer.updateMain();
                };
                Grid.SetColumn(button3, 4);
                Grid.SetRow(button3, 12);
                Grid.SetColumnSpan(button3, 2);
                Grid.SetRowSpan(button3, 2);
                g.Children.Add(button3);
                Grid.SetColumn(g, 1);
                grid.Children.Add(settingsGrid);
                grid.Children.Add(g);
                objPool.setObjectState("Settings", grid);
            }
            else
            { 
                grid  = objPool.getState("Settings");
                grid.Children.Remove(settingsGrid);
                grid.Children.Add(settingsGrid);
            }
            current = grid;
            return current;
        }
    }
}
