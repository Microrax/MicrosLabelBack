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

namespace MicrosLabel.Application.Commands.ClientCommand
{
    public class DeleteClientCommandHandler : ICommandHandler<DeleteClientCommand>
    {
        private readonly IClientsModelRepository _clientsModelRepository;
        private readonly IClientQueryRepository _clientsQueryRepository;
        private readonly ILabelConfigurationQueryRepository _labelConfigurationQueryRepository;

        public DeleteClientCommandHandler(IClientsModelRepository clientsModelRepository,
            IClientQueryRepository clientsQueryRepository,
            ILabelConfigurationQueryRepository labelConfigurationQueryRepository)
        {
            _clientsModelRepository = clientsModelRepository;
            _clientsQueryRepository = clientsQueryRepository;
            _labelConfigurationQueryRepository = labelConfigurationQueryRepository;
        }

        public async Task HandleAsync(DeleteClientCommand command)
        {
            var clientrelacion = await _labelConfigurationQueryRepository.GetByClientId(command.Id);
            if (clientrelacion != null)
            {
                throw new InvalidOperationException(Resources.RelactionClient);
            }
            var client = await _clientsQueryRepository.GetByIdAsync(command.Id);
            if (client == null)
            {
                throw new InvalidOperationException(Resources.ExistClient);
            }
            var cliente = new ClientsModel(command.Id, "asdfasdf", "asdfasdf");
            await _clientsModelRepository.DeleteAsync(cliente);
        }
    }
}
