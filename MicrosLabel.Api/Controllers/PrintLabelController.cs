using MicrosLabel.Api.Models.Client;
using MicrosLabel.Api.Models.Label;
using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Application.Commands.PrintLabelsCommand;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.Aggregates.Prints;
using Microsoft.AspNetCore.Mvc;

namespace MicrosLabel.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/labels/print")]
    public class PrintLabelController : ControllerBase
    {
        public readonly ICommandBus _commandBus;

        public PrintLabelController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpPost]
        public async Task<IActionResult> PrintLabel([FromBody] PrintLabelModel label)
        {
            PrintLabelCommand labelsend = new PrintLabelCommand(label.workstationCode, label.itemId, label.labelId, label.clientCode);
            await _commandBus.SendAsync(labelsend);
            return Ok();
        }
    }
}
