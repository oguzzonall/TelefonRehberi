using System.Collections.Generic;
using System.Linq;
using TelefonRehberiNuevo.CORE;
using TelefonRehberiNuevo.CORE.Entities;
using TelefonRehberiNuevo.DATA.GenericRepository;
using TelefonRehberiNuevo.DATA.UnitOfWork;
using TelefonRehberiNuevo.DTO.EEntity;
using TelefonRehberiNuevo.SERVICES.Interfaces;

namespace TelefonRehberiNuevo.SERVICES.Services
{
    public class KullaniciService : IKullaniciService
    {
        private readonly IGenericRepository<Kisiler> _kullaniciRepository;
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly IGenericRepository<Departman> _departmanRepository;
        private readonly IUnitOfWork _uow;

        public KullaniciService(IUnitOfWork uow)
        {
            _uow = uow;
            _kullaniciRepository = _uow.GetRepository<Kisiler>();
            _rolRepository = _uow.GetRepository<Rol>();
            _departmanRepository = _uow.GetRepository<Departman>();
        }

        public List<EKisilerDTO> KisileriListele()
        {
            List<EKisilerDTO> kisilerDtos = (from u in _kullaniciRepository.GetAll()
                                             select new EKisilerDTO()
                                             {
                                                 Yoneticisi = u.Yoneticisi,
                                                 Aktif = u.Aktif,
                                                 KisiId = u.KisiId,
                                                 Adi = u.Adi,
                                                 Soyadi = u.Soyadi,
                                                 RolID = u.RolID,
                                                 DepartmanID = u.DepartmanID,
                                                 Telefon = u.Telefon
                                             }).ToList();
            if (kisilerDtos.Count > 0)
            {
                return kisilerDtos;
            }
            else
            {
                return new List<EKisilerDTO>();
            }

        }

        public EKisilerDTO GetKullaniciById(int id)
        {
            EKisilerDTO eKisiDto = (from u in _kullaniciRepository.GetAll()
                                    where u.KisiId == id
                                    select new EKisilerDTO()
                                    {
                                        Adi = u.Adi,
                                        Soyadi = u.Soyadi,
                                        Telefon = u.Telefon,
                                        Yoneticisi = u.Yoneticisi,
                                        KisiId = u.KisiId,
                                        RolID = u.RolID,
                                        Aktif = u.Aktif,
                                        DepartmanID = u.DepartmanID
                                    }).FirstOrDefault();
            return eKisiDto;
        }

        public EKisiDetayDTO GetKullaniciDetayById(int id)
        {
            var user = _kullaniciRepository.Find(id);
            if (user == null)
            {
                return null;
            }
            var yonetici = _kullaniciRepository.Find(user.Yoneticisi);

            EKisiDetayDTO eKisilerDto = (from u in _kullaniciRepository.GetAll()
                                         where u.KisiId == user.KisiId
                                         join r in _rolRepository.GetAll() on u.RolID equals r.RolId
                                         join d in _departmanRepository.GetAll() on u.DepartmanID equals d.DepartmanId
                                         select new EKisiDetayDTO()
                                         {
                                             Adi = u.Adi,
                                             RolID = u.RolID,
                                             KisiId = u.KisiId,
                                             Aktif = u.Aktif,
                                             DepartmanID = u.DepartmanID,
                                             Rol = r.RolAdi,
                                             Soyadi = u.Soyadi,
                                             Telefon = u.Telefon,
                                             Yoneticisi = u.Yoneticisi,
                                             DepartmanAdi = d.DepartmanAdi
                                         }).SingleOrDefault();
            if (eKisilerDto != null)
            {
                if (yonetici != null)
                {
                    eKisilerDto.YoneticiAdi = yonetici.Adi + " " + yonetici.Soyadi;
                    return eKisilerDto;
                }

                eKisilerDto.YoneticiAdi = "YOK";
                return eKisilerDto;
            }

            return null;
        }

        public EKisiDetayDTO GetAdminByNameAndPassword(EAdminLoginDTO eAdminLoginDto)
        {
            EKisiDetayDTO admin = (from u in _kullaniciRepository.GetAll()
                                   where u.Adi == eAdminLoginDto.Username
                                   join r in _rolRepository.GetAll() on u.RolID equals r.RolId
                                   where eAdminLoginDto.Password == r.Sifre
                                   select new EKisiDetayDTO()
                                   {
                                       Adi = u.Adi,
                                       RolID = u.RolID,
                                       KisiId = u.KisiId,
                                       Aktif = u.Aktif,
                                       DepartmanID = u.DepartmanID,
                                       Rol = r.RolAdi,
                                       Soyadi = u.Soyadi,
                                       Telefon = u.Telefon,
                                       Yoneticisi = u.Yoneticisi
                                   }).SingleOrDefault();
            return admin;
        }

        public List<EKisilerDTO> GetYoneticiNotContainsKisiId(int? id)
        {
            List<EKisilerDTO> eyoneticisDtos = (from u in _kullaniciRepository.GetAll()
                                                where u.KisiId != id && u.Aktif == true
                                                select new EKisilerDTO()
                                                {
                                                    Adi = u.Adi,
                                                    Soyadi = u.Soyadi,
                                                    Aktif = u.Aktif,
                                                    Yoneticisi = u.Yoneticisi,
                                                    KisiId = u.KisiId,
                                                    RolID = u.RolID,
                                                    DepartmanID = u.DepartmanID,
                                                    Telefon = u.Telefon
                                                }).ToList();
            return eyoneticisDtos;
        }

        public int KisiUpdate(EKisilerDTO eKisilerDto)
        {
            Kisiler kisi = _kullaniciRepository.Find(eKisilerDto.KisiId);
            if (kisi == null)
            {
                return -1;
            }

            AutoMapper.Mapper.DynamicMap(eKisilerDto, kisi);
            _kullaniciRepository.Update(kisi);
            return _kullaniciRepository.SaveChanges();
        }

        public int KisiYoneticiMi(int id)
        {
            return _kullaniciRepository.GetAll().Where(x => x.Yoneticisi == id).ToList().Count();
        }

        public int KisiSil(EKisilerDTO eKisilerDto)
        {
            Kisiler kisi = new Kisiler();
            AutoMapper.Mapper.DynamicMap(eKisilerDto, kisi);
            _kullaniciRepository.Delete(kisi);
            return _kullaniciRepository.SaveChanges();
        }

        public bool EskiSifreKontrol(string EskiSifre)
        {
            if (_rolRepository.GetAll().Any(x => x.Sifre == EskiSifre))
                return true;
            return false;
        }

        public int KisiCreate(EKisilerDTO eKisilerDto)
        {
            Kisiler kisi = new Kisiler();
            AutoMapper.Mapper.DynamicMap(eKisilerDto, kisi);
            _kullaniciRepository.Insert(kisi);
            return _kullaniciRepository.SaveChanges();
        }
    }
}
