using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TelefonRehberiNuevo.CORE.Constants;
using TelefonRehberiNuevo.DTO.EEntity;
using TelefonRehberiNuevo.SERVICES.Interfaces;
using TelefonRehberiNuevo.WEB.Filter;
using TelefonRehberiNuevo.WEB.Managers;

namespace TelefonRehberiNuevo.WEB.Controllers
{
    [ExceptionFilter]
    public class KullaniciController : Controller
    {
        //_kullaniciService interfacesini burada kullanmadık.Fakat hızlı sonuçlar için api üzerinden gitmek yerine direk service sınıfına gidebiliriz diye tanımlı bırakıyorum.
        private readonly IKullaniciService _kullaniciService;

        public KullaniciController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }


        // GET: Kullanici
        public ActionResult Kisiler()
        {
            return View(ServiceManager.RestSharpGet<List<EKisilerDTO>>(ApiUrlConstants.GetAktifKisiler));
        }

        public ActionResult Details(int id)
        {
            EKisiDetayDTO kisiDto = ServiceManager.RestSharpGet<EKisiDetayDTO>(ApiUrlConstants.GetKullaniciDetayById+"?id="+id);
            if (kisiDto == null || kisiDto.Rol == "Admin")
            {
                return Error404("");
            }
            return View(kisiDto);
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Error404(string aspxerrorpath)
        {
            if (!string.IsNullOrWhiteSpace(aspxerrorpath))
            {
                return RedirectToAction("Error404");
            }

            return View("Error404");
        }
    }
}