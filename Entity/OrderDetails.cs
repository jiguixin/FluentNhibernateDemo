namespace Entity
{
    public class OrderDetails
    { 
        public virtual int OrderDetailId { get; set; }
         
        public virtual Order order { get; set; }

        public virtual Product product { get; set; }

        public virtual decimal UnitPrice { get; set; }

        public virtual int Quantity { get; set; }

        #region 用复合组件必须重写Equals和GetHasCode方法


        //public override bool Equals(object obj)
        //{
        //    var parmOd= obj as OrderDetails;

        //    return parmOd != null && (parmOd.order.OrderID == order.OrderID && parmOd.product.ProductID == product.ProductID);
        //}
        //public override int GetHashCode()
        //{
        //    return order.GetHashCode() + product.GetHashCode() + UnitPrice.GetHashCode() + Quantity.GetHashCode();
        //}

        #endregion
    }
}