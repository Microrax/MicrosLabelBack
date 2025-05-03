using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Infrastructure.Configurations;
using MicrosLabel.Infrastructure.CosmosDb;
using static MicrosLabel.Infrastructure.CosmosDb.CosmosContainer;

namespace MicrosLabel.Infrastructure.Repositories
{
    public class LabelRepository : ILabelsModelRepository
    {
        private readonly CosmosDbRepository<LabelsModel, LabelsEntity> _cosmosDBRepository;
        public LabelRepository(CosmosDbRepository<LabelsModel, LabelsEntity> cosmosDBRepository)
        {
            _cosmosDBRepository = cosmosDBRepository;
        }

        public LabelRepository()
        {
        }

        public async Task<LabelsModel> CreateAsync(LabelsModel label)
        {
            return await _cosmosDBRepository.AddItemAsync(label);
        }

        public async Task DeleteAsync(LabelsModel label)
        {
           await _cosmosDBRepository.DeleteItemAsync(label);
        }

        public async Task<LabelsModel> GetByIdAsync(string id)
        {
            return await _cosmosDBRepository.GetItemAsync(ContainerNames.Labels, PartitionKeys.Labels, l => l.Id == id);
        }

        public async Task UpdateAsync(LabelsModel label)
        {
            await _cosmosDBRepository.UpdateItemAsync(label);
        }

        public async Task<IEnumerable<LabelsModel>> GetAllAsync()
        {
            return await _cosmosDBRepository.GetAllItemsAsync(ContainerNames.Labels, PartitionKeys.Labels);
        }
    }
}
