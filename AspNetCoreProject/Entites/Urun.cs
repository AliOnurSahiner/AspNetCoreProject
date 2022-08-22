using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreProject.Entites
{
    [Dapper.Contrib.Extensions.Table("Uruns")]
    public class Urun
    {
        [ExplicitKey]
        public int Id { get; set; }
        //[Required(ErrorMessage = "Ad boş geçilemez")]
        public string Ad { get; set; }
        //[Column(TypeName ="ntext")]
        //[Required(ErrorMessage = "Aciklama boş geçilemez")]
        public string Aciklama { get; set; }
        //[Required(ErrorMessage = "Fiyat boş geçilemez")]
        public decimal Fiyat { get; set; }
        public List<UrunKategori> UrunKategoriler { get; set; }
    }
}
