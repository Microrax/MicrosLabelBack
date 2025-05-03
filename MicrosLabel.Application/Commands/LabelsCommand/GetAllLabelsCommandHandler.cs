using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Infrastructure.Repositories.QueryRepositories;
using MicrosLabel.Reads.Queries;
using MicrosLabel.Reads.ViewModels;

namespace MicrosLabel.Application.Commands.LabelsCommands
{
    public class GetAllLabelsCommandHandler : IQueryHandler<GetAllLabelsCommand, IEnumerable<LabelViewModel>>
    {
        private readonly ILabelQueryRepository _labelsQueryRepository;

        public GetAllLabelsCommandHandler(ILabelQueryRepository labelsQueryRepository)
        {
            _labelsQueryRepository = labelsQueryRepository;
        }

        public Task<IEnumerable<LabelViewModel>> HandleAsync(GetAllLabelsCommand command)
        {
            return _labelsQueryRepository.GetAllAsync();
        }

    }
}
