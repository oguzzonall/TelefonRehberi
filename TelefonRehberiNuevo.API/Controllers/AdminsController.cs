using System.Collections.Generic;
using System.Web.Http;
using TelefonRehberiNuevo.DTO.EEntity;
using TelefonRehberiNuevo.SERVICES.Interfaces;

namespace TelefonRehberiNuevo.API.Controllers
{
    [RoutePrefix("api/Admins")]
    public class AdminsController : ApiController
    {
        private readonly IKullaniciService _kullaniciService;
        private readonly IRolService _rolService;

        public AdminsController(IKullaniciService kullaniciService, IRolService rolService)
        {
            _kullaniciService = kullaniciService;
            _rolService = rolService;
        }

        [Route("GetAdminByNameAndPassword")]
        [HttpPost]
        public EKisiDetayDTO GetAdminByNameAndPassword(EAdminLoginDTO eAdminLoginDto)
        {
            return _kullaniciService.GetAdminByNameAndPassword(eAdminLoginDto);
        }

        [Route("GetYoneticiNotContainsKisiId")]
        [HttpGet]
        public List<EKisilerDTO> GetYoneticiNotContainsKisiId(int? id)
        {
            return _kullaniciService.GetYoneticiNotContainsKisiId(id);
        }

        [Route("KisiUpdate")]
        [HttpPost]
        public int KisiUpdate(EKisilerDTO eKisilerDto)
        {
            return _kullaniciService.KisiUpdate(eKisilerDto);
        }

        [Route("KisiYoneticiMi")]
        [HttpGet]
        public int KisiYoneticiMi(int id)
        {
            return _kullaniciService.KisiYoneticiMi(id);
        }

        [Route("KisiSil")]
        [HttpPost]
        public int KisiSil(EKisilerDTO eKisilerDto)
        {
            return _kullaniciService.KisiSil(eKisilerDto);
        }

        [Route("EskiSifreKontrol")]
        [HttpPost]
        public bool EskiSifreKontrol(string EskiSifre)
        {
            return _kullaniciService.EskiSifreKontrol(EskiSifre);
        }

        [Route("KisiCreate")]
        [HttpPost]
        public int KisiCreate(EKisilerDTO eKisilerDto)
        {
            return _kullaniciService.KisiCreate(eKisilerDto);
        }

        [Route("GetAllRols")]
        [HttpGet]
        public List<ERolDTO> GetAllRols()
        {
            return _rolService.GetRols();
        }

        [Route("RolGuncelle")]
        [HttpPost]
        public int RolGuncelle(ERolDTO eRolDto)
        {
            return _rolService.RolGuncelle(eRolDto);
        }
    }
}
