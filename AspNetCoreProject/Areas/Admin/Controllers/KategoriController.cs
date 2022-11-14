using AspNetCoreProject.Entites;
using AspNetCoreProject.Interfaces;
using AspNetCoreProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KategoriController : Controller
    {
        private readonly IKategoriRepository _kategoriRepository;

        public KategoriController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }

        public IActionResult Index()
        {
            return View(_kategoriRepository.GetirHepsi());
        }

        public IActionResult Ekle()
        {
            return View(new AddKategoriModel());
        }

        [HttpPost]
        public IActionResult Ekle(AddKategoriModel kategoriModel)
        {

            if (ModelState.IsValid)
            {
                Kategori kategori = new Kategori()
                {
                    Ad = kategoriModel.KategoriAd

                };


                _kategoriRepository.Ekle(kategori);

                return RedirectToAction("Index");
            }


            return View(kategoriModel);

        }
        public IActionResult Guncelle(int id)
        {
            var guncellenecekKategori = _kategoriRepository.GetirId(id);

            UpdateKategoriModel updateKategori = new UpdateKategoriModel()
            {
                KategoriId = guncellenecekKategori.Id,
                KategoriAd = guncellenecekKategori.Ad,
            };

            return View(updateKategori);

        }
        [HttpPost]
        public IActionResult Guncelle(UpdateKategoriModel kategoriModel)
        {
            if (ModelState.IsValid)
            {
                var guncelKategori = _kategoriRepository.GetirId(kategoriModel.KategoriId);
                guncelKategori.Ad = kategoriModel.KategoriAd;
                _kategoriRepository.Guncelle(guncelKategori);
                return RedirectToAction("Index", "Kategori", new { area = "Admin" });
            }
            return View(kategoriModel);
        }

        public IActionResult Sil(Kategori kategori)
        {

            _kategoriRepository.Sil(kategori);

            return RedirectToAction("Index");

        }
    }
}
