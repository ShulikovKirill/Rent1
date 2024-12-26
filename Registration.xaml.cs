using System.Windows;
using System.Windows.Input;

namespace test
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void LoginOrPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                
                string login = Username.Text;
                string password = Password.Password;
                string confpassword = ConfirmPassword.Password;

                
                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                {
                    Console.WriteLine(login + " " + password);
                    new CheckUser().CheckUsername(login, password, confpassword);
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите логин и пароль.");
                }
            }

        }

        private void Login_Button_Page_Click(object sender, RoutedEventArgs e)
        {
            MainWindow LoginWindow = new MainWindow();

            
            LoginWindow.Show();

            
            this.Close();
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            
            string username = Username.Text;
            string password = Password.Password;
            string confirmpassword = ConfirmPassword.Password;

           
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmpassword))
            {
                Console.WriteLine(username + " " + password);
                new CheckUser().CheckUsername(username, password, confirmpassword);
            }
            else
            {
                 MessageBox.Show("Пожалуйста, введите логин и пароль.");
            }

        }

        private void Guest_Button_Page_Click(object sender, RoutedEventArgs e)
        {
            GuestShopView guestShopView = new GuestShopView();

            guestShopView.Show();
            this.Close();
        }
    }
}
