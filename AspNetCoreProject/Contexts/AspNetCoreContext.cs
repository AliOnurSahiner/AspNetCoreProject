using AspNetCoreProject.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreProject.Contexts
{
    public class AspNetCoreContext:IdentityDbContext<AppUser> //Identity için böyle bir  IdentityDbContext'ten türettik normalde DbContextten türer.
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-MK1U8ED\SQLEXPRESS;Database=Northwind;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
            optionsBuilder
            .UseSqlServer("Server=localhost ,1434;Database=AspNetCoreProject;User Id=SA;Password=Arya!1234;");
            base.OnConfiguring(optionsBuilder);//DbContexte gönderdik 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Tabloları birbirine bağladık
        {
            modelBuilder.Entity<Urun>().HasMany(u=>u.UrunKategoriler).WithOne(k=>k.Urun).HasForeignKey(I=>I.KategoriId);
            modelBuilder.Entity<Kategori>().HasMany(u => u.UrunKategoriler).WithOne(k => k.Kategori).HasForeignKey(I => I.KategoriId);

            //Tekrarlı data girilmesini engellemek için 
            modelBuilder.Entity<UrunKategori>().HasIndex(I => new
            {

                I.KategoriId,
                I.UrunId
            }).IsUnique();
            base.OnModelCreating(modelBuilder);//DbContexte gönderdik 
        }

        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }

        public DbSet<UrunKategori> UrunsKategoris { get; set; }  

    }
}
