using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Properties;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.LabelsCommands
{
    public class PutLabelCommandHandler : ICommandHandler<PutLabelCommand>
    {

        private readonly ILabelsModelRepository _labelsModelRepository;
        private readonly ILabelQueryRepository _labelsQueryRepository;

        public PutLabelCommandHandler(ILabelsModelRepository labelsModelRepository,
            ILabelQueryRepository labelsQueryRepository)
        {
            _labelsModelRepository = labelsModelRepository;
            _labelsQueryRepository = labelsQueryRepository;
        }
        public async Task HandleAsync(PutLabelCommand command)
        {

            var label = await _labelsQueryRepository.GetByIdAsync(command.id);
            if(label == null)
            {
                throw new InvalidOperationException(Resources.ExistLabelPut);
            }
            
            LabelsModel lbmodel = new LabelsModel(
             id: command.id,
             labelType: command.LabelType,
             template: command.Template,
             zpl: command.Zpl,
             dpi: command.Dpi,
             sql: command.Sql
             );


            await _labelsModelRepository.UpdateAsync(lbmodel);
        }
    }
}
