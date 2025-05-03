using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Infrastructure.Configurations;
using MicrosLabel.Infrastructure.CosmosDb;
using static MicrosLabel.Infrastructure.CosmosDb.CosmosContainer;

namespace MicrosLabel.Infrastructure.Repositories
{
    public class LabelConfigurationRepository : ILabelConfigurationRepository
    {
        private readonly CosmosDbRepository<LabelConfigurations, LabelConfigurationEntity> _cosmosDBRepository;

        public LabelConfigurationRepository(CosmosDbRepository<LabelConfigurations, LabelConfigurationEntity> cosmosDBRepository)
        {
            _cosmosDBRepository = cosmosDBRepository;
        }

        public async Task<LabelConfigurations> CreateAsync(LabelConfigurations labelConfig)
        {
            return await _cosmosDBRepository.AddItemAsync(labelConfig);
        }

        public async Task DeleteAsync(LabelConfigurations labelConfig)
        {
            await _cosmosDBRepository.DeleteItemAsync(labelConfig);
        }

        public async Task UpdateAsync(LabelConfigurations labelConfig)
        {
            await _cosmosDBRepository.UpdateItemAsync(labelConfig);
        }

        //GETS

        public async Task<LabelConfigurations?> GetByBothId(string codclient, string discriminator)
        {
            return await _cosmosDBRepository.GetItemAsync(ContainerNames.ClientConfigurations,
                PartitionKeys.ClientConfigurations, conf => conf.Codclient == codclient && conf.Discriminator == discriminator);
        }

        public async Task<LabelConfigurations?> GetByClientId(string codclient)
        {
            return await _cosmosDBRepository.GetItemAsync(ContainerNames.ClientConfigurations,
                PartitionKeys.ClientConfigurations, client => client.Id == codclient);
        }

        public async Task<LabelConfigurations?> GetByDiscriminatorId(string discriminator)
        {
            return await _cosmosDBRepository.GetItemAsync(ContainerNames.ClientConfigurations,
                PartitionKeys.ClientConfigurations, label => label.Id == discriminator);
        }

        public async Task<IEnumerable<LabelConfigurations?>> GetAllByClientnId(string codclient)
        {
            return await _cosmosDBRepository.AllFilteredAsync(ContainerNames.ClientConfigurations,
               PartitionKeys.ClientConfigurations, conf => conf.Codclient == codclient);
        }
    }
}
