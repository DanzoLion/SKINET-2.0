using System.Collections.Concurrent;
using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions    // these are removed from Startup: ConfigureServices(IServiceCollection services) 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();    // implemented far caching service, needs to be a singleton and shared across all services
            services.AddScoped<ITokenService, TokenService>();                          // for identity
            services.AddScoped<IOrderService, OrderService>();                          // order services to extract from new database migration
            services.AddScoped<IUnitOfWork, UnitOfWork>();                              // unit of work implementation // we then implement in order service
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProductRepository, ProductRepository>();      // created when http request incoming to API  // creates instance of controller // controller creates instance of repository // when req. finished disposes of cont. and rep.
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));  // we don't know the type, and the type is collected at compile/run-time so slightly different config here

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext => 
                {
                    var errors = ActionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;

            // we extend IServiceCollection -> add services we need -> return services
        }
    }
}