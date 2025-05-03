using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Reads.Queries;
using MicrosLabel.Reads.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace MicrosLabel.Application.Commands.LabelsCommand
{
    public class GetNoConfigLabelQueryHandler : IQueryHandler<GetNoConfigLabelQuery, IEnumerable<LabelViewModel>>
    {
        private readonly ILabelQueryRepository _labelsQueryRepository;
        private readonly ILabelConfigurationQueryRepository _configQueryRepository;

        public GetNoConfigLabelQueryHandler(ILabelQueryRepository labelsQueryRepository,
            ILabelConfigurationQueryRepository configsQueryRepository) {

            _labelsQueryRepository = labelsQueryRepository;
            _configQueryRepository = configsQueryRepository;
        }

        public async Task<IEnumerable<LabelViewModel>> HandleAsync(GetNoConfigLabelQuery query)
        {
            //LISTAS
            IEnumerable<LabelViewModel?> labels = await _labelsQueryRepository.GetAllAsync();  //ALL LABELS
            IEnumerable<LabelConfigurationsViewModel?> labelsconfigurados = await _configQueryRepository.GetAllByClientnId(query.ClientId); // ALL CONFIGURATIONS OBJECT WITH LABELS ID

            //RETURN
            IEnumerable<LabelViewModel> noConfigurationLabel;
            List<LabelViewModel> configruadas = new List<LabelViewModel>();

            if (labelsconfigurados.IsNullOrEmpty())
            {
                return labels;
            }

            if (labels.Count() == labelsconfigurados.Count())
            {
                return null;
            }
            foreach (var label in labels)
            {
                if (!labelsconfigurados.Any(config => config.Discriminator == label.Id))
                {
                    configruadas.Add(label);
                }
            }
            
            noConfigurationLabel = configruadas;

            return noConfigurationLabel;
        }
    }
}
