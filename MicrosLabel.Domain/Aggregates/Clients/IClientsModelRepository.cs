using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicrosLabel.Domain.Aggregates.Clients
{
    public interface IClientsModelRepository
    {
        Task<ClientsModel> CreateAsync(ClientsModel client);
        Task<ClientsModel?> GetByIdAsync(string id);
        Task DeleteAsync(ClientsModel client);
        Task<IEnumerable<ClientsModel>>? GetAllAsync();
        Task<ClientsModel?> GetByNombreAsync(string nombre);
        Task<ClientsModel?> GetByClientCodeAsync(string clientCode);
    }
}
