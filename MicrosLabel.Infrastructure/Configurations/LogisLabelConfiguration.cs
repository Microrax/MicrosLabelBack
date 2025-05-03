namespace MicrosLabel.Infrastructure.Configurations
{
    public sealed class MicrosLabelConfiguration
    {
        public string ApplicationRegion { get; set; }

        public string LogisCoreConnectionString { get; set; }

        public AzureCosmosDbConfiguration AzureCosmosDB { get; set; }
    }
}
