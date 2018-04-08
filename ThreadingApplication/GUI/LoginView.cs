using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadingApplication.GUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ThreadingApplication
{
    class LoginView : StateView
    {
        public LoginView()
        {
        }
        public override Grid getView(ViewManager viewer)
        {
            viewer.setCurrentView(new LoginView());
            viewer.setNextView(new DashboardView());
            viewer.setPreviousView(new SignupView());
            Grid grid = new Grid();
            grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
            createColumns(grid, 4);
            createRows(grid, 7);

            TextBlock title = new TextBlock();
            title.Text = "Log in";
            title.FontSize = 25;
            title.HorizontalAlignment = HorizontalAlignment.Right;
            title.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 1);
            grid.Children.Add(title);

            TextBlock userBlock = new TextBlock();
            userBlock.Margin = new Thickness(5);
            userBlock.Text = "E-mail: ";
            userBlock.HorizontalAlignment = HorizontalAlignment.Center;
            userBlock.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(userBlock, 1);
            Grid.SetColumn(userBlock, 1);
            grid.Children.Add(userBlock);

            TextBox userEmail = new TextBox();
            userEmail.Margin = new Thickness(5);
            userEmail.HorizontalAlignment = HorizontalAlignment.Stretch;
            userEmail.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(userEmail, 1);
            Grid.SetColumn(userEmail, 2);
            grid.Children.Add(userEmail);

            TextBlock passBlock = new TextBlock();
            passBlock.Margin = new Thickness(5);
            passBlock.Text = "Password: ";
            passBlock.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(passBlock, 2);
            Grid.SetColumn(passBlock, 1);
            grid.Children.Add(passBlock);

            PasswordBox password = new PasswordBox
            {
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top
            };
            Grid.SetRow(password, 2);
            Grid.SetColumn(password, 2);
            grid.Children.Add(password);

            Button login = new Button();
            login.Content = "Login";
            login.HorizontalAlignment = HorizontalAlignment.Right;
            login.Margin = new Thickness(10);
            login.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(login, 3);
            Grid.SetColumn(login, 1);
            grid.Children.Add(login);
            login.Click += delegate (object sender, RoutedEventArgs e) {

                bool isCorrect = true;
                if (isCorrect)
                {
                    viewer.setCurrentView(viewer.getNextView());
                    current = viewer.getCurrentView().getView(viewer);
                    viewer.updateMain();
                }
            };

            Button signup = new Button();
            signup.Content = "Sign Up";
            signup.HorizontalAlignment = HorizontalAlignment.Left;
            signup.VerticalAlignment = VerticalAlignment.Top;
            signup.Margin = new Thickness(10);
            Grid.SetRow(signup, 3);
            Grid.SetColumn(signup, 2);
            grid.Children.Add(signup);
            signup.Click += delegate (object sender, RoutedEventArgs e) {

                viewer.setCurrentView(viewer.getPerviousView());
                current = viewer.getCurrentView().getView(viewer);
                viewer.updateMain();
            };
            manager = viewer;
            current = grid;
            return current;
        }
    }
}
