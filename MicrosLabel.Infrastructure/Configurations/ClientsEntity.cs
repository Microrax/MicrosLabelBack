using MicrosLabel.Infrastructure.CosmosDb;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MicrosLabel.Infrastructure.CosmosDb.CosmosContainer;

namespace MicrosLabel.Infrastructure.Configurations
{
    public class ClientsEntity : BaseEntity
    {
        [JsonIgnore]
        public override string ContainerName => ContainerNames.Clients;

        [JsonIgnore]
        public override string PartitionKeyName => PartitionKeys.Clients;

        [JsonIgnore]
        public override PartitionKey PartitionKey => new(Id);

        public string ClientCode { get; set; }

        public string Nombre { get; set; }
    }
}
