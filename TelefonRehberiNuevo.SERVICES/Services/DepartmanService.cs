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
    public class DepartmanService : IDepartmanService
    {
        private readonly IGenericRepository<Departman> _departmanRepository;
        private readonly IGenericRepository<Kisiler> _kulllaniciGenericRepository;
        private readonly IUnitOfWork _uow;

        public DepartmanService(IUnitOfWork uow)
        {
            _uow = uow;
            _departmanRepository = _uow.GetRepository<Departman>();
            _kulllaniciGenericRepository = _uow.GetRepository<Kisiler>();
        }

        public List<EDepartmanDTO> GetDepartmans()
        {
            List<EDepartmanDTO> departmanDtos = (from d in _departmanRepository.GetAll()
                                                 select new EDepartmanDTO()
                                                 {
                                                     DepartmanAdi = d.DepartmanAdi,
                                                     DepartmanId = d.DepartmanId
                                                 }).ToList();
            if (departmanDtos.Count > 0)
            {
                return departmanDtos;
            }
            else
            {
                return new List<EDepartmanDTO>();
            }
        }

        public EDepartmanDTO GetDeparmantById(int id)
        {
            Departman departman = _departmanRepository.GetAll().FirstOrDefault(x => x.DepartmanId == id);
            if (departman != null)
                return new EDepartmanDTO()
                {
                    DepartmanId = departman.DepartmanId,
                    DepartmanAdi = departman.DepartmanAdi
                };
            return new EDepartmanDTO();
        }

        public int DepartmanUpdate(EDepartmanDTO eDepartmanDto)
        {
            Departman departman = new Departman();
            AutoMapper.Mapper.DynamicMap(eDepartmanDto, departman);
            _departmanRepository.Update(departman);
            return _departmanRepository.SaveChanges();
        }

        public int AreThereAnyEmployees(int id)
        {
            return _kulllaniciGenericRepository.GetAll().Count(x => x.DepartmanID == id);
        }

        public int DepartmanDelete(int id)
        {
            Departman departman = _departmanRepository.Find(id);
            if (departman != null)
            {
                _departmanRepository.Delete(departman);
                return _departmanRepository.SaveChanges();
            }
            return 0;
        }

        public int CreateNewDepartmant(EDepartmanDTO departmanDto)
        {
            Departman newDepartman=new Departman()
            {
                DepartmanAdi = departmanDto.DepartmanAdi
            };
            _departmanRepository.Insert(newDepartman);
           return _departmanRepository.SaveChanges();
        }
    }
}
