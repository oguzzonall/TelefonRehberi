using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberiNuevo.DTO.EEntity;

namespace TelefonRehberiNuevo.SERVICES.Interfaces
{
    public interface IKullaniciService
    {
        /// <summary>
        ///  Kullanıcı bilgilerini Listeler
        /// </summary>
        /// <param name=""></param>
        List<EKisilerDTO> KisileriListele();

        /// <summary>
        /// İd'ye göre kullanıcı bilgilerini getirir.
        /// </summary>
        /// <param name=""></param>
        EKisiDetayDTO GetKullaniciDetayById(int id);
        /// <summary>
        /// Kullanıcı ve şifreye admin varsa bilgilerini döndürüyorum.
        /// </summary>
        /// <param name="eAdminLoginDto"></param>
        /// <returns></returns>
        EKisiDetayDTO GetAdminByNameAndPassword(EAdminLoginDTO eAdminLoginDto);

        /// <summary>
        /// id göre kullanıcın temel bilgilerini dönderir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EKisilerDTO GetKullaniciById(int id);

        /// <summary>
        /// Kisinin kendisi dışındaki aktif olan herkesi getir.O kişinin yöneticisi olma potansiyeli var.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<EKisilerDTO> GetYoneticiNotContainsKisiId(int? id);

        /// <summary>
        /// Gelen kişiyi güncelle
        /// </summary>
        /// <param name="eKisilerDto"></param>
        /// <returns></returns>
        int KisiUpdate(EKisilerDTO eKisilerDto);

        /// <summary>
        /// Kayıtlı kişinin herhangi bir kişinin yöneticisi olup olmadığını bulabiliriz.Dönüş değeri 0'dan büyükse en az bir kişinin yöneticidir deriz.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int KisiYoneticiMi(int id);

        /// <summary>
        /// Gelen Kişiyi Sil
        /// </summary>
        /// <param name="eKisilerDto"></param>
        /// <returns></returns>
        int KisiSil(EKisilerDTO eKisilerDto);

        /// <summary>
        /// Admin şifresinin doğruluğunu kontrol et.
        /// </summary>
        /// <param name="EskiSifre"></param>
        /// <returns></returns>
        bool EskiSifreKontrol(string EskiSifre);

        /// <summary>
        /// Yeni bir kullancı oluştur
        /// </summary>
        /// <param name="eKisilerDto"></param>
        /// <returns></returns>
        int KisiCreate(EKisilerDTO eKisilerDto);
    }
}
