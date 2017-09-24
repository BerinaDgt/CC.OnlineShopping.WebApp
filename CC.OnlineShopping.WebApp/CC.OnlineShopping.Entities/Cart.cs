using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.OnlineShopping.Entities
{
    public class Cart
    {

        [Key, ForeignKey("User")]
        public int ID { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<ProductsToCart> ProductsToCart { get; set; }
    }
}
