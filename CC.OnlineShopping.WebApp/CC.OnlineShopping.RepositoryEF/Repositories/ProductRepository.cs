using CC.OnlineShopping.Entities;
using CC.OnlineShopping.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.OnlineShopping.RepositoryEF.Repositories
{
    public class ProductRepository : IProductRepository
    {
        DatabaseOnlineShopping db = new DatabaseOnlineShopping();
        IUserRepository _userRepository = new UserRepository();

        public bool Create(Product product)
        {
            product.DateCreated = DateTime.Now;

            db.Products.Add(product);
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var dbProduct = GetById(id);

            if (dbProduct != null)
            {
                db.Products.Remove(dbProduct);
                db.SaveChanges();
                return true;
            }
            return true;
        }

        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public Product GetByCategory(int Category)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ProductId == id);
        }

        public bool Update(Product product)
        {
            var dbProduct = GetById(product.ProductId);

            if (dbProduct != null)
            {
                dbProduct.Title = product.Title;
                dbProduct.Price = product.Price;
                dbProduct.Image = product.Image;
                dbProduct.Category = product.Category;
                dbProduct.Sold = product.Sold;
                db.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
