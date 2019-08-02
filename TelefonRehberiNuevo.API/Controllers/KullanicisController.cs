using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TelefonRehberiNuevo.DTO.EEntity;
using TelefonRehberiNuevo.SERVICES.Interfaces;

namespace TelefonRehberiNuevo.API.Controllers
{
    [RoutePrefix("api/Kullanicis")]
    public class KullanicisController : ApiController
    {
        private readonly IKullaniciService _kullaniciService;

        public KullanicisController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        [Route("GetAktifKisiler")]
        [HttpGet]
        public List<EKisilerDTO> GetAktifKisiler()
        {
            return _kullaniciService.KisileriListele().Where(x => x.Aktif == true && x.RolID == 1).ToList();
        }

        [Route("GetAllCalisanKisiler")]
        [HttpGet]
        public List<EKisilerDTO> GetAllCalisanKisiler()
        {
            return _kullaniciService.KisileriListele().Where(x =>x.RolID == 1).ToList();
        }

        [Route("GetKullaniciDetayById")]
        [HttpGet]
        public EKisiDetayDTO GetKullaniciDetayById(int id)
        {
            EKisiDetayDTO kisiDto = _kullaniciService.GetKullaniciDetayById(id);
            return kisiDto;
        }

        [Route("GetKullaniciById")]
        [HttpGet]
        public EKisilerDTO GetKullaniciById(int id)
        {
            EKisilerDTO kisiDto = _kullaniciService.GetKullaniciById(id);
            return kisiDto;
        }
    }
}

