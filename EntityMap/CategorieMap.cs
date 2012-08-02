using FluentNHibernate.Mapping;
using Entity;

namespace EntityMap
{
    public class CategorieMap : ClassMap<Categorie>
    {
        public CategorieMap()
        {
            Table("Categories"); 
            Id(x => x.CategoryID);
            Map(x => x.Name, "CategoryName");
            
            //Inverse 代表反转, 意思是Product作为维护双方关系的主控方 

            /*
             * 此处：cascade="all" 而不会造成所谓的“孤儿”问题是因为设置了 inverse="true" 
             * 他不会用update 而是直接用delte语句。             
             * ------------------------------------------------------------------------
             * 注： 在one-to-many关联中一般通过将many一方(如本例中的Products)作为维护双方关系的主控方，也就是在one(如本例中的Categorie)设置inverse="true"
             * -----------------------------------------------------------------------
             */
            HasMany(x => x.Products).Cascade.All().Inverse();  //一个种类有多个产品

            /*
             * 如果去掉Inverse(),也就是不设置inverse="true" 
             * 而还是将Cascade.All()（注：cascade="all"）
             * 那么在删除Categorie时，只会把Product的关联键（update 语句）设置为NULL,这就会造成所谓的“孤儿”
             * 注：如果没有加 inverse="true" 
             *    而要级联删除Categorie集合中的Product，则需要将Cascade.AllDeleteOrphan()
             */
            //HasMany(x => x.Products).Cascade.All();  //一个种类有多个产品

            // HasMany(x => x.Products); 
        }
    }
}