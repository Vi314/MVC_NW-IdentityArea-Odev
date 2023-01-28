using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind_MVC.Models.Context;

namespace Northwind_MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Adding mvc
            services.AddControllersWithViews();

            //Adding Dbcontext Path
            services.AddDbContext<NORTHWNDContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultString")));

            //Adding AspNetIdentity
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<NORTHWNDContext>();

            //Account options
            services.Configure<IdentityOptions>(x =>
            {
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequiredLength = 6;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireDigit = true;
                x.Password.RequireNonAlphanumeric = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
