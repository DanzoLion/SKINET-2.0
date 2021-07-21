using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) // example of full path/namespace: Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder
        {                                                                                                   // in this case entity is our product
            builder.Property(p => p.Id).IsRequired();                                               // accessing individual properties of the entity // expression used to access specific property we are looking for // then we configure the entity as we want it
           // builder.Property(p => p.Name).IsRequired().HasMaxLength(100);    // removed .HasMaxLength(100); for database migration from sqlite to PostgreSQL
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(b => b.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId );
            builder.HasOne(t => t.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
            // NB: this defines our columns via p =>, indicates single column type, and b + t indicate .HasOne relationship
        }
    }
}