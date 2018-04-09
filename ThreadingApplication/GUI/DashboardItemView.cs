using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ThreadingApplication.GUI
{
    class DashboardItemView : StateView
    {
        private TextBox chartName = new TextBox();

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(chartName.Text))
            {
                createErrorMessage("The chart name is not valid");
            }
        }

        private async void cancel_Click(object sender, RoutedEventArgs e)
        {
        }

        public override Grid getView(ViewManager viewer, ObjectPool objPool)
        {
            Grid grid = new Grid();
            grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
            createColumns(grid, 4);
            createRows(grid, 7);

            TextBlock title = new TextBlock();
            title.Text = "Add new dashboard item";
            title.FontSize = 25;
            title.HorizontalAlignment = HorizontalAlignment.Right;
            title.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(title, 1);
            Grid.SetColumn(title, 1);
            grid.Children.Add(title);

            TextBlock name = new TextBlock();
            name.Text = "Name: ";
            name.HorizontalAlignment = HorizontalAlignment.Center;
            name.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(name, 2);
            Grid.SetColumn(name, 1);
            grid.Children.Add(name);

            chartName.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(chartName, 2);
            Grid.SetColumn(chartName, 2);
            grid.Children.Add(chartName);

            TextBlock currencyName = new TextBlock();
            currencyName.Text = "Currency: ";
            currencyName.HorizontalAlignment = HorizontalAlignment.Center;
            currencyName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(currencyName, 3);
            Grid.SetColumn(currencyName, 1);
            grid.Children.Add(currencyName);

            ComboBox currency = new ComboBox();
            currency.Items.Add("BTC");
            currency.Items.Add("ETH");
            currency.Items.Add("XRP");
            currency.Items.Add("BCH");
            currency.Items.Add("LTC");
            currency.Items.Add("EOS");
            currency.Items.Add("ADA");
            currency.Items.Add("XLM");
            currency.Items.Add("NEO");
            currency.Items.Add("XMR");
            currency.SelectedIndex = 0;
            currency.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(currency, 3);
            Grid.SetColumn(currency, 2);
            grid.Children.Add(currency);

            TextBlock refreshRate = new TextBlock();
            refreshRate.Text = "Refrash rate: ";
            refreshRate.HorizontalAlignment = HorizontalAlignment.Center;
            refreshRate.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(refreshRate, 4);
            Grid.SetColumn(refreshRate, 1);
            grid.Children.Add(refreshRate);

            ComboBox rate = new ComboBox();
            rate.Items.Add("Daily");
            rate.Items.Add("Weekly");
            rate.SelectedIndex = 0;
            rate.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(rate, 4);
            Grid.SetColumn(rate, 2);
            grid.Children.Add(rate);

            Button add = new Button();
            add.Content = "Add";
            add.HorizontalAlignment = HorizontalAlignment.Center;
            add.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(add, 5);
            Grid.SetColumn(add, 1);
            grid.Children.Add(add);
            add.Click += add_Click;

            Button cancel = new Button();
            cancel.Content = "Cancel";
            cancel.HorizontalAlignment = HorizontalAlignment.Center;
            cancel.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(cancel, 5);
            Grid.SetColumn(cancel, 2);
            grid.Children.Add(cancel);
            add.Click += cancel_Click;

            return grid;
        }
    }
}
