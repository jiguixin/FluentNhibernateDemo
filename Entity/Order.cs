using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class Order
    {
        public virtual int OrderID { get; set; }

        public virtual OrderState State { get; set; }

        public virtual Customer customer { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual decimal SumMoney { get; set; }

        public virtual string Comment { get; set; }

        public virtual bool Finished { get; set; }

        //public virtual IList<Product> products { get; set; }

        public virtual IList<OrderDetails> details { get; set; }

        public Order()
        {
            details = new List<OrderDetails>();
        }
    } 
}
