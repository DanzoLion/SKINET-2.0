using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)                                                                                      // ordering not important here
        {
            // services.AddScoped<IProductRepository, ProductRepository>();      // created when http request incoming to API  // creates instance of controller // controller creates instance of repository // when req. finished disposes of cont. and rep.
            // services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));  // we don't know the type, and the type is collected at compile/run-time so slightly different config here
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();                                                                                                                                          // gives access to endpoints added as a service
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));  // access to our database // via connection string and <StoreContext>
           
            services.AddDbContext<AppIdentityDbContext>(x => 
            {
                x.UseSqlite(_config.GetConnectionString("IdentityConnection"));
            });

        services.AddSingleton<IConnectionMultiplexer>(c => {
            var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(configuration);
        });

            services.AddApplicationServices();
            services.AddIdentityServices(_config);
            services.AddSwaggerDocumentation();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");   // informs API we won't allow unsecure headers via CORS
                });
            });
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            // });
            // services.Configure<ApiBehaviorOptions>(options =>
            // {
            //     options.InvalidModelStateResponseFactory = ActionContext => 
            //     {
            //         var errors = ActionContext.ModelState
            //         .Where(e => e.Value.Errors.Count > 0)
            //         .SelectMany(x => x.Value.Errors)
            //         .Select(x => x.ErrorMessage).ToArray();
            //         var errorResponse = new ApiValidationErrorResponse
            //         {
            //             Errors = errors
            //         };
            //         return new BadRequestObjectResult(errorResponse);
            //     };
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  // where we can manipulated data in and out of the pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)                           // middleware to manipulate the pipe              // ordering very important here as its config build     
        {
            // app.UseMiddleware<ExceptionMiddleware>();
            //     app.UseSwagger();
            //     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            // if (env.IsDevelopment())                                             // for dev. env only .. moved to above for production also 
            // {
            //    // app.UseDeveloperExceptionPage();                                                                                // provides the developer exception page when we get a server error
            //     // app.UseSwagger();
            //     // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            // }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}"); // if request endpoint does not match this middleware gets triggered -> redirects to ErrorController.cs and passes in code {0} / 404 not found
            app.UseHttpsRedirection();   // automatically re-directs to https
            app.UseRouting();
            app.UseStaticFiles();        // added when we imported our images folder into our project // this is middleware
            app.UseCors("CorsPolicy");
            app.UseAuthentication();                // implemented after setting up identity to use the token
            app.UseAuthorization();
            app.UseSwaggerDocumenation();
            app.UseEndpoints(endpoints =>             // so our application knows which endpoints are available and routed to
            {
                endpoints.MapControllers();
            });
        }
    }
}
