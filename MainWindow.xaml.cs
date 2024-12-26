using Microsoft.EntityFrameworkCore;
using System.Text;
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
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        Product prod_db = new Product();
        public MainWindow()
        {
            InitializeComponent();
            new UsingDatabase().InitializeDatabase();


        }
        private void LoginOrPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string login = Login.Text;
                string password = Password.Password;
                if (login == "admin")
                {
                    if (new CheckUser().GetLoginPassword(login, password))
                    {
                        if (new CheckUser().CheckLoginPassword(login, password))
                        {
                            new AdminShopView().Show();
                            this.Close();
                        }
                    }
                }
                else
                {
                    if (new CheckUser().GetLoginPassword(login, password))
                    {
                        if (new CheckUser().CheckLoginPassword(login, password))
                        {
                            new UserShopView().Show();
                            this.Close();
                        }
                    }
                }
            }
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            if (login == "admin")
            {
                if(new CheckUser().GetLoginPassword(login, password))
                {
                    if(new CheckUser().CheckLoginPassword(login, password))
                    {
                        new AdminShopView().Show();
                        this.Close();
                    }
                }
            }
            else
            {
                if (new CheckUser().GetLoginPassword(login, password))
                {
                    if (new CheckUser().CheckLoginPassword(login, password))
                    {
                        new UserShopView().Show();
                        this.Close();
                    }
                }
            }
        }

        private void Button_Reg_Page(object sender, RoutedEventArgs e)
        {
            Registration registrationWindow = new Registration();

            registrationWindow.Show();
            this.Close();
        }

        private void Guest_Button_Page(object sender, RoutedEventArgs e)
        {
            GuestShopView guestShopView = new GuestShopView();

            guestShopView.Show();
            this.Close();
        }
    }
}