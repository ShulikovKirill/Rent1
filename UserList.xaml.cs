using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace test
{
    /// <summary>
    /// Логика взаимодействия для UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        public UserList()
        {
            InitializeComponent();
            LoadUsers();
        }
        private void LoadUsers()
        {
            using (ApplicationContext db = new ApplicationContext()) 
            {
                db.Database.EnsureCreated();
                UsersPanel.Children.Clear();
                foreach (User user in db.Users)
                {
                    Border userCard = CreateUserCard(user);
                    UsersPanel.Children.Add(userCard);
                }
            }

        }
        private Border CreateUserCard(User user)
        {
            Border border = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                BorderBrush = new SolidColorBrush(Color.FromRgb(105, 103, 181)),
                BorderThickness = new Thickness(1),
                
            };

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

           
            StackPanel userInfo = new StackPanel();
            userInfo.Children.Add(new TextBlock
            {
                Text = $"Username: {user.Username}",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            });
            userInfo.Children.Add(new TextBlock
            {
                Text = $"Password: {user.Password}",
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 5),
                Foreground = Brushes.Gray
            });

            
            Button deleteButton = new Button
            {
                Content = "Удалить",
                Background = Brushes.Red,
                Foreground = Brushes.White,
                Width = 80,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(10)
            };

            deleteButton.Click += (s, e) => DeleteUser(user);

            Grid.SetColumn(userInfo, 0);
            Grid.SetColumn(deleteButton, 1);

            grid.Children.Add(userInfo);
            grid.Children.Add(deleteButton);
            border.Child = grid;

            return border;
        }
        private void DeleteUser(User user)
        {
            using (ApplicationContext db = new ApplicationContext())
            if (MessageBox.Show($"Вы уверены, что хотите удалить пользователя {user.Username}?",
            "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                LoadUsers();
            }
        }

    }
}
