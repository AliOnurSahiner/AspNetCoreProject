using AspNetCoreProject.Contexts;
using AspNetCoreProject.Entites;
using AspNetCoreProject.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Repositories
{
    public class UrunRepository : GenericRepository<Urun>, IUrunRepository
    {

        //Urunun kategorilerinin getirilmesi işlemi

        public List<Kategori> GetirKategoriForUrun(int urunId) //Bir ürünün birden fazla kategorisi olabilir.
        {
            using var context = new AspNetCoreContext();
            var urn = context.Uruns.Join(context.UrunsKategoris, u => u.Id, uk => uk.UrunId, (u, uk) => new { u, uk })
                 .Join(context.Kategoris, uuk => uuk.uk.KategoriId, k => k.Id, (uuk, k) => new { uuk, k }).Where(I => I.uuk.u.Id == urunId).Select(I => new Kategori
                 {
                     Ad=I.k.Ad,
                     Id=I.k.Id,
                 }).ToList();
            return urn;
        }

    }
}
