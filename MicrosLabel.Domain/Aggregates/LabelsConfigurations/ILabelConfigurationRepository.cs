using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;

namespace MicrosLabel.Domain.Aggregates.ClientConfigurations
{
    public interface ILabelConfigurationRepository
    {
        Task<LabelConfigurations> CreateAsync(LabelConfigurations labelConfig);

        Task UpdateAsync(LabelConfigurations labelConfig);

        Task DeleteAsync(LabelConfigurations labelConfig);

        //GETS
        Task<IEnumerable<LabelConfigurations?>> GetAllByClientnId(string Codclient);
        Task<LabelConfigurations?> GetByBothId(string Codclient, string Discriminator);
        Task<LabelConfigurations?> GetByClientId(string Codclient);
        Task<LabelConfigurations?> GetByDiscriminatorId(string Discriminator);
    }
}