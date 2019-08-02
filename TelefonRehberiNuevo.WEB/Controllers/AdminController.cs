using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using TelefonRehberiNuevo.CORE.Constants;
using TelefonRehberiNuevo.DTO.EEntity;
using TelefonRehberiNuevo.SERVICES.Interfaces;
using TelefonRehberiNuevo.UTILITIES.SessionOperations;
using TelefonRehberiNuevo.WEB.Filter;
using TelefonRehberiNuevo.WEB.Managers;
using TelefonRehberiNuevo.WEB.Models;

namespace TelefonRehberiNuevo.WEB.Controllers
{
    [ExceptionFilter]
    public class AdminController : Controller
    {
        //_kullaniciService interfacesini burada kullanmadık.Fakat hızlı sonuçlar için api üzerinden gitmek yerine direk service sınıfına gidebiliriz diye tanımlı bırakıyorum.
        private IKullaniciService _kullaniciService;
        private SessionContext _sessionContext;

        public AdminController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
            _sessionContext = new SessionContext();
        }

        // GET: Admin
        [Auth]
        public ActionResult Index()
        {
            return View(ServiceManager.RestSharpGet<List<EKisilerDTO>>(ApiUrlConstants.GetAllCalisanKisiler));
        }

        [Auth]
        public ActionResult Delete(int id)
        {
            EKisiDetayDTO kisi = ServiceManager.RestSharpGet<EKisiDetayDTO>(ApiUrlConstants.GetKullaniciDetayById + "?id=" + id);
            if (kisi == null)
            {
                return RedirectToAction("Error404", "Kullanici");
            }
            return View(kisi);
        }

        [Auth]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EKisilerDTO kisi = ServiceManager.RestSharpGet<EKisilerDTO>(ApiUrlConstants.GetKullaniciById + "?id=" + id);
            if (kisi == null)
            {
                return Json(new { sonuc = false, mesaj = "Kayıtlı kullanıcı bulunamadı" }, JsonRequestBehavior.AllowGet);
            }

            if (ServiceManager.RestSharpGet<int>(ApiUrlConstants.KisiYoneticiMi + "?id=" + id) > 0)
            {
                return Json(new { sonuc = false, mesaj = kisi.Adi + " " + kisi.Soyadi + " başka bir çalışanın yöneticisi statüsünde bulunuyor.Silinemez !!" });
            }

            if (ServiceManager.RestSharpPost<int>(ApiUrlConstants.KisiSil, kisi) > 0)
            {
                return Json(new { sonuc = true, mesaj = kisi.Adi + " " + kisi.Soyadi + " kişisini sildiniz." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { sonuc = false, mesaj = kisi.Adi + " " + kisi.Soyadi + " kişisini silerken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
        }

        [Auth]
        public ActionResult Details(int id)
        {
            EKisiDetayDTO kisiDto = ServiceManager.RestSharpGet<EKisiDetayDTO>(ApiUrlConstants.GetKullaniciDetayById + "?id=" + id);
            if (kisiDto == null)
            {
                return RedirectToAction("Error404", "Kullanici");
            }
            return View(kisiDto);
        }

        [Auth]
        public ActionResult Edit(int id)
        {

            EKisilerDTO kisi = ServiceManager.RestSharpGet<EKisilerDTO>(ApiUrlConstants.GetKullaniciById + "?id=" + id);
            if (kisi == null)
            {
                return RedirectToAction("Error404", "Kullanici");
            }
            EKisiDepartmansRolsYoneticisDTO eKisiDepartmansRolsYoneticisDto = new EKisiDepartmansRolsYoneticisDTO()
            {
                eKisilerDto = kisi,
                EDepartmanDtos = CacheHelper.GetDepartmansFromCache(),
                ERolDtos = CacheHelper.GetRolsFromCache(),
                EYoneticisDtos = ServiceManager.RestSharpGet<List<EKisilerDTO>>(ApiUrlConstants.GetYoneticiNotContainsKisiId+"?id="+id)
            };
            return View(eKisiDepartmansRolsYoneticisDto);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EKisiDepartmansRolsYoneticisDTO model)
        {
            if (ModelState.IsValid)
            {
                if (ServiceManager.RestSharpPost<int>(ApiUrlConstants.KisiUpdate, model.eKisilerDto) > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    model.EDepartmanDtos = CacheHelper.GetDepartmansFromCache();
                    model.ERolDtos = CacheHelper.GetRolsFromCache();
                    model.EYoneticisDtos =
                        ServiceManager.RestSharpGet<List<EKisilerDTO>>(ApiUrlConstants.GetYoneticiNotContainsKisiId + "?id=" + model.eKisilerDto.KisiId);
                    return View(model);
                }
            }
            model.EDepartmanDtos = CacheHelper.GetDepartmansFromCache();
            model.ERolDtos = CacheHelper.GetRolsFromCache();
            model.EYoneticisDtos =
                ServiceManager.RestSharpGet<List<EKisilerDTO>>(
                    ApiUrlConstants.GetYoneticiNotContainsKisiId + "?id=" + model.eKisilerDto.KisiId);
            return View(model);
        }

        public ActionResult AdminGirisi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminGirisi(EAdminLoginDTO adminLoginDto)
        {
            EKisiDetayDTO admin = ServiceManager.RestSharpPost<EKisiDetayDTO>(ApiUrlConstants.GetAdminByNameAndPassword, adminLoginDto);

            if (admin != null)
            {
                AutoMapper.Mapper.DynamicMap(admin, _sessionContext);
                CurrentSession.Set("Admin", _sessionContext);
                return Json(new { sonuc = true, islem = "/Admin/Index", mesaj = "Yönlendiriliyorunuz." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { sonuc = false, islem = "/Admin/AdminGirisi", mesaj = "Kayıtlı kullanıcı bulunamadı.Lütfen Bilgilerinizi kontrol ediniz." }, JsonRequestBehavior.AllowGet);
            }
        }

        [Auth]
        public ActionResult AdminCikis()
        {
            CurrentSession.Clear();

            return RedirectToAction("Kisiler", "Kullanici");
        }

        [Auth]
        public ActionResult SifreDegistir()
        {
            return View();
        }

        [Auth]
        [HttpPost]
        public ActionResult SifreDegistir(ESifreDegistirDTO eSifreDegistirDto)
        {
            if (ServiceManager.RestSharpPost<bool>(ApiUrlConstants.EskiSifreKontrol, eSifreDegistirDto.Eskisifre) == false)
            {
                return Json(new { sonuc = false, mesaj = "Geçerli Admin Şifrenizi Yanlış Girdiniz." });
            }

            if (eSifreDegistirDto.YeniSifre != eSifreDegistirDto.YeniSifreTekrar)
            {
                return Json(new { sonuc = false, mesaj = "Yeni Şifre ile Yeni Şifre Tekrar Uyuşmuyor." });
            }

            ERolDTO eRolDto = new ERolDTO()
            {
                RolAdi = "Admin",
                RolId = 2,
                Sifre = eSifreDegistirDto.YeniSifre
            };
            if (ServiceManager.RestSharpPost<int>(ApiUrlConstants.RolGuncelle, eRolDto) > 0)
            {
                WebCache.Remove(CacheConstants.GetRolsFromCache);
                CurrentSession.Clear();
                return Json(new { sonuc = true });
            }
            else
            {
                return Json(new { sonuc = false, mesaj = "Şifre Değiştirme Esnasında bir hata meydana geldi" });
            }
        }

        [Auth]
        public ActionResult Create()
        {
            EKisiDepartmansRolsYoneticisDTO eKisiDepartmansRolsYoneticisDto = new EKisiDepartmansRolsYoneticisDTO()
            {
                EDepartmanDtos = CacheHelper.GetDepartmansFromCache(),
                ERolDtos = CacheHelper.GetRolsFromCache(),
                EYoneticisDtos = ServiceManager.RestSharpGet<List<EKisilerDTO>>(ApiUrlConstants.GetYoneticiNotContainsKisiId+"?id=")
            };
            return View(eKisiDepartmansRolsYoneticisDto);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EKisiDepartmansRolsYoneticisDTO model)
        {
            if (ModelState.IsValid)
            {
                if (ServiceManager.RestSharpPost<int>(ApiUrlConstants.KisiCreate, model.eKisilerDto) > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    model.EDepartmanDtos = CacheHelper.GetDepartmansFromCache();
                    model.ERolDtos = CacheHelper.GetRolsFromCache();
                    model.EYoneticisDtos = ServiceManager.RestSharpGet<List<EKisilerDTO>>(ApiUrlConstants.GetYoneticiNotContainsKisiId + "?id=" + model.eKisilerDto.KisiId);
                    return View(model);
                }
            }
            model.EDepartmanDtos = CacheHelper.GetDepartmansFromCache();
            model.ERolDtos = CacheHelper.GetRolsFromCache();
            model.EYoneticisDtos = ServiceManager.RestSharpGet<List<EKisilerDTO>>(ApiUrlConstants.GetYoneticiNotContainsKisiId + "?id=" + model.eKisilerDto.KisiId);
            return View(model);
        }

    }
}