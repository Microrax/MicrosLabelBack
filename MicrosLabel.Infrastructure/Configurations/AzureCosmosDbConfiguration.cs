namespace MicrosLabel.Infrastructure.Configurations
{
    public sealed class AzureCosmosDbConfiguration
    {
        public string DatabaseName { get; set; }

        public string ConnectionString { get; set; }
    }
}
