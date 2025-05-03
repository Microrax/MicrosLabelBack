using MicrosLabel.Domain.Aggregates.ClientConfigurations;
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
    public sealed class LabelConfigurationQueryRepository : ILabelConfigurationQueryRepository
    {
        private readonly Database _cosmosDb;

        public LabelConfigurationQueryRepository(Database cosmosDB)
        {
            _cosmosDb = cosmosDB;
        }

        public async Task<IEnumerable<LabelConfigurationsViewModel?>> GetAllByClientnId(string codclient)
        {
            string sql = @"SELECT CC.id, CC.Codclient, CC.Discriminator FROM ClientsConfigurations CC WHERE CC.Codclient = " + "'" + codclient + "'";

            var query = new QueryDefinition(sql);

            var configurationContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.ClientConfigurations, $"/{PartitionKeys.ClientConfigurations}");
            return await configurationContainer.Container.QueryAsync<LabelConfigurationsViewModel>(query);
        }

        public async Task<IEnumerable<LabelConfigurationsViewModel?>> GetAllByLabelId(string idLabel)
        {
            string sql = @"SELECT CC.id, CC.Codclient, CC.Discriminator FROM ClientsConfigurations CC WHERE CC.Discriminator = " + "'" + idLabel + "'";

            var query = new QueryDefinition(sql);

            var configurationContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.ClientConfigurations, $"/{PartitionKeys.ClientConfigurations}");
            return await configurationContainer.Container.QueryAsync<LabelConfigurationsViewModel>(query);
        }

        public async Task<LabelConfigurationsViewModel?> GetByBothId(string codclient, string discriminator)
        {
            string sql = @"SELECT CC.id, CC.Codclient, CC.Discriminator FROM ClientsConfigurations CC WHERE CC.Codclient = '"+ codclient +"' AND CC.Discriminator = '"+ discriminator +"'";

            var query = new QueryDefinition(sql);

            var configurationContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.ClientConfigurations, $"/{PartitionKeys.ClientConfigurations}");
            return await configurationContainer.Container.QuerySingleOrDefaultAsync<LabelConfigurationsViewModel>(query);
        }

        public async Task<LabelConfigurations?> GetByClientId(string codclient)
        {
            string sql = @"SELECT CC.id, CC.Codclient, CC.Discriminator FROM ClientsConfigurations CC WHERE CC.Codclient = '"+ codclient +"'";

            var query = new QueryDefinition(sql);

            var configurationContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.ClientConfigurations, $"/{PartitionKeys.ClientConfigurations}");
            return await configurationContainer.Container.QuerySingleOrDefaultAsync<LabelConfigurations>(query);
        }

        public async Task<LabelConfigurations?> GetByDiscriminatorId(string discriminator)
        {
            string sql = @"SELECT CC.id, CC.Codclient, CC.Discriminator FROM ClientsConfigurations CC WHERE CC.Discriminator = '" + discriminator + "'";

            var query = new QueryDefinition(sql);

            var configurationContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.ClientConfigurations, $"/{PartitionKeys.ClientConfigurations}");
            return await configurationContainer.Container.QuerySingleOrDefaultAsync<LabelConfigurations>(query);
        }
    }
}
