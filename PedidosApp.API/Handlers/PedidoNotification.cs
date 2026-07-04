using MediatR;
using PedidosApp.API.Enums;
using PedidosApp.API.Models;

namespace PedidosApp.API.Handlers
{
    public class PedidoNotification : INotification
    {
        public Pedidos? Pedidos { get; set; }
        public ActionNotification Action { get; set; }
    }
}
