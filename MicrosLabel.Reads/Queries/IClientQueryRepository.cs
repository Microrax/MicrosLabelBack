using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Infrastructure.Reads.Queries;
using MicrosLabel.Reads.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Reads.Queries
{
    public interface IClientQueryRepository : IQuery
    {
        Task<ClientsModel?> GetByIdAsync(string id);

        Task<IEnumerable<ClientsViewModel>>? GetAllAsync();


        Task<ClientsModel?> GetByNombreAsync(string nombre);

        Task<ClientsModel?> GetByClientCodeAsync(string clientCode);
    }
}
