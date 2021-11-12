using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<RouteOptions>(options => options.ConstraintMap.Add("weekday",typeof(WeekDayConstraint)));

            services.AddControllersWithViews(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseMvc(routes => 
            {
                routes.MapRoute(
                    name:"areas",
                    template:"{area:exists}/{controller=Home}/{action=Index}"
                    );

                routes.MapRoute(
                    name: "default", 
                    template:"{controller=Home}/{action=CustomVariable}/{id?}");

                routes.MapRoute(
                    name:"out",
                    template:"outbound/{controller=Home}/{action=Index}");
            });
            //app.UseMvc(routes =>
            //{
            //    //routes.MapRoute(name:"MyRoute",template:"{controller=Home}/{action=Index}/{id:weekday?}");
            //    //routes.MapRoute(name:"MyRoute",template:"{controller=Home}/{action=Index}/{id?}/{*catchall}");
            //    /*
            //    routes.MapRoute("ShopScheme2",template:"Shop/OldActionn",defaults:new { controller = "Home", action = "Index" });
            //    routes.MapRoute("ShopScheme", template: "Shop/{action}",defaults: new { controller = "Home" });
            //    routes.MapRoute("","X{controller}/{action}");
            //    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}");
            //    routes.MapRoute(name: "", template: "Public/{controller=Home}/{action=Index}");
            //    */
            //});


        }
    }
}
