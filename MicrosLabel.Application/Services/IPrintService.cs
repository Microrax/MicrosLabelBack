using MicrosLabel.Domain.Aggregates.Labels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Services
{
    public interface IPrintService
    {
        Task PrintLabelAsync(string workstationCode, LabelsModel label);

    }
}
