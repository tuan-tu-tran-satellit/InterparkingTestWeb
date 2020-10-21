using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using InterparkingTest.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InterparkingTestWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddLogging(logging =>
            {
                logging
                    .AddConsole()
                ;
            });
            services.AddInterparkingTestApplicationServices(dbOptions =>
            {
                dbOptions.UseSqlite("Filename=routes.sqlite3");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Route}/{action=Index}/{id?}");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var applicationFacade = scope.ServiceProvider.GetRequiredService<IApplicationFacade>();
                applicationFacade.EnsureDatabaseCreated();
            }

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture; //this is needed for comma vs period as decimal separator issue when entering
            //coordinates in the route modification form : client side validation expects a period as separator
            //whereas server side, it depends on the system where this app is running, which we don't want: we want it to be independant of the OS setting.
        }
    }
}
