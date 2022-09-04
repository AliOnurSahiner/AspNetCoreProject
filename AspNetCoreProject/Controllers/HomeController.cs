using AspNetCoreProject.Contexts;
using AspNetCoreProject.Interfaces;
using AspNetCoreProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Controllers
{
    //[Route("Arya/[Action]")] // localhost:45698/Arya/Index demektir bu startup classındaki maproute'ı ezer. Sadece bu route ile çalışır.
    public class HomeController : Controller
    {
        AspNetCoreContext db = new AspNetCoreContext();
        public IUrunRepository _urunRepository;
       
        public HomeController(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
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
            //var kullaniciBilgileri = db.Users.Find(x=>x.u);
            return View(new KullaniciGirisModel());
        }
    }
}
