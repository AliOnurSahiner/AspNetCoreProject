using System.ComponentModel.DataAnnotations;

namespace AspNetCoreProject.Models
{
    public class KullaniciGirisModel
    {
        [Required(ErrorMessage ="Kullanıcı Adı Boş Geçilemez!")]
        public string KullaniciAd { get; set; }
        [Required(ErrorMessage = "Şifre Adı Boş Geçilemez!")]
        public string Sifre { get; set; }
        public bool  BeniHatirla { get; set; }

    }
}
