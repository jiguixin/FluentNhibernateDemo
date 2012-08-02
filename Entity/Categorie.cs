using System.Collections.Generic;

namespace Entity
{
    public class Categorie
    {
        public virtual int CategoryID { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}