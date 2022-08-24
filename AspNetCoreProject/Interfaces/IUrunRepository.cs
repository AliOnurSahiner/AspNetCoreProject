using AspNetCoreProject.Entites;
using System.Collections.Generic;

namespace AspNetCoreProject.Interfaces
{
    public interface IUrunRepository:IGenericRepository<Urun>
    {
        List<Kategori> GetirKategoriForUrun(int urunId);
    }
}
