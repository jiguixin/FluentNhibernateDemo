using Entity;
using FluentNHibernate.Mapping;

namespace EntityMap
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("Customers");
            Id(m => m.CustomerID).GeneratedBy.Identity();
            ;
            Map(m => m.Name, "CustomerName");
            Map(m => m.ContactName);
            //Map(m => m.Address);
            //Map(m => m.PostalCode);
            //Map(m => m.Tel);

            //映射嵌套类
            Component<CustomerContactInfo>(m => m.ContactInfo, a =>
                                                                   {
                                                                       a.Map(ci => ci.Address);
                                                                       a.Map(ci => ci.PostalCode);
                                                                       a.Map(ci => ci.Tel); 
                                                                   });

            HasOne<CustomerDetail>(m => m.Detail).Cascade.All();

            /*
            * 如果不加Inverse(),在删除客户时，客户所对应的订单，就会先将OrderDetail 的OrderId UPDATE 为NULL,
            * 而外键是不为空，所以就会报错“不能将值 NULL 插入列 'OrderID'，表 'MyNorthwind.dbo.Order Details'。”
            * 
            */
            HasMany(m => m.Orders).Cascade.All().Inverse(); // 一个客户有多个订单

           
        }
    }
}