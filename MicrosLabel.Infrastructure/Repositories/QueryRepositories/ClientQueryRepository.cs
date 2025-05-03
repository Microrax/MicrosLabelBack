using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Infrastructure.CosmosDb.Extensions;
using MicrosLabel.Reads.Queries;
using MicrosLabel.Reads.ViewModels;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MicrosLabel.Infrastructure.CosmosDb.CosmosContainer;

namespace MicrosLabel.Infrastructure.Repositories.QueryRepositories
{
    public sealed class ClientQueryRepository : IClientQueryRepository
    {
        private readonly Database _cosmosDb;

        public ClientQueryRepository(Database cosmosDB)
        {
            _cosmosDb = cosmosDB;
        }

        public async Task<IEnumerable<ClientsViewModel>>? GetAllAsync()
        {
            string sql = @"SELECT C.id, C.ClientCode, C.Nombre FROM clients C";

            var query = new QueryDefinition(sql);

            var clientContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.Clients, $"/{PartitionKeys.Clients}");
            return await clientContainer.Container.QueryAsync<ClientsViewModel>(query);
        }

        public async Task<ClientsModel?> GetByClientCodeAsync(string clientCode)
        {
            string sql = @"SELECT C.id, C.ClientCode, C.Nombre FROM clients C WHERE C.ClientCode = " + "'" + clientCode + "'";

            var query = new QueryDefinition(sql);

            var clientContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.Clients, $"/{PartitionKeys.Clients}");
            return await clientContainer.Container.QuerySingleOrDefaultAsync<ClientsModel>(query);
        }

        public async Task<ClientsModel?> GetByIdAsync(string id)
        {
            string sql = @"SELECT C.id, C.ClientCode, C.Nombre FROM clients C WHERE C.id = " + "'" + id + "'";

            var query = new QueryDefinition(sql);

            var clientContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.Clients, $"/{PartitionKeys.Clients}");
            return await clientContainer.Container.QuerySingleOrDefaultAsync<ClientsModel>(query);
        }

        public async Task<ClientsModel?> GetByNombreAsync(string nombre)
        {
            string sql = @"SELECT C.id, C.ClientCode, C.Nombre FROM clients C WHERE C.Nombre = " + "'" + nombre + "'";

            var query = new QueryDefinition(sql);

            var clientContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.Clients, $"/{PartitionKeys.Clients}");
            return await clientContainer.Container.QuerySingleOrDefaultAsync<ClientsModel>(query);
        }

   
    }
}
