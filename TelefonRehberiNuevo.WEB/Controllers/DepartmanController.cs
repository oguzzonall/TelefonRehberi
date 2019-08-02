using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TelefonRehberiNuevo.CORE.Constants;
using TelefonRehberiNuevo.DTO.EEntity;
using TelefonRehberiNuevo.SERVICES.Interfaces;
using TelefonRehberiNuevo.WEB.Filter;
using TelefonRehberiNuevo.WEB.Managers;
using TelefonRehberiNuevo.WEB.Models;

namespace TelefonRehberiNuevo.WEB.Controllers
{
    [Auth]
    [ExceptionFilter]
    public class DepartmanController : Controller
    {
        //_departmanService interfacesini burada kullanmadık.Fakat hızlı sonuçlar için api üzerinden gitmek yerine direk service sınıfına gidebiliriz diye tanımlı bırakıyorum.
        private readonly IDepartmanService _departmanService;

        public DepartmanController(IDepartmanService departmanService)
        {
            _departmanService = departmanService;
        }

        // GET: Departman
        public ActionResult Index()
        {
            //Uzun zaman aralıklarıyla değişecek verileri cache atıp oradan çekmek her zaman daha hızlı ve performanslıdır.
            return View(CacheHelper.GetDepartmansFromCache());
        }

        public ActionResult Edit(int id)
        {

            EDepartmanDTO departman = ServiceManager.RestSharpGet<EDepartmanDTO>(ApiUrlConstants.GetDeparmantById + "?id=" + id);
            if (departman == null || departman.DepartmanId == 0)
            {
                return RedirectToAction("Error404", "Kullanici");
            }
            return View(departman);
        }

        [HttpPost]
        public ActionResult Edit(EDepartmanDTO eDepartmanDto)
        {
            if (ModelState.IsValid)
            {
                if (ServiceManager.RestSharpPost<int>(ApiUrlConstants.DepartmanUpdate, eDepartmanDto) > 0)
                {
                    //Departman verisi sadece veritabanında değişti cache üzerinde değişmedi bu yüzden cache'i sildim ki yukarıdaki Index ActionResultu çağrıldığında cache null olduğu için yeni verileri çekip cacheyi güncellicekti.
                    //Silmek yerine cache burada güncel veriyi çekip atabilirdik ama işi Index kısmına bırakmayı tercih ettim.

                    WebCache.Remove(CacheConstants.GetDepartmansFromCache);

                    return Json(new { sonuc = true, mesaj = "İşleminiz Başarıyla Gerçekleştirildi." });
                }
                else
                {
                    return Json(new { sonuc = false, mesaj = "İşleminiz Gerçekleştirilemedi.Lütfen Tekrar Deneyiniz." });
                }
            }
            return Json(new { sonuc = false, mesaj = "Bilgilerinizi Kontrol Ediniz." });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            EDepartmanDTO departman = ServiceManager.RestSharpGet<EDepartmanDTO>(ApiUrlConstants.GetDeparmantById + "?id=" + id);
            ;
            if (departman == null || departman.DepartmanId == 0)
            {
                return Json(new { sonuc = false, mesaj = "İlgili Departman Sistemde Kayıtlı Değil" });
            }

            int employees = ServiceManager.RestSharpGet<int>(ApiUrlConstants.AreThereAnyEmployees + "?id=" + id);
            if (employees <= 0)
            {
                if (ServiceManager.RestSharpGet<int>(ApiUrlConstants.DepartmanDelete + "?id=" + id) > 0)
                {
                    //Departman verisi sadece veritabanında değişti cache üzerinde değişmedi bu yüzden cache'i sildim ki yukarıdaki Index ActionResultu çağrıldığında cache null olduğu için yeni verileri çekip cacheyi güncellicekti.
                    //Silmek yerine cache burada güncel veriyi çekip atabilirdik ama işi Index kısmına bırakmayı tercih ettim.

                    WebCache.Remove(CacheConstants.GetDepartmansFromCache);
                    return Json(new { sonuc = true, mesaj = "İşleminiz Başarıyla Gerçekleştirildi" });
                }
                else
                {
                    return Json(new { sonuc = false, mesaj = "İşleminiz Gerçekleştirilemedi.Lütfen Tekrar Deneyiniz." });
                }
            }
            else
            {
                return Json(new { sonuc = false, mesaj = "Bu Departmanın Altında " + employees + " Kişi Çalışmaktadır.Silemezsiniz" });
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EDepartmanDTO departman)
        {
            if (ModelState.IsValid)
            {
                if (ServiceManager.RestSharpPost<int>(ApiUrlConstants.CreateNewDepartmant, departman) > 0)
                {
                    //Departman verisi sadece veritabanında değişti cache üzerinde değişmedi bu yüzden cache'i sildim ki yukarıdaki Index ActionResultu çağrıldığında cache null olduğu için yeni verileri çekip cacheyi güncellicekti.
                    //Silmek yerine cache burada güncel veriyi çekip atabilirdik ama işi Index kısmına bırakmayı tercih ettim.
                    WebCache.Remove(CacheConstants.GetDepartmansFromCache);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(departman);
                }
            }

            return View(departman);
        }
    }
}