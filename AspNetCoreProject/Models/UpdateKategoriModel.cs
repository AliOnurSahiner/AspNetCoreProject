using System.ComponentModel.DataAnnotations;

namespace AspNetCoreProject.Models
{
    public class UpdateKategoriModel
    {
        public int KategoriId { get; set; }
        [Required(ErrorMessage = "Ürün Adı Boş Geçilemez!")]
        public string KategoriAd { get; set; }
    }
}
