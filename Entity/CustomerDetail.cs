using System;

namespace Entity
{
    public class CustomerDetail
    {
        public virtual int customerId { get; set; }
        public virtual string CustomerEmail { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime LastUpdated { get; set; }
        public virtual Customer customer { get; set; }
 
    }
}