using Entity; 
using FluentNHibernate.Mapping;

namespace EntityMap
{
    public class OrderDetailMap:ClassMap<OrderDetails>
    {
        public OrderDetailMap()
        {
            Table("[Order Details]");

            #region 没有主键的情况下，用复合组件的方式，注：不推荐就这种方式
        http://nhforge.org/blogs/nhibernate/archive/2010/06/30/nhibernate-and-composite-keys.aspx
            //Id(m => m.Quantity).GeneratedBy.Assigned(); //没有主键，用这种方式,能查询，但是在插入时就有问题。因为他要查询会引用这个属性作为where条件。

            /*
             * 用的是复合主键
             * 如果在该类中定义了，如，OrderId、ProductId，那么就可以用
            CompositeId().KeyProperty(x => x.order, "OrderID").KeyProperty(x => x.product, "ProductID");
             * 
             * 作为符合主键，XML映射为：
             * <composite-id> 
             * <key-property name="OrderId "/> 
             * <key-property name="ProductId"/> 
             * </composite-id> 
             * -------------------------            
             * 而我这边是定义的2个对象所以就用KeyReference实现，
                <composite-id mapped="false" unsaved-value="undefined">
                  <key-many-to-one name="order" class="Entity.Order, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
                    <column name="OrderID" />
                  </key-many-to-one>
                  <key-many-to-one name="product" class="Entity.Product, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
                    <column name="ProductID" />
                  </key-many-to-one>
                </composite-id>
             * ---------------------------
             * 完成了上面的定义必须重写OrderDetail的Equals、GetHashCode方法。
             * 
             */
            //CompositeId().KeyReference(x => x.order, "OrderID").KeyReference(x => x.product, "ProductID");
             
            #endregion

            Id(m => m.OrderDetailId);

            Map(m => m.UnitPrice);
            Map(m => m.Quantity);

            /*
             * 
             * 用复合组件时，References(m => m.order, "OrderID")，如果不加 Not.Insert()就会报，“此 SqlParameterCollection 的 Count=4 的索引 4 无效。”             
             * 在插入订单时就会报错，
             * 不加Not.Update（）更新是没有问题的。      
             *        
             */
            References(m => m.order, "OrderID");

            References(m => m.product, "ProductID");
             

        }
    }
}