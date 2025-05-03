using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Infrastructure.CosmosDb.Extensions;
using MicrosLabel.Reads.Queries;
using MicrosLabel.Reads.ViewModels;
using Microsoft.Azure.Cosmos;
using static MicrosLabel.Infrastructure.CosmosDb.CosmosContainer;

namespace MicrosLabel.Infrastructure.Repositories.QueryRepositories
{
    public sealed class LabelQueryRepository : ILabelQueryRepository
    {
        private readonly Database _cosmosDb;

        public LabelQueryRepository(Database cosmosDB)
        {
            _cosmosDb = cosmosDB;
        }

        public async Task<IEnumerable<LabelViewModel>> GetAllAsync()
        {
            string sql = @"SELECT * FROM labels l";

            var query = new QueryDefinition(sql);

            var configurationContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.Labels, $"/{PartitionKeys.Labels}");
            return await configurationContainer.Container.QueryAsync<LabelViewModel>(query);
        }

        public async Task<LabelsModel> GetByIdAsync(string id)
        {
            string sql = @"SELECT * FROM labels l WHERE l.id = '"+ id +"'";

            var query = new QueryDefinition(sql);

            var configurationContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.Labels, $"/{PartitionKeys.Labels}");
            return await configurationContainer.Container.QuerySingleOrDefaultAsync<LabelsModel>(query);
        }

        public async Task<LabelViewModel> ExistLabel(int labeltype, string template, int dpi)   //CUIDADO EN COSMOSDB LABEL TEMPLATE NO SE GUARDA COMO INT
        {
            string sql = @"SELECT * FROM labels l WHERE l.LabelType = '" + labeltype + "'" + " AND " + "l.Template = '"+ template +"'" + " AND " + "l.Dpi = " + dpi;

            var query = new QueryDefinition(sql);

            var configurationContainer = await _cosmosDb.CreateContainerIfNotExistsAsync(ContainerNames.Labels, $"/{PartitionKeys.Labels}");
            return await configurationContainer.Container.QuerySingleOrDefaultAsync<LabelViewModel>(query);
        }
    }
}
