using MediatR;

namespace PedidosApp.API.Commands
{
    public class PedidoCreateCommand : IRequest
    {
        public string? NomeCliente { get; set; }
        public decimal Valor { get; set; }
        public string? Observacoes { get; set; }
    }
}
