using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.LabelsCommand
{
    public class GetNoConfigLabelQuery : IQuery<IEnumerable<LabelViewModel>>
    {
        public GetNoConfigLabelQuery(string clientId) {

            LabelsValidation.ValidateNullStrings(clientId);

            ClientId = clientId;
        }

        public string ClientId { get; set; }
    }
}
