using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidosApp.API.Commands;
using MediatR;

namespace PedidosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController (IMediator mediator) : ControllerBase
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
    }
}
