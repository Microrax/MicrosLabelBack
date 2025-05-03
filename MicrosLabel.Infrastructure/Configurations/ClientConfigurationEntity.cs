using MicrosLabel.Infrastructure.CosmosDb;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using static MicrosLabel.Infrastructure.CosmosDb.CosmosContainer;

namespace MicrosLabel.Infrastructure.Configurations
{
    public sealed class ClientConfigurationEntity : BaseEntity
    {
        [JsonIgnore]
        public override string ContainerName => ContainerNames.ClientConfigurations;

        [JsonIgnore]
        public override string PartitionKeyName => PartitionKeys.ClientConfigurations;

        [JsonIgnore]
        public override PartitionKey PartitionKey => new(Id);

    }
}
