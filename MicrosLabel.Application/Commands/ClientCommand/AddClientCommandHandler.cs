using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Application.Properties;
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
    public class AddClientCommandHandler : ICommandHandler<AddClientCommand>
    {
        private readonly IClientsModelRepository _clientsModelRepository;
        private readonly IClientQueryRepository _clientsQueryRepository;

        public AddClientCommandHandler(IClientsModelRepository clientsModelRepository, IClientQueryRepository clientQueryRepository)
        {
            _clientsModelRepository = clientsModelRepository;
            _clientsQueryRepository = clientQueryRepository;
        }


        public async Task HandleAsync(AddClientCommand command)
        {
            var client = new ClientsModel();
            client = await _clientsQueryRepository.GetByClientCodeAsync(command.ClientCode);
            if (client != null)
            {
                throw new InvalidOperationException(Resources.ExistClientCode);
            }
            var client2 = new ClientsModel();
            client2 = await _clientsQueryRepository.GetByNombreAsync(command.Nombre);
            if (client2 != null)
            {
                throw new InvalidOperationException(Resources.ExistClientName);
            }
            var cliente = new ClientsModel(command.ClientCode, command.Nombre);

            await _clientsModelRepository.CreateAsync(cliente);
        }
    }
}
