using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Properties;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;

namespace MicrosLabel.Application.Commands.LabelsCommands
{
    public class DeleteLabelCommandHandler : ICommandHandler<DeleteLabelCommand>
    {

        private readonly ILabelsModelRepository _labelsModelRepository;
        private readonly ILabelQueryRepository _labelsQueryRepository;
        private readonly ILabelConfigurationQueryRepository _labelConfigurationQueryRepository;
        

        public DeleteLabelCommandHandler(ILabelsModelRepository labelsModelRepository,
            ILabelQueryRepository labelsQueryRepository,
            ILabelConfigurationQueryRepository labelConfigurationQueryRepository)
        {
            _labelsModelRepository = labelsModelRepository;
            _labelsQueryRepository = labelsQueryRepository;
            _labelConfigurationQueryRepository = labelConfigurationQueryRepository;
        }

        public async Task HandleAsync(DeleteLabelCommand command)
        {
            LabelConfigurations labelrelacion = await _labelConfigurationQueryRepository.GetByDiscriminatorId(command.id);
            if(labelrelacion != null)
            {
                throw new InvalidOperationException(Resources.RelactionLabel);
            }
            var label = await _labelsQueryRepository.GetByIdAsync(command.id);
            if (label == null)
            {
                throw new InvalidOperationException(Resources.ExistLabelDel);
            }
            var labelfin = new LabelsModel(command.id, 1, "Standart", "OASHDFASHJF{{zapatilla}}ASDFKKASFJ{{percha}}ASLDHFADJSF{{colesterol}}ASDHFKASHF", 100, "SELECT zapatilla Ze percha P colesterol MALO FROM Asdfasdfasdfasdf WHERE ClientCode = asd AND sku = opipoi");
            await _labelsModelRepository.DeleteAsync(labelfin);

        }
    }
}

