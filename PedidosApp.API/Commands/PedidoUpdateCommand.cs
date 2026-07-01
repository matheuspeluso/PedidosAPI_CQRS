using MediatR;

namespace PedidosApp.API.Commands
{
    public class PedidoUpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? nomeCliente { get; set; }
        public DateTime? DataPedido { get; set; }
        public decimal valor { get; set; }
        public string? Observacoes { get; set; }
    }
}
