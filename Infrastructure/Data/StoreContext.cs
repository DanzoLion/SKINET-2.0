using System;
using System.Linq;
using System.Reflection;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;  //DbContext
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext                                                                                       // DbContext is combination of unit of work and repository patterns  // we abstract our database away from our code .. we don't query our database directly
    {
        public StoreContext( DbContextOptions<StoreContext> options) : base(options)    
        {                                                                                                                                            // we need this constructor to provide options, in this case connection string -> passes the options up inot base(options) constructor
        }

        public DbSet<Product> Products {get; set;}                                                                       // allows us to query our entities from database
        public DbSet<ProductBrand> ProductBrands {get; set;}                                                                       // allows us to query our entities from database
        public DbSet<ProductType> ProductTypes {get; set;}                                                                       // allows us to query our entities from database
        public DbSet<Order> Orders {get; set;}                                                                                      // Order | OrderItem | DeliveryMethod all created within OrderAggregate to create order entities
        public DbSet<OrderItem> OrderItems {get; set;}                                                                       // 
        public DbSet<DeliveryMethod> DeliveryMethods {get; set;}                                                      // 

        protected override void OnModelCreating(ModelBuilder modelBuilder)              // override the method here and look for our configurations
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());          // this builds our tables after we configure them in ProductConfiguration.cs

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")            // string so careful with typo error
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(properties => properties.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)); 

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}

