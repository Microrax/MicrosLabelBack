using MicrosLabel.Api.Models.Client;
using MicrosLabel.Api.Models.Label;
using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Application.Commands.LabelConfiguration;
using MicrosLabel.Application.Commands.LabelConfigurationCommand;
using MicrosLabel.Application.Commands.LabelsCommand;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.OtherModels;
using MicrosLabel.Reads.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MicrosLabel.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/labels")]
    public class LabelController : ControllerBase
    {
        public readonly ICommandBus _commandBus;
        public readonly IQueryBus _queryBus;

        public LabelController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpPut]
        public async Task<IActionResult> PutLabel([FromBody] PutLabelModel model)
        {
            PutLabelCommand command = new PutLabelCommand(model.id,model.LabelType, model.Template, model.Zpl, model.Dpi, model.Sql);
            await _commandBus.SendAsync(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddLabel([FromBody] AddLabelModel model)
        {
            AddLabelCommand command = new AddLabelCommand(model.LabelType, model.Template, model.Zpl, model.Dpi, model.Sql);
            await _commandBus.SendAsync(command);
            return NoContent();
        }

        [HttpDelete("{ClientID}")]
        public async Task<IActionResult> DeleteLabel(string clientId)
        {
            DeleteLabelCommand command = new DeleteLabelCommand(clientId);
            await _commandBus.SendAsync(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<LabelViewModel>> GetAllLabels()
        {
            return await _queryBus.SendAsync<GetAllLabelsCommand, IEnumerable<LabelViewModel>>(new GetAllLabelsCommand());
        }


        //TODOS LOS LABELS QUE NO TENGAN CONFIGURACIÓN CON EL CLIENTE
        [HttpGet("noconfig/{ClientID}")]
        public async Task<IEnumerable<LabelViewModel>> GetAllLabelLabelConfiguration([StringLength(100, ErrorMessage = "NameMaxLength")] string clientID)
        {
            return await _queryBus.SendAsync<GetNoConfigLabelQuery, IEnumerable<LabelViewModel>>(new GetNoConfigLabelQuery(clientID));
        }
    }
}