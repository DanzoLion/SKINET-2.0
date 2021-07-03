using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.SeedData {
    public class StoreContextSeed // static allows us to use  a new class without us creating a new instance of the class
    {

        public static async Task SeedAsync (StoreContext context, ILoggerFactory loggerFactory) {
            try // we're running the seed method from our program.cs so we don't have global exception handling here
            {
                if (!context.ProductBrands.Any ()) // conditionals for product brands // we also do the same for productTypes and productLists ..
                {
                    var brandsData = File.ReadAllText ("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>> (brandsData); // deserializes into a list of our products with brandsData

                    foreach (var item in brands) {
                        context.ProductBrands.Add (item); // context tracks everything we add to our product brands
                    }

                    await context.SaveChangesAsync (); // saves all of our product brands into our database
                }
            

            if (!context.ProductTypes.Any ()) {
                var typesData = File.ReadAllText ("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>> (typesData); // deserializes into a list of our products with brandsData

                foreach (var item in types) {
                    context.ProductTypes.Add (item); // context tracks everything we add to our product brands
                }

                await context.SaveChangesAsync (); // saves all of our product brands into our database
            }

            if (!context.Products.Any ()) {
                var productsData = File.ReadAllText ("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>> (productsData); // deserializes into a list of our products with brandsData

                foreach (var item in products) {
                    context.Products.Add (item); // context tracks everything we add to our product brands
                }

                await context.SaveChangesAsync (); // saves all of our product brands into our database
            }

            if (!context.DeliveryMethods.Any ()) {  // we implement this after we create our OrderAggregate entities context
                
                var dmData = File.ReadAllText ("../Infrastructure/Data/SeedData/delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>> (dmData); 

                foreach (var item in methods) {
                    context.DeliveryMethods.Add (item); 
                }

                await context.SaveChangesAsync ();
            }
        }

        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}
}
