using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Properties;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;

namespace MicrosLabel.Application.Commands.LabelsCommands
{
    public class AddLabelCommandHandler : ICommandHandler<AddLabelCommand>
    {
        private readonly ILabelsModelRepository _labelsModelRepository;
        private readonly ILabelQueryRepository _labelsQueryRepository;

        public AddLabelCommandHandler(ILabelsModelRepository labelsModelRepository,
            ILabelQueryRepository labelsQueryRepository)
        {
            _labelsModelRepository = labelsModelRepository;
            _labelsQueryRepository = labelsQueryRepository;
        }


        public async Task HandleAsync(AddLabelCommand command)
        {
            LabelsModel lbmodel = new LabelsModel(
            labelType: command.LabelType,
             template: command.Template,
             zpl: command.Zpl,
             dpi: command.Dpi,
             sql: command.Sql
             );

            var existlabel = await _labelsQueryRepository.ExistLabel(lbmodel.LabelType, lbmodel.Template, lbmodel.Dpi);
            if( existlabel != null )
            {
                throw new InvalidOperationException(Resources.AlExistLabel);
            }

            await _labelsModelRepository.CreateAsync(lbmodel);



            await Task.CompletedTask;
        }
    }
}
