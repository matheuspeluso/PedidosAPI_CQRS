using PedidosApp.API.Commands;
using PedidosApp.API.Contexts;
using MediatR;
using PedidosApp.API.Entities;
using PedidosApp.API.Enums;
using PedidosApp.API.Models;

namespace PedidosApp.API.Handlers
{
    public class PedidoRequestHandler(SqlServerContexts context, PedidoNotificationHandler notification, IMediator mediator) : 
        IRequestHandler<PedidoCreateCommand>, 
        IRequestHandler<PedidoUpdateCommand>, 
        IRequestHandler<PedidoDeleteCommand>
    {
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

            //Gerando notificação para que o pedido seja salvo no MongoDB
            var notification = new PedidoNotification
            {
                Action = ActionNotification.PedidoCriado,
                Pedidos = new Pedidos
                {
                    Id = pedido.Id.ToString(),
                    NomeCliente = pedido.NomeCliente,
                    Valor = pedido.Valor,
                    Observacoes = pedido.Observacoes,
                    DataHoraPedido = DateTime.Now
                }
            };

            await mediator.Publish(notification);
        }

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

            //Gerando notificação para que o pedido seja atualizado no MongoDB
            var notification = new PedidoNotification
            {
                Action = ActionNotification.PedidoAlterado,
                Pedidos = new Pedidos
                {
                    Id = pedido.Id.ToString(),
                    NomeCliente = pedido.NomeCliente,
                    Valor = pedido.Valor,
                    Observacoes = pedido.Observacoes,
                    DataHoraPedido = DateTime.Now
                }
            };

            await mediator.Publish(notification);
        }


        public async Task Handle(PedidoDeleteCommand request, CancellationToken cancellationToken)
        {
            var pedido = context.Pedidos.Find(request.Id);

            if(pedido is null)
                throw new ApplicationException("Pedido não encontrado");

            context.Remove(pedido); //remove o pedido do banco de dados
            await context.SaveChangesAsync();//Executando a gravação no banco de dados

            //Gerando notificação para que o pedido seja removido do MongoDB
            var notification = new PedidoNotification
            {
                Action = ActionNotification.PedidoExcluido,
                Pedidos = new Pedidos
                {
                    Id = pedido.Id.ToString(),
                    NomeCliente = pedido.NomeCliente,
                    Valor = pedido.Valor,
                    Observacoes = pedido.Observacoes,
                    DataHoraPedido = DateTime.Now
                }
            };

            await mediator.Publish(notification);
        }
    }
}
