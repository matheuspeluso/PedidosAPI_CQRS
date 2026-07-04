using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PedidosApp.API.Models
{
    /// <summary>
    /// Modelo de dados do que iremos salvar no MongoDB
    /// </summary>
    public class Pedidos
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }

        [BsonElement("nome_cliente")]
        public string? NomeCliente { get; set; }

        [BsonElement("valor_pedido")]
        public decimal? Valor { get; set; }

        [BsonElement("observacoes_pedido")]
        public string? Observacoes { get; set; }

        [BsonElement("data_hora_pedido")]
        public DateTime? DataHoraPedido { get; set; }
    }
}
