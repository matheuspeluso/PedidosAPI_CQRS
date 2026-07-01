using MediatR;

namespace PedidosApp.API.Commands
{
    public class PedidoDeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
