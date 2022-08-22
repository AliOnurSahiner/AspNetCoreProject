using AspNetCoreProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Controllers
{
    public class HomeController : Controller
    {
        public IUrunRepository _urunRepository;
        public HomeController(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }
        public IActionResult Index()
        {
           
            return View(_urunRepository.GetirHepsi());
        }
        public IActionResult UrunDetay(int id)
        {

            return View(_urunRepository.GetirId(id));
        }
    }
}
