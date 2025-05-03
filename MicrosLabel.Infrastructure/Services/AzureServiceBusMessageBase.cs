using System.Text.Json.Serialization;

namespace MicrosLabel.Infrastructure.Services
{
    public abstract class AzureServiceBusMessageBase
    {
        protected AzureServiceBusMessageBase(Type queueServiceType)
        {
            QueueServiceType = queueServiceType;
        }

        public string ClientCode { get; set; }

        public string ClientName { get; set; }

        [JsonIgnore]
        public string SessionId { get; set; }

        [JsonIgnore]
        public Type QueueServiceType { get; }
    }
}
