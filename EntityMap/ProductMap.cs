using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;

namespace EntityMap
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Products");

            Id(m => m.ProductID);
            Map(m => m.Name, "ProductName");
            Map(m => m.Price, "UnitPrice");
            Map(m => m.Unit);
            Map(x => x.Remark);
            Map(x => x.Quantity);

            #region 对类别的映射
            
            /*             
             * 如果把Cascade.SaveUpdate()就只支持插入和更新，不支持级联删除，如 :在新建一个“Categorie”与“Product”一起添加 可以直接保存Product就可以了，而Categories其实也自动保存成功了， 也可以将product添加到Categorie集合中，只保存Categorie就OK了。而在删除产品时，他不会把类别给删除掉， 这才是我们想要的，我们不可能删除一个产品，结果把整个类别都给删除了，
             * 如果Cascade.All() 在删除产品时，他会把该类别及类别下的其它产品都给删除了。
             * 参考：Program->CreateCategorieAndProduct()，Program->CreateCategorieAndProduct2() 
             */
            References(x => x.categorie).Column("CategoryID").Cascade.SaveUpdate(); // 多个产品对应一个类别

            /*
             * 不加Cascade.All()就必须先保存Categorie再保存Product
             * 参考:Program->CreateCategorieAndProduct1()   
             */
            //References(x => x.categorie).Column("CategoryID"); // 多个产品对应一个类别
            #endregion

            #region 对订单的映射

            //加Cascade.All() 此处要报UPDATE错误,  
            //HasManyToMany(x => x.order).Cascade.SaveUpdate().ParentKeyColumn("ProductID").ChildKeyColumn("OrderID").Table("[Order Details]");

            //不加Cascade.All() 如果删除产品，他只会删除产品，与订单详情，不会删除订单数据 因为是多对多关系。
            //HasManyToMany(x => x.order).ParentKeyColumn("ProductID").ChildKeyColumn("OrderID").Table("[Order Details]");

            #endregion

            #region 对订单详情的映射

            HasMany(x => x.orderDetails).Cascade.All().Inverse();

            #endregion
        }
    }
}
