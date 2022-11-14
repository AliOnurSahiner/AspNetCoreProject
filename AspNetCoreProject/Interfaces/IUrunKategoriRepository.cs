using AspNetCoreProject.Entites;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AspNetCoreProject.Interfaces
{
    public interface IUrunKategoriRepository:IGenericRepository<UrunKategori>
    {
        UrunKategori GetirFiltreIle(Expression<Func<UrunKategori, bool>> filter);

    }
}
