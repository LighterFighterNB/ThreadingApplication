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
using Windows.UI.Xaml.Media.Imaging;

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

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            String email = userBlock.Text;
            if (string.IsNullOrWhiteSpace(currency.Text) || !Regex.IsMatch(owned.Text, @"^[0-9]+.?[0-9]*$"))
            {
                createErrorMessage("The stock is not valid");
            }
        }
        

        public override Grid getView(ViewManager viewer, ObjectPool objPool)
        {
            Grid grid2;
            Grid grid1 = new Grid();
            grid1.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 150, 180, 250));
            createColumns(grid1, 2);
            createRows(grid1, 25);
            createMenu(grid1, viewer, objPool);
            Grid.SetColumn(grid1, 0);
            if (objPool.getState("Portfolio") == null)
            {
                grid2 = new Grid();
                grid2.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                grid2.ColumnDefinitions.Add(col);

                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(20, GridUnitType.Star);
                grid2.ColumnDefinitions.Add(col1);

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
                    viewer.setCurrentView(new AddNewItemView());
                    current = viewer.getCurrentView().getView(viewer, objPool);
                    viewer.updateMain();
                };
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
                Grid.SetColumn(grid, 1);
                grid2.Children.Add(grid);
                grid2.Children.Add(grid1);
                objPool.setObjectState("Portfolio", grid2);
            }
            else
            {
                grid2 = objPool.getState("Portfolio");
                grid2.Children.Remove(grid1);
                grid2.Children.Add(grid1);
            }
            current = grid2;
            return current;
        }
    }
}
