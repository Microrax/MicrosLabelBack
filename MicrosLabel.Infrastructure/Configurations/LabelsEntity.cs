using MicrosLabel.Infrastructure.CosmosDb;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using static MicrosLabel.Infrastructure.CosmosDb.CosmosContainer;

namespace MicrosLabel.Infrastructure.Configurations
{
    public class LabelsEntity : BaseEntity
    {
        [JsonIgnore]
        public override string ContainerName => ContainerNames.Labels;

        [JsonIgnore]
        public override string PartitionKeyName => PartitionKeys.Labels;

        [JsonIgnore]
        public override PartitionKey PartitionKey => new(Id);

        public string LabelType { get; set; }

        public string Template { get; set; }

        public string Zpl { get; set; }

        public int Dpi { get; set; }

        public string Sql { get; set; }
    }
}
