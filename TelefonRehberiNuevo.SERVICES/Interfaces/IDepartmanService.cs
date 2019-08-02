using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberiNuevo.DTO.EEntity;

namespace TelefonRehberiNuevo.SERVICES.Interfaces
{
    public interface IDepartmanService
    {
        /// <summary>
        /// Departmanları Getir.
        /// </summary>
        /// <returns></returns>
        List<EDepartmanDTO> GetDepartmans();

        /// <summary>
        /// İd'ye göre Departman getir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EDepartmanDTO GetDeparmantById(int id);

        /// <summary>
        /// Gelen Departmanı Güncelle.
        /// </summary>
        /// <param name="eDepartmanDto"></param>
        /// <returns></returns>
        int DepartmanUpdate(EDepartmanDTO eDepartmanDto);

        /// <summary>
        /// Bu Departmanda çalışan biri yada birileri varsa onun sayısını döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int AreThereAnyEmployees(int id);

        /// <summary>
        /// Gelen Id'ye Göre İlgili Departmanı Sil.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DepartmanDelete(int id);

        /// <summary>
        /// Yeni bir Departman Oluştur.
        /// </summary>
        /// <param name="departmanDto"></param>
        /// <returns></returns>
        int CreateNewDepartmant(EDepartmanDTO departmanDto);
    }
}
