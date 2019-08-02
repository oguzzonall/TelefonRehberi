using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberiNuevo.DATA.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Sorgu işlemleri için GetAll metodunu kullanıyoruz
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Id'e göre veri çekmek için find metodu.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(int id);

        /// <summary>
        /// Gönderilen Entity'e göre Kayıt işlemi
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Gönderilen Entity'e göre güncelleme işlemi
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Gönderilen Entity'e göre Silme işlemi
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Değişikleri kaydet(1 tablodaki işlem için kullan)
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
