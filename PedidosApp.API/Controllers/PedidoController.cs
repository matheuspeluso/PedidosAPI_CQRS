using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidosApp.API.Commands;
using MediatR;
using MongoDB.Driver;
using PedidosApp.API.Models;
using PedidosApp.API.Contexts;

namespace PedidosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController (IMediator mediator, MongoDbContexts mongoDbContext) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PedidoCreateCommand command)
        {
            await mediator.Send(command);
            return Ok(new {message = "Pedido criado com sucesso!"});
        }

        [HttpPatch]
        public async Task<IActionResult> PatchAsync([FromBody] PedidoUpdateCommand command)
        {
            await mediator.Send(command);
            return Ok(new {message = "Pedido atualizado com sucesso!"});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new PedidoDeleteCommand { Id = id };
            await mediator.Send(command);
            return Ok(new {message = "Pedido deletado com sucesso!"});
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            var filter = Builders<Pedidos>.Filter.Empty;
            var pedidos = await mongoDbContext.Pedidos.Find(filter).ToListAsync();

            return Ok(pedidos);        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(string Id)
        {
            var filter = Builders<Pedidos>.Filter.Eq(p => p.Id, Id);

            var pedido = await mongoDbContext.Pedidos.Find(filter).FirstOrDefaultAsync();
            return Ok(pedido);
        }
    }
}
