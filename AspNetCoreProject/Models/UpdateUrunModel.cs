using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreProject.Models
{
    public class UpdateUrunModel
    {
        public int UrunId { get; set; }
        [Required(ErrorMessage = "Lütfen Ad alanını doldurunuz!")]
        public string Ad { get; set; }
        [Range(minimum: 1, maximum: 1000, ErrorMessage = "Fiyat 0 dan yüksek olmalıdır.")]
        public decimal Fiyat { get; set; }
        public IFormFile Resim { get; set; }

    }
}
