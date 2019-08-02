using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberiNuevo.DATA.GenericRepository;

namespace TelefonRehberiNuevo.DATA.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Tentity> GetRepository<Tentity>() where Tentity : class;

        /// <summary>
        /// Değişiklikleri kaydet.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Transaction Başlat.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Kayıt esnasında bir sorun olmaz ise transaction durdur.
        /// </summary>
        void Commit();

        /// <summary>
        /// Kayıt esnasında bir sorun olursa değişiklikleri geri al.
        /// </summary>
        void Rollback();
    }
}
