using MicrosLabel.Application.Commands.Configuration;

namespace MicrosLabel.Api.Models.Label
{
    public class PrintLabelModel : ICommand
    {

        public string workstationCode { get; set; }

        public string itemId { get; set; }

        public string labelId { get; set; }

        public string clientCode { get; set; }
    }
}
