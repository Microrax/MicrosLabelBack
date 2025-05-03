using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Reads.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.LabelConfigurationCommand
{
    public class GetAllLabelConfigurationQuery : IQuery<IEnumerable<LabelConfigurationsViewModel>>
    {
        public GetAllLabelConfigurationQuery(string codclient)
        {
            this.Codclient = codclient; 
        }

        public string Codclient { get; set; }
    }
}
