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
    //子类映射
    public class GoldOrderMap : SubclassMap<GoldOrder>
    {
        public GoldOrderMap()
        {
            Table("GoldOrders");
            KeyColumn("OrderID");
            Map(m => m.GoldCount);
            Map(m => m.CharacterName);
        }
    }
}