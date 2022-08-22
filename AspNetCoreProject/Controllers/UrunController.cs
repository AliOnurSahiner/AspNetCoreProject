using AspNetCoreProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Controllers
{
    public class UrunController : Controller
    {
        public IActionResult Index()
        {
            DpUrunRepositories urunRepository = new DpUrunRepositories();

            return View(urunRepository.GetirHepsi());
        }
    }
}
