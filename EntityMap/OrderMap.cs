using FluentNHibernate.Mapping;
using Entity;

namespace EntityMap
{
    public class OrderMap:ClassMap<Order>
    {
        public OrderMap()
        {
            Table("Orders");

            Id(m => m.OrderID);

            Map(m => m.OrderDate);
            Map(m => m.SumMoney);
            Map(m => m.Comment);
            Map(m => m.Finished);
            Map(m => m.State).CustomType<OrderState>();  //映射枚举类型
             
            //在复合组件中:HasMany<OrderDetails>(m => m.details).Cascade.All(); 如果加.Inverse()就会报Unexpected row count: 0; expected: 1

            HasMany<OrderDetails>(m => m.details).Cascade.All().Inverse();

            /*
             * 不延迟加载,用 .Not.LazyLoad()
             */
            //HasManyToMany(m => m.products).Not.LazyLoad().Cascade.SaveUpdate().ParentKeyColumn("OrderID").ChildKeyColumn(
            //    "ProductID").Table("[Order Details]");  //订单与产品 是多对多，不延迟加载产品

            References(m => m.customer, "CustomerID");//多个订单对应一个客户
              
        }
    }
}