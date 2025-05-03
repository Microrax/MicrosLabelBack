using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Application.Properties;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.LabelConfiguration
{
    public class AddLabelConfigurationCommandHandler : ICommandHandler<AddLabelConfigurationCommand>
    {
        private readonly ILabelConfigurationRepository _labelConfigurationRepository;
        private readonly ILabelConfigurationQueryRepository _labelConfigurationQueryRepository;
        private readonly ILabelQueryRepository _labelQueryRepository;
        private readonly IClientQueryRepository _clientQueryRepository;

        public AddLabelConfigurationCommandHandler(ILabelConfigurationRepository labelConfigurationRepository,
            ILabelConfigurationQueryRepository labelConfigurationQueryRepository,
            ILabelQueryRepository labelQueryRepository,
            IClientQueryRepository clientQueryRepository)
        {
            _labelConfigurationRepository = labelConfigurationRepository;
            _labelConfigurationQueryRepository = labelConfigurationQueryRepository;
            _labelQueryRepository = labelQueryRepository;
            _clientQueryRepository = clientQueryRepository;
        }

        public async Task HandleAsync(AddLabelConfigurationCommand command)
        {

            var label = await _labelQueryRepository.GetByIdAsync(command.Discriminator);
            if(label == null)
            {
                throw new InvalidOperationException(Resources.ExistLabelConfAdd);
            }
            var client = await _clientQueryRepository.GetByIdAsync(command.Codclient);
            if(client == null)
            {
                throw new InvalidOperationException(Resources.ExistClientConfAdd);
            }
            var config = await _labelConfigurationQueryRepository.GetByBothId(command.Codclient, command.Discriminator);
            if (config != null)
            {
                throw new InvalidOperationException(Resources.ExistConfigurationAdd);
            }
            var configuration = new LabelConfigurations(command.Codclient, command.Discriminator);
            await _labelConfigurationRepository.CreateAsync(configuration);
        }
    }
}
