using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
           var host = CreateHostBuilder(args).Build();
           using (var scope = host.Services.CreateScope())                      // using is a run once only conditional where methods run here are disposed of immediately after use
           {
               var services = scope.ServiceProvider;
               var loggerFactory = services.GetRequiredService<ILoggerFactory>();       // we create an instance of the LoggerFactory class
               try {
                   var context = services.GetRequiredService<StoreContext>();
                   await context.Database.MigrateAsync();                                               // applies pending migrations to database or create database if it does not exist
                   await StoreContextSeed.SeedAsync(context, loggerFactory);                // applies our seeded database data on program startup
               }

               catch (Exception ex)
               {
                   var logger = loggerFactory.CreateLogger<Program>();
                   logger.LogError(ex, "An error occurred during migration");
               }
           }


            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();                                               // <Startup> a call to use our Startup.cs class
                });
    }
}
