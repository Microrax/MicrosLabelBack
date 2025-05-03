using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;
using MicrosLabel.Reads.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.ClientCommand
{
    public class GetAllClientsQueryHandler : IQueryHandler<GetAllClientsQuery, IEnumerable<ClientsViewModel>>
    {
        private readonly IClientQueryRepository _clientsQueryRepository;

        public GetAllClientsQueryHandler(IClientQueryRepository clientsQueryRepository)
        {
            _clientsQueryRepository = clientsQueryRepository;
        }

        public Task<IEnumerable<ClientsViewModel>> HandleAsync(GetAllClientsQuery command)
        {
            return _clientsQueryRepository.GetAllAsync();
        }
    }
}
