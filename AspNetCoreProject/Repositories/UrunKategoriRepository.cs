using AspNetCoreProject.Contexts;
using AspNetCoreProject.Entites;
using AspNetCoreProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AspNetCoreProject.Repositories
{
    public class UrunKategoriRepository : GenericRepository<UrunKategori>, IUrunKategoriRepository
    {
        public UrunKategori GetirFiltreIle(Expression<Func<UrunKategori, bool>> filter)
        {
            using var context =new AspNetCoreContext();

          return context.UrunsKategoris.FirstOrDefault(filter);
        }
    }
}
