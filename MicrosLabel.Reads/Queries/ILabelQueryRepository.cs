using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Infrastructure.Reads.Queries;
using MicrosLabel.Reads.ViewModels;

namespace MicrosLabel.Reads.Queries
{
    public interface ILabelQueryRepository : IQuery
    {
        Task<LabelsModel?> GetByIdAsync(string id);

        Task<IEnumerable<LabelViewModel>> GetAllAsync();

        Task<LabelViewModel> ExistLabel(int labeltype, string template, int dpi);
    }
}
