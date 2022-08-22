using AspNetCoreProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.ViewComponent
{
    public class UrunList: Microsoft.AspNetCore.Mvc.ViewComponent
    {
       readonly IUrunRepository _urunRepository; //Kategoriyi VT den çektik
        public UrunList(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }

        public IViewComponentResult Invoke()
        {

            return View(_urunRepository.GetirHepsi());  //Metodumuza yazdık
        }

    }
}
