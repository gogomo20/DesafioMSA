using DesafioMSA.Application.MyMediator.Interfaces;
using DesafioMSA.Application.Responses;
using DesafioMSA.Application.UseCases.Clients.Commands;
using DesafioMSA.Application.UseCases.Clients.Queries;
using DesafioMSA.Domain.Repositories.Views;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DesafioMSA.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<long>))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateClientCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListQueryResponse<ICollection<ClientView>>))]
        public async Task<IActionResult> ListAsync([FromQuery] ListClientQuery query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<ClientView>))]
        public async Task<IActionResult> GetAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            var query = new GetClientByIdQuery { Id = id };
            var response = await _mediator.Send(query, cancellationToken);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<long>))]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UpdateClientCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var response = await _mediator.Send(command, cancellationToken);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponseNoData))]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            var command = new DeleteClientCommand { Id = id };
            var response = await _mediator.Send(command, cancellationToken);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
