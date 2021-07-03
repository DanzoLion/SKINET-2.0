using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config{

    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>              // we bring in the interface IEntityTypeConfiguration and we configure <OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, io => {io.WithOwner();});

            builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
        }
    }
}