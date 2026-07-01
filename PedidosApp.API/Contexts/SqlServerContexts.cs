using Microsoft.EntityFrameworkCore;
using PedidosApp.API.Entities;
using PedidosApp.API.Mappings;

namespace PedidosApp.API.Contexts
{
    public class SqlServerContexts : DbContext
    {
        public SqlServerContexts(DbContextOptions<SqlServerContexts> context) : base(context)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PedidoMap());
        }
}}
