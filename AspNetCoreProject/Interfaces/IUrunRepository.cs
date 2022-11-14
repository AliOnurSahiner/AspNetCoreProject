using AspNetCoreProject.Entites;
using System.Collections.Generic;

namespace AspNetCoreProject.Interfaces
{
    public interface IUrunRepository:IGenericRepository<Urun>
    {
        List<Kategori> GetirKategoriForUrun(int urunId);
        void EkleKategori(UrunKategori urunKategori);
        void SilKategori(UrunKategori urunKategori);
        List<Urun> GetirKategoriIdile(int kategoriId);
    }
}
