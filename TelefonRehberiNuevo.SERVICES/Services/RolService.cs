using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberiNuevo.CORE.Entities;
using TelefonRehberiNuevo.DATA.GenericRepository;
using TelefonRehberiNuevo.DATA.UnitOfWork;
using TelefonRehberiNuevo.DTO.EEntity;
using TelefonRehberiNuevo.SERVICES.Interfaces;

namespace TelefonRehberiNuevo.SERVICES.Services
{
    public class RolService : IRolService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Rol> _rolRepository;

        public RolService(IUnitOfWork uow)
        {
            _uow = uow;
            _rolRepository = _uow.GetRepository<Rol>();
        }

        public List<ERolDTO> GetRols()
        {
            List<ERolDTO> kisilerDtos = (from r in _rolRepository.GetAll()
                                         select new ERolDTO()
                                         {
                                             RolId = r.RolId,
                                             RolAdi = r.RolAdi
                                         }).ToList();
            if (kisilerDtos.Count > 0)
            {
                return kisilerDtos;
            }
            else
            {
                return new List<ERolDTO>();
            }
        }

        public int RolGuncelle(ERolDTO eRolDto)
        {
            Rol rol = new Rol();
            AutoMapper.Mapper.DynamicMap(eRolDto, rol);
            _rolRepository.Update(rol);
            return _rolRepository.SaveChanges();
        }
    }
}
