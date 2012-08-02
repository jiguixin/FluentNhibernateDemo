using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class Product
    {
        public virtual int ProductID { get; set; }
         
        public virtual string Name { get; set; }

        public virtual string Unit { get; set; }

        public virtual decimal Price { get; set; }

        public virtual string Remark { get; set; }

        public virtual int Quantity { get; set; }

        public virtual Categorie categorie { get; set; }

        //public virtual IList<Order> order { get; set; }

        public virtual IList<OrderDetails> orderDetails { get; set; }
          
    }
}
