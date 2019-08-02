using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberiNuevo.CORE;
using TelefonRehberiNuevo.CORE.Entities;

namespace TelefonRehberiNuevo.DATA.Context
{
    public partial class MyArchContext : DbContext
    {
        private readonly MyArchContext _context;

        public MyArchContext()
            : base("name=MyArchEntities")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Kisiler> Kisiler { get; set; }
        public virtual DbSet<Departman> Departman { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kisiler>().ToTable("Kisiler", "dbo");
            modelBuilder.Entity<Departman>().ToTable("Departman", "dbo");
            modelBuilder.Entity<Rol>().ToTable("Rol", "dbo");
            base.OnModelCreating(modelBuilder);
        }

    }
}
