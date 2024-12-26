using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static test.CheckUser;

namespace test
{
    public class CheckUser
    {    

        internal bool CheckLoginPassword(string login, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
                db.Users.Load();

                foreach (User user in db.Users)
                {
                    if (user.Username == login && user.Password == password)
                    {
                        return true; 
                    }
                }
                MessageBox.Show("Неверный логин или пароль");
                return false; 
                
            }
        }

        internal void CheckUsername(string login, string password, string confpassword)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
                db.Users.Load();

                bool userExists = false;
                foreach (User user in db.Users)
                {
                    if (user.Username == login)
                    {
                        userExists = true;
                        break;
                    }
                }

                if (userExists)
                {
                    MessageBox.Show("Такой пользователь уже существует");
                }
                else
                {
                    if (password == confpassword)
                    {
                        User newUser = new User { Username = login, Password = password };
                        db.Users.Add(newUser);
                        db.SaveChanges();
                        MessageBox.Show("Пользователь успешно зарегистрирован");
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают");
                    }
                }
            }
        }
    

    internal bool GetLoginPassword(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
                return false;
            }
        }
    }
}
