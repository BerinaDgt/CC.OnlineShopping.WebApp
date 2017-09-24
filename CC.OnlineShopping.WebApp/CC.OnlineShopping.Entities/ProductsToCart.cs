using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.OnlineShopping.Entities
{
    public class ProductsToCart
    {
        public int ID { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
