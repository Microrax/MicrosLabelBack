using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace MicrosLabel.Infrastructure.CosmosDb;

public abstract class BaseEntity
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonIgnore]
    public abstract string ContainerName { get; }

    [JsonIgnore]
    public abstract string PartitionKeyName { get; }

    [JsonIgnore]
    public abstract PartitionKey PartitionKey { get; }
}
