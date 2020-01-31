using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration["Data:SportStoreProducts:ConnectionString"]));
            // Entity framework
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(
                Configuration["Data:SportStoreIdentity:ConnectionString"]));
            // use to setup the Identity services using the built in 
            // classes to represent users and roles
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            // Register the Repository Service
            // can replace fake to real repository later
            //services.AddTransient<IProductRepository, FakeProductRepository>();// serviceType, implementType
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            // AddScoped method specifies that the same obj should be used to satisfy related requests for Cart 
            // instances
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // specifies that the same obj should be used , use HttpContextAccessor class when 
            // IHttpContextAccessor is required => Accessing the current session in the SessionCart

            // it gets FacebookBuilderAppExtension to use it with Facebook API
            services.AddTransient<IOrderRespository, EFOrderRepository>();
            services.AddMvc();
         
        // to save in memory, it will be deleted when app stopped or restarted
            services.AddMemoryCache(); // setup in memory data store
            services.AddSession(); // register the services used to access session data
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            app.UseDeveloperExceptionPage(); // displays details of exceptions that occur in the app
            // should not be in deployed app

            app.UseStatusCodePages();
            // add simple messages to Http responses => example 404-NotFound


            app.UseSession(); // allow the session system to automatically associate request with sessions when they arrive from client
            app.UseAuthentication();
            // setup the components that will intercept requests and responses to implement the security policy
            // map URLs to controller and action methods
            app.UseMvc(routes =>
            {
                // URLs Pagination Category
                routes.MapRoute
                (
                    name: null,
                    template: "{category}/Page{productPage}",
                    defaults: new { controller = "Product", action = "List" }
                );

                routes.MapRoute(
                    name: null,
                    template: "Page{productPage}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );

                routes.MapRoute(name: null,
                    template: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");

                //// after improving 
                //routes.MapRoute(
                //    name: "pagination",
                //    template: "Products/Page{productPage}",
                //    defaults: new { Controller = "Product", action = "List" });


                //// note not controllers , but it is controller not s at the end 
                //routes.MapRoute(
                //name: "default",
                //template: "{controller=Product}/{action=List}/{id?}"
                //);


            }); // enables ASP.Net Core MVC

            app.UseStaticFiles();

            // app.UseMvcWithDefaultRoute(); // enables ASP.NEt core MVC
            // we create database and written just one time
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
