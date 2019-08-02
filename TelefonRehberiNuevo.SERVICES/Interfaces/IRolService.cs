using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberiNuevo.DTO.EEntity;

namespace TelefonRehberiNuevo.SERVICES.Interfaces
{
    public interface IRolService
    {
        /// <summary>
        /// Bütün Rolleri Getir
        /// </summary>
        /// <returns></returns>
        List<ERolDTO> GetRols();

        /// <summary>
        /// İlgili Rolü Güncelle
        /// </summary>
        /// <param name="eRolDto"></param>
        /// <returns></returns>
        int RolGuncelle(ERolDTO eRolDto);
    }
}
