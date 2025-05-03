using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Infrastructure.Configurations;
using MicrosLabel.Infrastructure.CosmosDb;
using static MicrosLabel.Infrastructure.CosmosDb.CosmosContainer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicrosLabel.Infrastructure.Repositories
{
    public class ClientRepository : IClientsModelRepository
    {
        private readonly CosmosDbRepository<ClientsModel, ClientsEntity> _cosmosDBRepository;

        public ClientRepository(CosmosDbRepository<ClientsModel, ClientsEntity> cosmosDBRepository)
        {
            _cosmosDBRepository = cosmosDBRepository;
        }

        public async Task<ClientsModel> CreateAsync(ClientsModel client)
        {
            return await _cosmosDBRepository.AddItemAsync(client);
        }

        public async Task DeleteAsync(ClientsModel client)
        {
            await _cosmosDBRepository.DeleteItemAsync(client);
        }

        //
        //getters

        public async Task<ClientsModel?> GetByIdAsync(string id)
        {
            return await _cosmosDBRepository.GetItemAsync(ContainerNames.Clients, PartitionKeys.Clients, c => c.Id == id);
        }

        public async Task<IEnumerable<ClientsModel>>? GetAllAsync()
        {
            return await _cosmosDBRepository.GetAllItemsAsync(ContainerNames.Clients, PartitionKeys.Clients);
        }

        public async Task<ClientsModel?> GetByNombreAsync(string nombre)
        {
            return await _cosmosDBRepository.GetItemAsync(ContainerNames.Clients, PartitionKeys.Clients, c => c.Nombre == nombre);
        }

        public async Task<ClientsModel?> GetByClientCodeAsync(string clientCode)
        {
            return await _cosmosDBRepository.GetItemAsync(ContainerNames.Clients, PartitionKeys.Clients, c => c.ClientCode == clientCode);
        }
    }
}
