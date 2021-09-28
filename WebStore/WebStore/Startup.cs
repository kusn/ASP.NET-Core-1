﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Services;
using WebStore.Services.Interfaces;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
                
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")));

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddSingleton<IProductData, InMemoryProductData>();

            services.AddControllersWithViews(opt => opt.Conventions.Add(new TestControllerConvention()))
                .AddRazorRuntimeCompilation();
        }
                
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStatusCodePagesWithReExecute("/Home/Status/", "?code={0}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<TestMiddleware>();

            //var loggin = Configuration["Loggin:LogLevel:Default"];
            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapGet("/greetings", async context =>
                {
                    var greetings = Configuration["Greetings"];
                    await context.Response.WriteAsync(greetings);
                });

                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
