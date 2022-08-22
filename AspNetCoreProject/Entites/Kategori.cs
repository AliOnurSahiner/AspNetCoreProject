using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreProject.Entites
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "Ad alanı boş geçilemez")]
        public string Ad { get; set; }

        public List<UrunKategori> UrunKategoriler { get; set; }
    }
}
