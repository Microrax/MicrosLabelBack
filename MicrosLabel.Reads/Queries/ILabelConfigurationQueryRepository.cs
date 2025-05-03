using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Infrastructure.Reads.Queries;
using MicrosLabel.Reads.ViewModels;

namespace MicrosLabel.Reads.Queries
{
    public interface ILabelConfigurationQueryRepository : IQuery
    {
        Task<LabelConfigurationsViewModel?> GetByBothId(string codclient, string discriminator);

        Task<LabelConfigurations?> GetByClientId(string codclient);

        Task<LabelConfigurations?> GetByDiscriminatorId(string discriminator);

        Task<IEnumerable<LabelConfigurationsViewModel?>> GetAllByClientnId(string codclient);

        Task<IEnumerable<LabelConfigurationsViewModel?>> GetAllByLabelId(string labelId);
    }
}
