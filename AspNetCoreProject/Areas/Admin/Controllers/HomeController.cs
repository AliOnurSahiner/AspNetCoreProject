using AspNetCoreProject.Entites;
using AspNetCoreProject.Interfaces;
using AspNetCoreProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace AspNetCoreProject.Areas.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUrunRepository _urunRepository;
        public HomeController(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }
        public IActionResult Index()
        {
            return View(_urunRepository.GetirHepsi());
        }

        public IActionResult Ekle()
        {
            return View(new AddUrunModel());
        }
        [HttpPost]
        public IActionResult Ekle(AddUrunModel model)
        {
            if (ModelState.IsValid)
            {
                Urun urun = new Urun();
                if (model.Resim != null)
                {
                    var uzanti = Path.GetExtension(model.Resim.FileName);
                    var yeniResimAd = Guid.NewGuid() + uzanti;
                    var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + yeniResimAd);//wwwrootun yerini belirledik GetCurrentDirectory ile uygulamanın nerede çalıştığını anladık
                    var stream = new FileStream(yuklenecekYer, FileMode.Create); //	FileMode.Create seçeneği ile yeni bir dosya oluşturulur, aynı dosya varsa üzerine yazılır.
                    model.Resim.CopyTo(stream);

                    urun.Resim = yeniResimAd;

                }
                urun.Ad = model.Ad;
                urun.Fiyat = model.Fiyat;
                _urunRepository.Ekle(urun);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View(model);
        }

        public IActionResult Guncelle(int id)
        {
            var eklenenUrun = _urunRepository.GetirId(id);

            UpdateUrunModel updateUrun = new UpdateUrunModel()
            {
                Ad = eklenenUrun.Ad,
                Fiyat = eklenenUrun.Fiyat,
                UrunId = eklenenUrun.Id

            };
            return View(updateUrun);
        }
        [HttpPost]
        public IActionResult Guncelle(UpdateUrunModel updateUrun)
        {

            if (ModelState.IsValid)
            {
                var guncellenecekUrun = _urunRepository.GetirId(updateUrun.UrunId);
                if (updateUrun.Resim != null)
                {
                    var uzanti = Path.GetExtension(updateUrun.Resim.FileName);
                    var yeniResimAd = Guid.NewGuid() + uzanti;
                    var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + yeniResimAd);//wwwrootun yerini belirledik GetCurrentDirectory ile uygulamanın nerede çalıştığını anladık
                    var stream = new FileStream(yuklenecekYer, FileMode.Create); //	FileMode.Create seçeneği ile yeni bir dosya oluşturulur, aynı dosya varsa üzerine yazılır.
                    updateUrun.Resim.CopyTo(stream);

                    guncellenecekUrun.Resim = yeniResimAd;

                }
                guncellenecekUrun.Ad = updateUrun.Ad;
                guncellenecekUrun.Fiyat = updateUrun.Fiyat;
                guncellenecekUrun.Id = updateUrun.UrunId;
                _urunRepository.Guncelle(guncellenecekUrun);
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }

            return View(updateUrun);

        }
        public IActionResult Sil(Urun urun)
        {

            _urunRepository.Sil(urun);

            return RedirectToAction("Index");

        }
    }

}
