using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Application.Commands.LabelsCommand;
using MicrosLabel.Reads.Queries;
using MicrosLabel.Reads.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.ClientCommand
{
    public class GetYesConfigLabelClientsQueryHandler : IQueryHandler<GetYesConfigLabelClientsQuery, IEnumerable<ClientsViewModel>>
    {
        private readonly IClientQueryRepository _clientsQueryRepository;
        private readonly ILabelConfigurationQueryRepository _configQueryRepository;

        public GetYesConfigLabelClientsQueryHandler(IClientQueryRepository clientQueryRepository,
            ILabelConfigurationQueryRepository configsQueryRepository)
        {

            _clientsQueryRepository = clientQueryRepository;
            _configQueryRepository = configsQueryRepository;
        }

        public async Task<IEnumerable<ClientsViewModel>> HandleAsync(GetYesConfigLabelClientsQuery query)
        {
            //LISTAS
            IEnumerable<ClientsViewModel?> clients = await _clientsQueryRepository.GetAllAsync();  //ALL Clients
            IEnumerable<LabelConfigurationsViewModel?> configurationClients = await _configQueryRepository.GetAllByLabelId(query.LabelId); // ALL CLIENTS OBJECT FOR LABEL ID

            //RETURN
            IEnumerable<ClientsViewModel> configurationClientsInCosmos;
            List<ClientsViewModel> configruadas = new List<ClientsViewModel>();

            if (configurationClients.IsNullOrEmpty())
            {
                return null;
            }

            if (clients.Count() == configurationClients.Count())
            {
                return clients;
            }
            foreach (var client in clients)
            {
                if (configurationClients.Any(config => config.CodClient == client.Id))
                {
                    configruadas.Add(client);
                }
            }

            configurationClientsInCosmos = configruadas;

            return configurationClientsInCosmos;
        }
    }
}

