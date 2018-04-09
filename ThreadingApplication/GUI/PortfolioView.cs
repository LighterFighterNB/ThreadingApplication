using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThreadingApplication.GUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ThreadingApplication
{
    class PortfolioView : StateView
    {
        private TextBlock userBlock = new TextBlock();
        private TextBox currency = new TextBox();
        private TextBox owned = new TextBox();

        public PortfolioView()
        {

        }

        private void createContext(Grid context)
        {
            createColumns(context, 3);
            createRows(context, 6);

            TextBlock nameTitle = new TextBlock();
            nameTitle.Text = "Currency Name";
            Grid.SetRow(nameTitle, 0);
            Grid.SetColumn(nameTitle, 0);
            context.Children.Add(nameTitle);

            TextBlock ownedTitle = new TextBlock();
            ownedTitle.Text = "Currency Owned";
            Grid.SetRow(ownedTitle, 0);
            Grid.SetColumn(ownedTitle, 1);
            context.Children.Add(ownedTitle);

            TextBlock valueTitle = new TextBlock();
            valueTitle.Text = "Currency value";
            Grid.SetRow(valueTitle, 0);
            Grid.SetColumn(valueTitle, 2);
            context.Children.Add(valueTitle);

            for (int i = 0; i < 3; i++)
            {
                TextBlock currencyName = new TextBlock();
                currencyName.Text = "1";
                Grid.SetRow(currencyName, i + 1);
                Grid.SetColumn(currencyName, 0);
                context.Children.Add(currencyName);

                TextBlock currencyOwned = new TextBlock();
                currencyOwned.Text = "1";
                Grid.SetRow(currencyOwned, i + 1);
                Grid.SetColumn(currencyOwned, 1);
                context.Children.Add(currencyOwned);

                TextBlock currencyValue = new TextBlock();
                currencyValue.Text = "1";
                Grid.SetRow(currencyValue, i + 1);
                Grid.SetColumn(currencyValue, 2);
                context.Children.Add(currencyValue);
            }
        }

        private Grid createPlusPortfolio()
        {
            Grid grid = new Grid();
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
            add.Click += add_Click;

            Button cancel = new Button();
            cancel.Content = "Cancel";
            cancel.HorizontalAlignment = HorizontalAlignment.Left;
            cancel.VerticalAlignment = VerticalAlignment.Top;
            cancel.Margin = new Thickness(10);
            Grid.SetRow(cancel, 4);
            Grid.SetColumn(cancel, 2);
            grid.Children.Add(cancel);
            cancel.Click += cancel_Click;

            return grid;
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            String email = userBlock.Text;
            if (string.IsNullOrWhiteSpace(currency.Text) || !Regex.IsMatch(owned.Text, @"^[0-9]+.?[0-9]*$"))
            {
                createErrorMessage("The stock is not valid");
            }
        }

        private async void cancel_Click(object sender, RoutedEventArgs e)
        {
        }

        public override Grid getView(ViewManager viewer, ObjectPool objPool)
        {
            Grid grid = new Grid();
            grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
            createColumns(grid, 6);
            createRows(grid, 15);

            TextBlock title = new TextBlock();
            title.Text = "Profile";
            title.FontSize = 25;
            title.HorizontalAlignment = HorizontalAlignment.Right;
            title.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 1);
            grid.Children.Add(title);

            TextBlock name = new TextBlock();
            name.Text = "Name: ";
            name.FontSize = 15;
            name.HorizontalAlignment = HorizontalAlignment.Right;
            name.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(name, 1);
            Grid.SetColumn(name, 1);
            grid.Children.Add(name);

            TextBlock userName = new TextBlock();
            userName.Text = "name";
            userName.HorizontalAlignment = HorizontalAlignment.Left;
            userName.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(userName, 1);
            Grid.SetColumn(userName, 2);
            grid.Children.Add(userName);

            TextBlock sum = new TextBlock();
            sum.Text = "Total amount: ";
            sum.FontSize = 15;
            sum.HorizontalAlignment = HorizontalAlignment.Right;
            sum.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(sum, 1);
            Grid.SetColumn(sum, 3);
            grid.Children.Add(sum);

            TextBlock totalAmount = new TextBlock();
            totalAmount.Text = "amount";
            totalAmount.HorizontalAlignment = HorizontalAlignment.Left;
            totalAmount.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(totalAmount, 1);
            Grid.SetColumn(totalAmount, 4);
            grid.Children.Add(totalAmount);

            Button plus = new Button();
            plus.Content = "+";
            Grid.SetColumn(plus, 4);
            Grid.SetRow(plus, 15);
            grid.Children.Add(plus);

            Grid context = new Grid();
            createColumns(context, 3);
            createRows(context, 10);
            createContext(context);
            Grid.SetRow(context, 3);
            Grid.SetRowSpan(context, 10);
            Grid.SetColumn(context, 1);
            Grid.SetColumnSpan(context, 5);
            grid.Children.Add(context);

            return grid;
        }
    }
}
