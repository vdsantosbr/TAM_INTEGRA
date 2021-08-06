using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : 
            base("name=DefaultConnection")
        {
            Database.SetInitializer<DatabaseContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
            
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
        }

        public DbSet<UsuarioModuloSituacao> UsuarioModuloSituacoes { get; set; }
        public DbSet<UsuarioPerfilSituacao> UsuarioPerfilSituacoes { get; set; }
        public DbSet<UsuarioModulo> UsuarioModulos { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfis { get; set; }
    }
}
