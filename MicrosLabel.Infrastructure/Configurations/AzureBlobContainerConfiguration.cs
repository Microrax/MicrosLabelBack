namespace MicrosLabel.Infrastructure.Configurations
{
    public class AzureBlobContainerConfiguration
    {
        public string ConnectionString { get; set; }

        public ContainerNames ContainerName { get; set; }

        public class ContainerNames
        {
            public string Printing { get; set; }
        }
    }
}
