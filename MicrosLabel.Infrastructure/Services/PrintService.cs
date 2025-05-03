using MicrosLabel.Application.Enumerations;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.Services;
using MicrosLabel.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Infrastructure.Services
{
    public sealed class PrintService : IPrintService
    {
        private readonly string _blobContainerName;
        private readonly AzureBlobContainerManager _blobContainerManager;
        private readonly AzureServiceBusManager _serviceBusManager;

        public PrintService(
            IOptions<AzureBlobContainerConfiguration> blobContainerConfiguration,
            AzureBlobContainerManager blobContainerManager,
            AzureServiceBusManager serviceBusManager)
        {
            _blobContainerName = blobContainerConfiguration.Value.ContainerName.Printing;
            _blobContainerManager = blobContainerManager;
            _serviceBusManager = serviceBusManager;
        }

        public async Task PrintLabelAsync(string workstationCode, Label label)
        {
            var fileName = BuildFileName(workstationCode, label.DocumentSize);
            await _blobContainerManager.UploadAsync(_blobContainerName, fileName, label.Zpl);

            var message = new PrintingMessage
            {
                DocumentType = label.DocumentSize.ToString(),
                DocumentFormat = label.DocumentFormat.ToString(),
                FileName = fileName
            };

            await _serviceBusManager.SendMessageAsync(workstationCode, message);
        }

        private static string BuildFileName(string workstationCode, DocumentSize documentSize)
        {
            var date = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return $"{workstationCode}-{documentSize:G}-{date}.txt";
        }
    }
}
