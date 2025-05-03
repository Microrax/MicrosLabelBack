using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.Aggregates.Prints;
using MicrosLabel.Infrastructure.Reads.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Reads.Queries
{
    public interface ILabelPrintQueryRepository : IQuery
    {
        Task<string?> GetByIdAsync(string clientCode, string sku, string zpl, string sql);

    }
}
