using AspNetCoreProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.ViewComponent
{
    public class KategoriList: Microsoft.AspNetCore.Mvc.ViewComponent
    {
       readonly IKategoriRepository _kategoriRepository; //Kategoriyi VT den çektik
        public KategoriList(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }

        public IViewComponentResult Invoke()
        {

            return View(_kategoriRepository.GetirHepsi());  //Metodumuza yazdık
        }

    }
}
