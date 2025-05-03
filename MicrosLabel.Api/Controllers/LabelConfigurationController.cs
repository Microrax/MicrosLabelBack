using MicrosLabel.Api.Models.Client;
using MicrosLabel.Api.Models.LabelConfiguration;
using MicrosLabel.Application.Commands.ClientCommand;
using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Application.Commands.LabelConfiguration;
using MicrosLabel.Application.Commands.LabelConfigurationCommand;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MicrosLabel.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/labels/configuration")]
    public class LabelConfigurationController : ControllerBase
    {
        public readonly ICommandBus _commandBus;
        public readonly IQueryBus _queryBus;

        public LabelConfigurationController(ICommandBus commandBus, IQueryBus QueryBus)
        {
            _commandBus = commandBus;
            _queryBus = QueryBus;
        }

        [HttpPut]
        public async Task<IActionResult> PutLabelConfiguration([FromBody] LabelConfigurationModel model)
        {
            PutLabelConfigurationCommand command = new PutLabelConfigurationCommand(model.Codclient, model.Discriminator);
            await _commandBus.SendAsync(command);
            return Ok();
        }
        [HttpPost("{Clientid}/{Labelid}")]
        public async Task<IActionResult> AddLabelConfiguration(string clientid, string labelid)
        {
            AddLabelConfigurationCommand command = new AddLabelConfigurationCommand(clientid, labelid);
            await _commandBus.SendAsync(command);
            return NoContent();
        }

        [HttpDelete("{Clientid}/{Labelid}")]
        public async Task<IActionResult> DeleteLabelConfiguration(string clientid, string labelid)
        {
            DeleteLabelConfigurationCommand command = new DeleteLabelConfigurationCommand(clientid, labelid);
            await _commandBus.SendAsync(command);
            return NoContent();
        }

        [HttpGet("{ClientId}")]
        public async Task<IEnumerable<LabelConfigurationsViewModel>> GetAllClientLabelConfiguration([StringLength(100, ErrorMessage = "NameMaxLength")] string clientId)
        {
            return await _queryBus.SendAsync<GetAllLabelConfigurationQuery, IEnumerable<LabelConfigurationsViewModel>>(new GetAllLabelConfigurationQuery(clientId));
        }
        
        
    }
}
