using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind_MVC.Models.IdentityContextNW;
using System;

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
            services.AddDbContext<NorthwindContextIdentity>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultString")));

            //Adding AspNetIdentity
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<NorthwindContextIdentity>();

            //Account options
            services.Configure<IdentityOptions>(x =>
            {
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequiredLength = 4;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireDigit = false;
                x.Password.RequireNonAlphanumeric = false;
            });

            //COOKIES YES I LOVE COOKIES
            services.ConfigureApplicationCookie(cookie =>
            {
                cookie.Cookie = new Microsoft.AspNetCore.Http.CookieBuilder
                {
                    Name = "Sugar-free-high-fiber-low-fat-amazing-cookie"
                };
                cookie.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
                cookie.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
                cookie.SlidingExpiration = true;
                cookie.ExpireTimeSpan = TimeSpan.FromMinutes(30);
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
