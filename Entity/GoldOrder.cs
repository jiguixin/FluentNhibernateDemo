namespace Entity
{
    public class GoldOrder:Order
    {
        public virtual int GoldCount { get; set; }

        public virtual string CharacterName { get; set; } 

    }
}