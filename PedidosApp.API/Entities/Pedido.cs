namespace PedidosApp.API.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NomeCliente { get; set; } = string.Empty;
        public DateTime? DataPedido { get; set; } = DateTime.Now;
        public decimal Valor { get; set; } = 0.0m;
        public string Observacoes { get; set; } = string.Empty;
    }
}
