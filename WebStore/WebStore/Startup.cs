using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;

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
            services.AddControllersWithViews(opt => opt.Conventions.Add(new TestControllerConvention()))
                .AddRazorRuntimeCompilation();
        }
                
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
