using FluentNHibernate.Mapping;
using Entity;
namespace EntityMap
{
    public class CustomerDetailMap : ClassMap<CustomerDetail>
    {
        public CustomerDetailMap()
        {
            Id(u => u.customerId).Column("CustomerId").GeneratedBy.Foreign("customer");

            Map(u => u.CustomerEmail).Nullable();
            Map(u => u.CreateTime).Nullable();
            Map(u => u.LastUpdated).Nullable();
            HasOne<Customer>(u => u.customer).Cascade.All().Constrained();
        }
    }
}