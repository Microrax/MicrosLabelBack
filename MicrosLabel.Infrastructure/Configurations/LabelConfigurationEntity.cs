using MicrosLabel.Infrastructure.CosmosDb;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MicrosLabel.Infrastructure.CosmosDb.CosmosContainer;

namespace MicrosLabel.Infrastructure.Configurations
{
    public class LabelConfigurationEntity : BaseEntity
    {
        public override string ContainerName => ContainerNames.ClientConfigurations;

        public override string PartitionKeyName => PartitionKeys.ClientConfigurations;

        public override PartitionKey PartitionKey => new(Id);

        public string Codclient { get; set; }

        public string Discriminator { get; set; }

    }
}
