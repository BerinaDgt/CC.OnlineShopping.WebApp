using CC.OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.OnlineShopping.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int id);
        Product GetByCategory(int Category);
        bool Create(Product product);
        bool Update(Product product);
        bool Delete(int id);
    }
}
