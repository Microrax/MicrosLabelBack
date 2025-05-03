using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.ClientCommand
{
    public class GetYesConfigLabelClientsQuery : IQuery<IEnumerable<ClientsViewModel>>
    {
        public GetYesConfigLabelClientsQuery(string labelId)
        {

            LabelsValidation.ValidateNullStrings(labelId);

            LabelId = labelId;
        }

        public string LabelId { get; set; }
    }
}
