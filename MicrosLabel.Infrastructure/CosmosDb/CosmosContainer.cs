namespace MicrosLabel.Infrastructure.CosmosDb;

public static class CosmosContainer
{
    public static class ContainerNames
    {
        public const string ClientConfigurations = "ClientsConfigurations";
        public const string Labels = "labels";
        public const string Clients = "clients";
    }

    public static class PartitionKeys
    {
        public const string ClientConfigurations = "id";
        public const string Labels = "id";
        public const string Clients = "id";
    }
}