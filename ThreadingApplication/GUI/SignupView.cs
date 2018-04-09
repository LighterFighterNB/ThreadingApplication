using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ThreadingApplication.GUI
{
    class SignupView : StateView
    {
        private TextBlock userBlock;

        public SignupView()
        {
        }

        public override Grid getView(ViewManager viewer, ObjectPool objPool)
        {
            viewer.setCurrentView(this);
            viewer.setNextView(new LoginView());
            viewer.setPreviousView(new LoginView());

            if (!isCreated && objPool.getState("Signup") == null)
            {
                isCreated = !isCreated;
                Grid grid = new Grid();
                grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
                createColumns(grid, 4);
                createRows(grid, 7);

                TextBlock title = new TextBlock();
                title.Text = "Sign up";
                title.FontSize = 25;
                title.HorizontalAlignment = HorizontalAlignment.Right;
                title.VerticalAlignment = VerticalAlignment.Bottom;
                Grid.SetRow(title, 0);
                Grid.SetColumn(title, 1);
                grid.Children.Add(title);
                userBlock = new TextBlock();
                userBlock.Margin = new Thickness(5);
                userBlock.Text = "User e-mail: ";
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

                TextBlock user = new TextBlock();
                user.Margin = new Thickness(5);
                user.Text = "User name: ";
                user.HorizontalAlignment = HorizontalAlignment.Center;
                user.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(user, 2);
                Grid.SetColumn(user, 1);
                grid.Children.Add(user);

                TextBox userName = new TextBox();
                userName.Margin = new Thickness(5);
                userName.HorizontalAlignment = HorizontalAlignment.Stretch;
                userName.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(userName, 2);
                Grid.SetColumn(userName, 2);
                grid.Children.Add(userName);

                TextBlock passBlock = new TextBlock();
                passBlock.Margin = new Thickness(5);
                passBlock.Text = "Password: ";
                passBlock.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(passBlock, 3);
                Grid.SetColumn(passBlock, 1);
                grid.Children.Add(passBlock);

                PasswordBox password = new PasswordBox();
                password.Margin = new Thickness(5);
                password.HorizontalAlignment = HorizontalAlignment.Stretch;
                password.VerticalAlignment = VerticalAlignment.Top;
                Grid.SetRow(password, 3);
                Grid.SetColumn(password, 2);
                grid.Children.Add(password);

                Button signup = new Button();
                signup.Content = "Sign up";
                signup.HorizontalAlignment = HorizontalAlignment.Right;
                signup.VerticalAlignment = VerticalAlignment.Top;
                signup.Margin = new Thickness(10);
                Grid.SetRow(signup, 4);
                Grid.SetColumn(signup, 1);
                grid.Children.Add(signup);
                signup.Click += delegate (object sender, RoutedEventArgs e)
                {
                    String email = userEmail.Text;
                    if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                    {
                        createErrorMessage("The email is invalid");
                    }
                    else
                    {
                        db.createUser(userEmail.Text, userName.Text, password.Password.ToString());
                        db.addDashboard("MyDashboard");
                        db.addPortfolio("MyPortfolio");
                        db.addDefaultPreferences();
                        db.logout();
                        viewer.setCurrentView(viewer.getNextView());
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
                    viewer.setCurrentView(viewer.getNextView());
                    current = viewer.getCurrentView().getView(viewer, objPool);
                    viewer.updateMain();
                };
                objPool.setObjectState("Signup", grid);
                current = grid;
            }
            else
            {
                current = objPool.getState("Signup");
            }
            return current;
        }

        private async void createErrorMessage(String message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
        
    }
}
