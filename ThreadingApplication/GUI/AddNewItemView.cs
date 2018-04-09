using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ThreadingApplication.GUI
{
    class AddNewItemView : StateView
    {
        private object currency;

        public AddNewItemView()
        {
        }

        public override Grid getView(ViewManager viewer, ObjectPool objPool)
        {

            Grid grid = new Grid();
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
            TextBox owned = new TextBox();
            grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 150, 180, 255));
            createColumns(grid, 4);
            createRows(grid, 7);

            TextBlock title = new TextBlock();
            title.Text = "Add new criptocurrency";
            title.FontSize = 25;
            title.HorizontalAlignment = HorizontalAlignment.Right;
            title.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 1);
            grid.Children.Add(title);

            TextBlock currencyName = new TextBlock();
            currencyName.Margin = new Thickness(5);
            currencyName.Text = "Currency name: ";
            currencyName.HorizontalAlignment = HorizontalAlignment.Center;
            currencyName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(currencyName, 2);
            Grid.SetColumn(currencyName, 1);
            grid.Children.Add(currencyName);

            currency.Margin = new Thickness(5);
            currency.HorizontalAlignment = HorizontalAlignment.Stretch;
            currency.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(currency, 2);
            Grid.SetColumn(currency, 2);
            grid.Children.Add(currency);

            TextBlock stock = new TextBlock();
            stock.Margin = new Thickness(5);
            stock.Text = "Stock: ";
            stock.HorizontalAlignment = HorizontalAlignment.Center;
            stock.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(stock, 3);
            Grid.SetColumn(stock, 1);
            grid.Children.Add(stock);

            owned.Margin = new Thickness(5);
            owned.HorizontalAlignment = HorizontalAlignment.Stretch;
            owned.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(owned, 3);
            Grid.SetColumn(owned, 2);
            grid.Children.Add(owned);

            Button add = new Button();
            add.Content = "Add";
            add.HorizontalAlignment = HorizontalAlignment.Left;
            add.VerticalAlignment = VerticalAlignment.Top;
            add.Margin = new Thickness(10);
            Grid.SetRow(add, 4);
            Grid.SetColumn(add, 1);
            grid.Children.Add(add);
            add.Click += delegate (object sender, RoutedEventArgs e)
            {

                if (string.IsNullOrWhiteSpace(owned.Text) || !Regex.IsMatch(owned.Text, @"^[0-9]+.?[0-9]*$") || string.IsNullOrWhiteSpace(currency.SelectedItem.ToString()))
                {
                    createErrorMessage("The stock is not valid");
                }
                else
                {
                    db.addCurrency("MyPortfolio", currency.SelectedItem.ToString(), Convert.ToDouble(owned.Text.ToString()));
                    viewer.setCurrentView(new PortfolioView());
                    current = viewer.getCurrentView().getView(viewer, objPool);
                    viewer.updateMain();
                }
            }; 

            Button cancel = new Button();
            cancel.Content = "Cancel";
            cancel.HorizontalAlignment = HorizontalAlignment.Left;
            cancel.VerticalAlignment = VerticalAlignment.Top;
            cancel.Margin = new Thickness(10);
            Grid.SetRow(cancel, 4);
            Grid.SetColumn(cancel, 2);
            grid.Children.Add(cancel);
            cancel.Click += delegate (object sender, RoutedEventArgs e)
            {
                    viewer.setCurrentView(new PortfolioView());
                    current = viewer.getCurrentView().getView(viewer, objPool);
                    viewer.updateMain();
            };

            return grid;
        }
    }
}
