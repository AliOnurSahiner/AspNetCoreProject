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
                //Bunlar� olu�turmak istedi�imiz Admin kullan�c�s� i�in baz� password de�erlerini configure ettik. 

            }).AddEntityFrameworkStores<AspNetCoreContext>(); //Identity i�in startup'a ekleme yapt�k 

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
            IdentityInitializer.CreateAdminUser(userManager,roleManager); // Admin Kullan�c�s� olu�turmak i�in yaz�ld� 1 kereye mahsus
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseRouting();
            //Bunu yazmam�z�n nedeni bootstrap dosyalar�na fizksel olarak eri�mek .net projelerinde normal aspnet projesi gibi kopyala yap��t�rla bootstrap dosyalar�na eri�ilemez! node js y�klemek ve npm.json dosyas� eklemek gerekir.
            app.UseStaticFiles(new StaticFileOptions
            {

                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/content"
            });
            //app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //�zel olan genel olan�n �st�ne yaz�l�r localhost:44635/Arya yazmak i�in
                //endpoints.MapControllerRoute(
                //    name: "aryaRoute",
                //    pattern: "Arya", // Controllerda /Arya diye bir istek gelirse 
                //    defaults: new { controller = "Home", Action = "Index" }//a�a��daki controlleri �al��t�r.�rnek : localhost:45698/Arya=localhost:45698/Home/Index
                //    );
                endpoints.MapControllerRoute(name: "areas", pattern: "{area}/{Controller=Home}/{action=Index}/{id?}"); //Admin Paneli i�in yaz�ld�.
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller}/{action}/{id?}");
               
            });
        }
    }
}
