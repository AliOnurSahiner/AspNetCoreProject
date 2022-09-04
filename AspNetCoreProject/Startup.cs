using AspNetCoreProject.Contexts;
using AspNetCoreProject.Entites;
using AspNetCoreProject.Interfaces;
using AspNetCoreProject.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject
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
            services.AddDbContext<AspNetCoreContext>();
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {

                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                //Bunlarý oluþturmak istediðimiz Admin kullanýcýsý için bazý password deðerlerini configure ettik. 

            }).AddEntityFrameworkStores<AspNetCoreContext>(); //Identity için startup'a ekleme yaptýk 

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/GirisYap");
                options.Cookie.Name = "AspNetCoreProject";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });



            services.AddRazorPages();
            services.AddSession();
            services.AddAuthentication();   
            services.AddControllersWithViews();
            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddScoped<IKategoriRepository, KategoriRepository>();
            services.AddScoped<IUrunKategoriRepository, UrunKategoriRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            IdentityInitializer.CreateAdminUser(userManager,roleManager); // Admin Kullanýcýsý oluþturmak için yazýldý 1 kereye mahsus
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseRouting();
            //Bunu yazmamýzýn nedeni bootstrap dosyalarýna fizksel olarak eriþmek .net projelerinde normal aspnet projesi gibi kopyala yapýþtýrla bootstrap dosyalarýna eriþilemez! node js yüklemek ve npm.json dosyasý eklemek gerekir.
            app.UseStaticFiles(new StaticFileOptions
            {

                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/content"
            });
            //app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Özel olan genel olanýn üstüne yazýlýr localhost:44635/Arya yazmak için
                //endpoints.MapControllerRoute(
                //    name: "aryaRoute",
                //    pattern: "Arya", // Controllerda /Arya diye bir istek gelirse 
                //    defaults: new { controller = "Home", Action = "Index" }//aþaðýdaki controlleri çalýþtýr.Örnek : localhost:45698/Arya=localhost:45698/Home/Index
                //    );
                endpoints.MapControllerRoute(name: "areas", pattern: "{area}/{Controller=Home}/{action=Index}/{id?}"); //Admin Paneli için yazýldý.
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller}/{action}/{id?}");
               
            });
        }
    }
}
