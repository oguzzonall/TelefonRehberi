using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberiNuevo.DATA.Context;
using TelefonRehberiNuevo.DATA.GenericRepository;

namespace TelefonRehberiNuevo.DATA.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyArchContext _context;
        private bool disposed = false;

        public UnitOfWork(MyArchContext context)
        {
            Database.SetInitializer<MyArchContext>(null);
            if (context == null)
                throw new ArgumentException("context is null");
            _context = context;
        }

        public IGenericRepository<Tentity> GetRepository<Tentity>() where Tentity : class
        {
            return new GenericRepository<Tentity>(_context);
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Transaction Tanımladım.
        DbContextTransaction transaction;
        public void BeginTransaction()
        {
            transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
