using AspNetCoreProject.Contexts;
using AspNetCoreProject.Entites;
using AspNetCoreProject.Interfaces;
using AspNetCoreProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Controllers
{
    //[Route("Arya/[Action]")] // localhost:45698/Arya/Index demektir bu startup classındaki maproute'ı ezer. Sadece bu route ile çalışır.
    public class HomeController : Controller
    {
        AspNetCoreContext db = new AspNetCoreContext();
        private readonly SignInManager<AppUser> _signInManager;
        public IUrunRepository _urunRepository;
        private readonly IKategoriRepository _kategoriRepository;
       
        public HomeController(IUrunRepository urunRepository, IKategoriRepository kategoriRepository, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _urunRepository = urunRepository;
            _kategoriRepository = kategoriRepository;
        }
        public IActionResult Index()
        {
            //SetCookie("kisi", "Arya");
            SetSession("kisi", "Arya");
            return View(_urunRepository.GetirHepsi());
        }
        public IActionResult UrunDetay(int id)
        {
            //ViewBag.Cookie = GetCookie("kisi");
            ViewBag.Session = GetSession("kisi");
            return View(_urunRepository.GetirId(id));
        }

        //public void SetCookie(string key, string value)
        //{
        //    HttpContext.Response.Cookies.Append(key, value);
        //}
        public void SetSession(string key, string value)
        {

            HttpContext.Session.SetString(key, value);
        }
        public string GetSession(string key)
        {
          return  HttpContext.Session.GetString(key);

           
        }

        //public string GetCookie(string key)
        //{
        //    HttpContext.Request.Cookies.TryGetValue(key, out string value);

        //    return value; 
        //}

        public IActionResult GirisYap()
        {
            return View(new KullaniciGirisModel());
        }
        [HttpPost]
        public IActionResult GirisYap(KullaniciGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = _signInManager.PasswordSignInAsync(model.KullaniciAd,model.Sifre,model.BeniHatirla,false).Result;
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index","Home",new {area="Admin"});
                }
                ModelState.AddModelError("", "Kullanıcı adı veya parola hatalı!");
            }
            //var kullaniciBilgileri = db.Users.Find(x=>x.u);
            return View(new KullaniciGirisModel());
        }

        public IActionResult AtaKategori(int id)  // buradaki Id Urun Id
        {
            TempData["UrunId"] = id;
            var urununKategorisi = _urunRepository.GetirKategoriForUrun(id).Select(x=>x.Ad);
            var getirKategori = _kategoriRepository.GetirHepsi();
            List<KategoriAtaModel> kategoriAtaModels = new List<KategoriAtaModel>();

            foreach (var item in getirKategori)
            {
                KategoriAtaModel kategoriAtaModel = new KategoriAtaModel();
                kategoriAtaModel.KategoriId = item.Id;
                kategoriAtaModel.KategoriAd = item.Ad;
                kategoriAtaModel.VarMi = urununKategorisi.Contains(item.Ad);
                kategoriAtaModels.Add(kategoriAtaModel);
            }
            return View(kategoriAtaModels);
        }
        [HttpPost]
        public IActionResult AtaKategori(List<KategoriAtaModel> kategoriAtaModels)  // buradaki Id Urun Id
        {
            int urunId = (int)TempData["UrunId"];
            foreach (var item in kategoriAtaModels)
            {
                if (item.VarMi)
                {
                    _urunRepository.EkleKategori(new UrunKategori
                    {
                        KategoriId = item.KategoriId,
                        UrunId = urunId

                    });
                }
                else
                {
                    _urunRepository.SilKategori(new UrunKategori
                    {
                        KategoriId = item.KategoriId,
                        UrunId = urunId

                    });
                }
            }
            return RedirectToAction("Index"); 
        }
    }
}
