using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Application.Properties;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.LabelConfiguration
{
    public class DeleteLabelConfigurationCommandHandler : ICommandHandler<DeleteLabelConfigurationCommand>
    {
        private readonly ILabelConfigurationRepository _labelConfigurationRepository;
        private readonly ILabelConfigurationQueryRepository _labelConfigurationQueryRepository;

        public DeleteLabelConfigurationCommandHandler(ILabelConfigurationRepository labelConfigurationRepository,
            ILabelConfigurationQueryRepository labelConfigurationQueryRepository)
        {
            _labelConfigurationRepository = labelConfigurationRepository;
            _labelConfigurationQueryRepository = labelConfigurationQueryRepository;
        }

        public async Task HandleAsync(DeleteLabelConfigurationCommand command)
        {
            var config = await _labelConfigurationQueryRepository.GetByBothId(command.Codclient, command.Discriminator);
            if (config == null)
            {
                throw new InvalidOperationException(Resources.ExistConfigurationDel);
            }
            var configuration = new LabelConfigurations(config.Id, config.CodClient, config.Discriminator);
            await _labelConfigurationRepository.DeleteAsync(configuration);
        }
    }  
}
