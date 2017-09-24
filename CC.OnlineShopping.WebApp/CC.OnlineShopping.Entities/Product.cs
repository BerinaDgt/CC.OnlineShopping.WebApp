using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.OnlineShopping.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public int? Sold { get; set; }
        public DateTime? DateCreated { get; set; }
        
        public virtual int UserId{ get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<ProductsToCart> ProductsToCart { get; set; }

        public string Category { get; set; }
    }


    //public enum ProductCategory
    //{
    //    Clothes = 0,
    //    Shoes = 1,
    //    Accessories = 2
    //}
}

