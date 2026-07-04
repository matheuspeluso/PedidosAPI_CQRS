using MediatR;
using MongoDB.Driver;
using PedidosApp.API.Contexts;
using PedidosApp.API.Enums;
using PedidosApp.API.Models;

namespace PedidosApp.API.Handlers
{
    public class PedidoNotificationHandler : INotificationHandler<PedidoNotification>
    {
        private readonly MongoDbContexts _mongoDbContext;

        public PedidoNotificationHandler(MongoDbContexts mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task Handle(PedidoNotification notification, CancellationToken cancellationToken)
        {
            var pedidos = notification.Pedidos;

            switch (notification.Action)
            {
                case ActionNotification.PedidoCriado:
                    //Salvar no mongoDb
                    await _mongoDbContext.Pedidos!.InsertOneAsync(pedidos!);
                    break;

                case ActionNotification.PedidoAlterado:
                    //Atualizar no mongoDb
                    var update = Builders<Pedidos>.Filter.Eq(p => p.Id, pedidos!.Id);
                    await _mongoDbContext.Pedidos!.ReplaceOneAsync(update, pedidos);
                    break;

                case ActionNotification.PedidoExcluido:
                    //Excluir no mongoDb
                    var delete = Builders<Pedidos>.Filter.Eq(p => p.Id, pedidos!.Id);
                    await _mongoDbContext.Pedidos!.DeleteOneAsync(delete);
                    break;
            }
        }
    }
}
