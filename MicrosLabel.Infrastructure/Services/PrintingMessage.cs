namespace MicrosLabel.Infrastructure.Services
{
    public class PrintingMessage: AzureServiceBusMessageBase
    {
        public PrintingMessage() : base(typeof(PrintingMessage))
        {
        }

        public string DocumentType { get; set; }

        public string DocumentFormat { get; set; }

        public string FileName { get; set; }
    }
}
