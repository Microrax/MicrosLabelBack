using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.ViewModels;

namespace MicrosLabel.Application.Commands.LabelsCommands
{
    public class GetAllLabelsCommand : IQuery<IEnumerable<LabelViewModel>>
    {

        public GetAllLabelsCommand()
        {
        }
    }
}
