using AspNetCoreProject.Interfaces;
using AspNetCoreProject.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddRazorPages();
            services.AddControllersWithViews(); 
            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddScoped<IKategoriRepository, KategoriRepository>();
            services.AddScoped<IUrunKategoriRepository, UrunKategoriRepository>();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

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
                endpoints.MapControllerRoute(
                    name: "aryaRoute",
                    pattern:"Arya", // Controllerda /Arya diye bir istek gelirse 
                    defaults: new { controller ="Home",Action = "Index" }//aþaðýdaki controlleri çalýþtýr.Örnek : localhost:45698/Arya=localhost:45698/Home/Index
                    ) ;
                endpoints.MapControllerRoute(name:default,pattern:"{controller}/{action}/{id?}");
            });
        }
    }
}
