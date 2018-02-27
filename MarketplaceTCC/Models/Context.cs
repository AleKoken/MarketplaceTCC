using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MarketplaceTCC.Models
{
    public class Context : DbContext
    {

        public Context() : base ("DefaultConnection")
        {

        }

       // DESABILITAR CASCATAS
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        
        public System.Data.Entity.DbSet<MarketplaceTCC.Models.Categorias> Categorias { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.Estados> Estados { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.Cidades> Cidades { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.Status> Status { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.Clientes> Clientes { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.Vendedores> Vendedores { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.Marcas> Marcas { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.Produtos> Produtos { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.StatusPedido> StatusPedidoes { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.Pedidos> Pedidos { get; set; }

        public System.Data.Entity.DbSet<MarketplaceTCC.Models.ItensPedido> ItensPedidoes { get; set; }
    }
}