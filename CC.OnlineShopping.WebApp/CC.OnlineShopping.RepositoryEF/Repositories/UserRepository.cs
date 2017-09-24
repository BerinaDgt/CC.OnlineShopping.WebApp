using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.OnlineShopping.Interfaces;
using CC.OnlineShopping.Entities;

namespace CC.OnlineShopping.RepositoryEF.Repositories
{
    public class UserRepository : IUserRepository
    {
        DatabaseOnlineShopping db = new DatabaseOnlineShopping();

        public bool Create(User user)
        {
            user.Credits = 1000;
            db.Users.Add(user);
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var dbUser = GetById(id);

            if (dbUser != null)
            {
                db.Users.Remove(dbUser);
                db.SaveChanges();
                return true;
            }
            return true;
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }

        public bool Update(User user)
        {
            var dbUser = GetById(user.ID);

            if (dbUser != null)
            {
                dbUser.ConfirmPassword = user.ConfirmPassword;
                dbUser.Password = user.Password;
                dbUser.Credits = user.Credits;
                dbUser.Email = user.Email;
                dbUser.LastName = user.LastName;
                dbUser.Name = user.Name;

                db.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
