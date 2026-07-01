using PedidosApp.API.Commands;
using PedidosApp.API.Contexts;
using MediatR;
using PedidosApp.API.Entities;

namespace PedidosApp.API.Handlers
{
    public class PedidoRequestHandler(SqlServerContexts context) : IRequestHandler<PedidoCreateCommand>, IRequestHandler<PedidoUpdateCommand>, IRequestHandler<PedidoDeleteCommand>
    {
        public async Task Handle(PedidoUpdateCommand request, CancellationToken cancellationToken)
        {
            var pedido = await context.Pedidos.FindAsync(request.Id);

            if(pedido is null)
                throw new ApplicationException("Pedido não encontrado");

            if(!string.IsNullOrEmpty(request.nomeCliente))
                pedido.NomeCliente = request.nomeCliente;

            if(request.DataPedido != null)
                pedido.DataPedido = request.DataPedido;

            if(request.valor != null)
                pedido.Valor = request.valor;

            if(!string.IsNullOrEmpty(request.Observacoes))
                pedido.Observacoes = request.Observacoes;

            context.Update(pedido); //atualiza o pedido no banco de dados
            await context.SaveChangesAsync();//Executando a gravação no banco de dados
        }

        public async Task Handle(PedidoCreateCommand request, CancellationToken cancellationToken)
        {
            var pedido = new Pedido
            {
                NomeCliente = request.NomeCliente!,
                Valor = request.Valor!,
                Observacoes = request.Observacoes!,
            };

            await context.AddAsync(pedido); //grva no banco de dados
            await context.SaveChangesAsync();//Executando a gravação no banco de dados
        }

        public async Task Handle(PedidoDeleteCommand request, CancellationToken cancellationToken)
        {
            var pedido = context.Pedidos.Find(request.Id);

            if(pedido is null)
                throw new ApplicationException("Pedido não encontrado");

            context.Remove(pedido); //remove o pedido do banco de dados
            await context.SaveChangesAsync();//Executando a gravação no banco de dados
        }
    }
}
