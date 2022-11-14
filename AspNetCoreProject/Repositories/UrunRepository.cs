using AspNetCoreProject.Contexts;
using AspNetCoreProject.Entites;
using AspNetCoreProject.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Repositories
{
    public class UrunRepository : GenericRepository<Urun>, IUrunRepository
    {
        private readonly IUrunKategoriRepository _urunKategoriRepository;
        public UrunRepository(IUrunKategoriRepository urunKategoriRepository)
        {
            _urunKategoriRepository = urunKategoriRepository;
        }

        //Urunun kategorilerinin getirilmesi işlemi

        public List<Kategori> GetirKategoriForUrun(int urunId) //Bir ürünün birden fazla kategorisi olabilir.
        {
            using var context = new AspNetCoreContext();
            var urn = context.Uruns.Join(context.UrunsKategoris, u => u.Id, uk => uk.UrunId, (u, uk) => new { u, uk })
                 .Join(context.Kategoris, uuk => uuk.uk.KategoriId, k => k.Id, (uuk, k) => new { uuk, k }).Where(I => I.uuk.u.Id == urunId).Select(I => new Kategori
                 {
                     Ad = I.k.Ad,
                     Id = I.k.Id,
                 }).ToList();
            return urn;
        }

        public void EkleKategori(UrunKategori urunKategori)
        {
            var KayitKontrol = _urunKategoriRepository.GetirFiltreIle(x => x.KategoriId == urunKategori.KategoriId && x.UrunId == urunKategori.UrunId);

            if (KayitKontrol != null)
            {
                _urunKategoriRepository.Ekle(urunKategori);
            }
        }

        public void SilKategori(UrunKategori urunKategori)
        {
            var KayitKontrol = _urunKategoriRepository.GetirFiltreIle(x => x.KategoriId == urunKategori.KategoriId && x.UrunId == urunKategori.UrunId);

            if (KayitKontrol != null)
            {
                _urunKategoriRepository.Sil(urunKategori);
            }
        }
        public List<Urun> GetirKategoriIdile(int kategoriId)
        {
            using var context = new AspNetCoreContext();

           var getirUrun = context.Uruns.Join(context.UrunsKategoris, u => u.Id, uk => uk.UrunId, (urun, urunKategori) => new
            {

                Urun = urun,
                UrunKategori = urunKategori
            }).Where(k => k.UrunKategori.KategoriId == kategoriId).Select(k => new Urun
            {
                Id = k.Urun.Id,
                Ad = k.Urun.Ad,
                Fiyat = k.Urun.Fiyat,
                Resim = k.Urun.Resim,
            }).ToList();

            return getirUrun;
        }
    }
}
