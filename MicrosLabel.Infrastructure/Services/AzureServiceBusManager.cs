using Azure.Messaging.ServiceBus;
using MicrosLabel.Infrastructure.Configurations;
using System.Text.Json;

namespace MicrosLabel.Infrastructure.Services
{
    public class AzureServiceBusManager
    {
        private readonly ServiceBusClient _client;

        public AzureServiceBusManager(AzureServiceBusConfiguration configuration)
        {
            _client = new ServiceBusClient(configuration.ConnectionString);
        }

        public async Task SendMessageAsync<TMessage>(string queueName, TMessage message) where TMessage : AzureServiceBusMessageBase
        {
            var serializedMessage = JsonSerializer.Serialize(message);
            var serviceBusMessage = new ServiceBusMessage(serializedMessage);

            var sender = _client.CreateSender(queueName);
            await sender.SendMessageAsync(serviceBusMessage);
        }
    }
}
