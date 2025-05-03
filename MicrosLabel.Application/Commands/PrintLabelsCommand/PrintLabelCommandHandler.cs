using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using MicrosLabel.Application.Commands.LabelConfigurationCommand;
using MicrosLabel.Application.Enumerations;
using MicrosLabel.Application.Properties;
using MicrosLabel.Application.Services;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.Aggregates.Prints;
using MicrosLabel.Domain.Services;
using MicrosLabel.Infrastructure.Repositories.QueryRepositories;
using MicrosLabel.Reads.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPrintService = MicrosLabel.Domain.Services.IPrintService;

namespace MicrosLabel.Application.Commands.PrintLabelsCommand
{
    public class PrintLabelCommandHandler : ICommandHandler<PrintLabelCommand>
    {
        ILabelPrintQueryRepository _labelPrintQueryRepository;
        ILabelQueryRepository _labelQueryRepository;
        ILabelConfigurationQueryRepository _labelConfigQueryRepository;
        IPrintService _printService;

        public PrintLabelCommandHandler(ILabelPrintQueryRepository labelPrintQueryRepository,
            ILabelQueryRepository labelQueryRepository,
            ILabelConfigurationQueryRepository labelConfigQueryRepository,
            IPrintService printService)
        {
            _labelPrintQueryRepository = labelPrintQueryRepository;
            _labelQueryRepository = labelQueryRepository;
            _labelConfigQueryRepository = labelConfigQueryRepository;
            _printService = printService;
            _printService = printService;
        }


        async Task ICommandHandler<PrintLabelCommand>.HandleAsync(PrintLabelCommand command)
        {
            var lbmodel = await _labelQueryRepository.GetByIdAsync(command.LabelId);
            if (lbmodel == null)
            {
                throw new InvalidOperationException(Resources.ExistLabel);
            }
            var config = await _labelConfigQueryRepository.GetByBothId(command.ClientCode, command.LabelId);
            if (config == null)
            {
                throw new InvalidOperationException(Resources.ExistLabel);
            }
            string zpl = await _labelPrintQueryRepository.GetByIdAsync(command.ClientCode, command.ItemId, lbmodel.Zpl, lbmodel.Sql);
            var label = new Label(zpl, DocumentSize.BigLabel, DocumentFormat.Zpl);

            await _printService.PrintLabelAsync(command.WorkstationCode, label);
        }
    }
}
