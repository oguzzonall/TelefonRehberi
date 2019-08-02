using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using TelefonRehberiNuevo.CORE.Constants;
using TelefonRehberiNuevo.DTO.EEntity;
using TelefonRehberiNuevo.WEB.Managers;

namespace TelefonRehberiNuevo.WEB.Models
{
    public static class CacheHelper
    {
        public static List<EDepartmanDTO> GetDepartmansFromCache()
        {
            List<EDepartmanDTO> departmans = WebCache.Get(CacheConstants.GetDepartmansFromCache) as List<EDepartmanDTO>;

            if (departmans == null)
            {
                departmans = ServiceManager.RestSharpGet<List<EDepartmanDTO>>(ApiUrlConstants.GetDepartmans);
                WebCache.Set(CacheConstants.GetDepartmansFromCache, departmans, 60, true);
            }

            return departmans;
        }

        public static List<ERolDTO> GetRolsFromCache()
        {
            List<ERolDTO> rols = WebCache.Get(CacheConstants.GetRolsFromCache) as List<ERolDTO>;

            if (rols == null)
            {
                rols = ServiceManager.RestSharpGet<List<ERolDTO>>(ApiUrlConstants.GetAllRols);
                WebCache.Set(CacheConstants.GetRolsFromCache, rols, 60, true);
            }

            return rols;

        }
    }
}