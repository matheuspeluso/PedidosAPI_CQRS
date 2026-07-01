using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedidosApp.API.Entities;

namespace PedidosApp.API.Mappings
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("PEDIDOS");                
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("ID");

            builder.Property(p => p.NomeCliente)
                .HasColumnName("NOME_CLIENTE")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.DataPedido)
                .HasColumnName("DATA_PEDIDO")
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasColumnName("VALOR")
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(p => p.Observacoes)
                .HasColumnName("OBSERVACOES")
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
