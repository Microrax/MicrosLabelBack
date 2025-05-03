using MicrosLabel.Api.Models.Client;
using MicrosLabel.Application.Commands.ClientCommand;
using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MicrosLabel.Reads.ViewModels;
using System.Drawing.Drawing2D;
using System.ComponentModel.DataAnnotations;
using MicrosLabel.Application.Commands.LabelsCommand;

namespace MicrosLabel.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/clients")]
    public class ClientController : ControllerBase
    {
        public readonly ICommandBus _commandBus;
        public readonly IQueryBus _queryBus;

        public ClientController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody]AddClientModel model)
        {
            AddClientCommand command = new AddClientCommand(model.ClientCode, model.Nombre);
            await _commandBus.SendAsync(command);
            return Ok();
        }

        [HttpDelete("{LabelId}")]
        public async Task<IActionResult> DeleteClient([StringLength(100, ErrorMessage = "NameMaxLength")] string labelId)
        {
            DeleteClientCommand command = new DeleteClientCommand(labelId);
            await _commandBus.SendAsync(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<ClientsViewModel>> GetClients()
        {
            return await _queryBus.SendAsync<GetAllClientsQuery, IEnumerable<ClientsViewModel>>(new GetAllClientsQuery());
        }

        //TODOS LOS LABELS QUE NO TENGAN CONFIGURACIÓN CON EL CLIENTE
        [HttpGet("yesconfig/{LabelId}")]
        public async Task<IEnumerable<ClientsViewModel>> GetAllLabelLabelConfiguration([StringLength(100, ErrorMessage = "NameMaxLength")] string labelId)
        {
            return await _queryBus.SendAsync<GetYesConfigLabelClientsQuery, IEnumerable<ClientsViewModel>>(new GetYesConfigLabelClientsQuery(labelId));
        }
    }
}