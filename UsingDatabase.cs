using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class UsingDatabase
    {
      
        internal void InitializeDatabase()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
               
                bool adminExists = false;

                foreach (var user in db.Users)
                {
                    if (user.Username == "admin")
                    {
                        adminExists = true;
                        break;
                    }
                }
                if (!adminExists)
                {
                    User admin = new User { Username = "admin", Password = "admin" };
                    db.Users.Add(admin);
                    db.SaveChanges();
                }
            }
        }

    }
}
