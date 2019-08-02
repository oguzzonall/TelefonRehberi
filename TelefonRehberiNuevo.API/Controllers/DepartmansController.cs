using System.Collections.Generic;
using System.Web.Http;
using TelefonRehberiNuevo.DTO.EEntity;
using TelefonRehberiNuevo.SERVICES.Interfaces;

namespace TelefonRehberiNuevo.API.Controllers
{
    [RoutePrefix("api/Departmans")]
    public class DepartmansController : ApiController
    {
        private readonly IDepartmanService _departmanService;

        public DepartmansController(IDepartmanService departmanService)
        {
            _departmanService = departmanService;
        }

        [Route("AreThereAnyEmployees")]
        [HttpGet]
        public int AreThereAnyEmployees(int id)
        {
            return _departmanService.AreThereAnyEmployees(id);
        }

        [Route("GetDepartmans")]
        [HttpGet]
        public List<EDepartmanDTO> GetDepartmans()
        {
            return _departmanService.GetDepartmans();
        }

        [Route("GetDeparmantById")]
        [HttpGet]
        public EDepartmanDTO GetDeparmantById(int id)
        {
            return _departmanService.GetDeparmantById(id);
        }

        [Route("DepartmanUpdate")]
        [HttpPost]
        public int DepartmanUpdate(EDepartmanDTO eDepartmanDto)
        {
            return _departmanService.DepartmanUpdate(eDepartmanDto);
        }


        [Route("DepartmanDelete")]
        [HttpGet]
        public int DepartmanDelete(int id)
        {
            return _departmanService.DepartmanDelete(id);
        }


        [Route("CreateNewDepartmant")]
        [HttpPost]
        public int CreateNewDepartmant(EDepartmanDTO departmanDto)
        {
            return _departmanService.CreateNewDepartmant(departmanDto);
        }

    }
}
