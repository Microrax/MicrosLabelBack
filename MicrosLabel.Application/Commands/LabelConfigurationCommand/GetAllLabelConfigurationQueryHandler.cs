using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;
using MicrosLabel.Reads.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.LabelConfigurationCommand
{
    public class GetAllLabelConfigurationQueryHandler : IQueryHandler<GetAllLabelConfigurationQuery, IEnumerable<LabelConfigurationsViewModel>>
    {
        private readonly ILabelConfigurationQueryRepository _labelsConfigurationQueryRepository;

        public GetAllLabelConfigurationQueryHandler(ILabelConfigurationQueryRepository labelsConfigurationQueryRepository)
        {
            _labelsConfigurationQueryRepository = labelsConfigurationQueryRepository;
        }
        public Task<IEnumerable<LabelConfigurationsViewModel>> HandleAsync(GetAllLabelConfigurationQuery query)
        {
            return _labelsConfigurationQueryRepository.GetAllByClientnId(query.Codclient);
        }
    }
}
