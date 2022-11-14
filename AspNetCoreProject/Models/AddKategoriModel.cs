using System.ComponentModel.DataAnnotations;

namespace AspNetCoreProject.Models
{
    public class AddKategoriModel
    {
        [Required(ErrorMessage ="Ürün Adı Boş Geçilemez!")]
        public string KategoriAd { get; set; }
    }
}
