using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Prints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.PrintLabelsCommand
{
    public class PrintLabelCommand : ICommand
    {
        public PrintLabelCommand(string workstationCode, string itemId, string labelId, string clientCode)
        {
            LabelId = labelId;
            WorkstationCode = workstationCode;
            ItemId = itemId;
            ClientCode = clientCode;
        }

        public string WorkstationCode { get; }

        public string ItemId { get; }

        public string LabelId { get; }

        public string ClientCode { get; }

    }
}
