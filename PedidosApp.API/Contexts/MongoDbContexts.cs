using MongoDB.Driver;
using PedidosApp.API.Models;

namespace PedidosApp.API.Contexts
{
    public class MongoDbContexts
    {
        private readonly IConfiguration _configuration;

        public MongoDbContexts(IConfiguration configuration)
        {
            _configuration = configuration;

            //Captura a connection string do banco de dados
            var mongoClient = new MongoClient(_configuration.GetConnectionString("MongoDbConnection"));

            //Nome do banco de dados
            var mongoDatabase = mongoClient.GetDatabase("DbPedidos");

            //Mapear a collection onde os dados serão salvos no MongoDB
            Pedidos = mongoDatabase.GetCollection<Pedidos>("Pedidos");

        }
        public IMongoCollection<Pedidos>? Pedidos { get; set; }
    }
}
