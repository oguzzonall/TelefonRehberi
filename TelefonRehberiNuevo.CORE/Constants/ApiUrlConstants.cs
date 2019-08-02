using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberiNuevo.CORE.Constants
{
    public static class ApiUrlConstants
    {
        public const string BaseUrl = "http://localhost:56650/api/";

        #region Kullanicis Urls
        public const string GetAktifKisiler = "Kullanicis/GetAktifKisiler";
        public const string GetAllCalisanKisiler = "Kullanicis/GetAllCalisanKisiler";
        public const string GetKullaniciById = "Kullanicis/GetKullaniciById";
        public const string GetKullaniciDetayById = "Kullanicis/GetKullaniciDetayById";
        #endregion


        #region Admin Urls
        public const string GetAdminByNameAndPassword = "Admins/GetAdminByNameAndPassword";
        public const string GetYoneticiNotContainsKisiId = "Admins/GetYoneticiNotContainsKisiId";
        public const string KisiUpdate = "Admins/KisiUpdate";
        public const string KisiYoneticiMi = "Admins/KisiYoneticiMi";
        public const string KisiSil = "Admins/KisiSil";
        public const string EskiSifreKontrol = "Admins/EskiSifreKontrol";
        public const string KisiCreate = "Admins/KisiCreate";
        public const string RolGuncelle = "Admins/RolGuncelle";
        public const string GetAllRols = "Admins/GetAllRols";


        #endregion

        #region Departman Urls
        public const string GetDepartmans = "Departmans/GetDepartmans";
        public const string GetDeparmantById = "Departmans/GetDeparmantById";
        public const string DepartmanUpdate = "Departmans/DepartmanUpdate";
        public const string AreThereAnyEmployees = "Departmans/AreThereAnyEmployees";
        public const string DepartmanDelete = "Departmans/DepartmanDelete";
        public const string CreateNewDepartmant = "Departmans/CreateNewDepartmant";


        #endregion
    }
}
