using AspNetCoreProject.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using System.Text;

namespace AspNetCoreProject.TagHelpers
{
    [HtmlTargetElement("getirKategoriAd")] //CsHtml Dosyasında arayacağımız isim getirKategoriAd
    public class KategoriAd:TagHelper
    {
        private readonly IUrunRepository _urunRepository;
        public KategoriAd(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }
        public int UrunId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //TagBuilder tagBuilder = new TagBuilder("ul"); //Ul nesnesi oluşturur.
            //StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append();

            string data = "";
            var gelenKategoriler = _urunRepository.GetirKategoriForUrun(UrunId).Select(I => I.Ad);
            foreach (var item in gelenKategoriler)
            {
                data += item + " ";
            }
            output.Content.SetContent(data);
        }
     

    }
}
