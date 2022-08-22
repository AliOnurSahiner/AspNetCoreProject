using AspNetCoreProject.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Repositories
{
    public class GenericRepository<Tablo> where Tablo : class,new()
    {
        public void Ekle(Tablo tablo)
        {
            using (var context = new AspNetCoreContext())
            {
                context.Set<Tablo>().Add(tablo);
                context.SaveChanges();
            }

        }

        public void Guncelle(Tablo tablo)
        {
            using (var context = new AspNetCoreContext())
            {
                context.Set<Tablo>().Update(tablo);
                context.SaveChanges();
            }

        }
        public void Sil(Tablo tablo)
        {
            using (var context = new AspNetCoreContext())
            {
                context.Set<Tablo>().Remove(tablo);
                context.SaveChanges();
            }

        }
        public List<Tablo> GetirHepsi()
        {
            using (var context = new AspNetCoreContext())
            {
                return context.Set<Tablo>().ToList();
            }

        }
        public Tablo GetirId(int Id)
        {
            using (var context = new AspNetCoreContext())
            {
                return context.Set<Tablo>().Find(Id);
            }

        }

    }
}
