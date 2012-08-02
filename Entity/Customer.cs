using System.Collections.Generic;

namespace Entity
{
    public class Customer
    {
        public virtual int CustomerID { get; set; }

        public virtual string Name { get; set; }

        public virtual string ContactName { get; set; }

        public virtual CustomerContactInfo ContactInfo { get; set; }

        public virtual CustomerDetail Detail { get; set; }
         
        public virtual IList<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}