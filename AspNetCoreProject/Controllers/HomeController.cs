using AspNetCoreProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Controllers
{
    //[Route("Arya/[Action]")] // localhost:45698/Arya/Index demektir bu startup classındaki maproute'ı ezer. 
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
