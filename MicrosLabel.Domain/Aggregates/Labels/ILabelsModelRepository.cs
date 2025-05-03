using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicrosLabel.Domain.Aggregates.Labels
{
    public interface ILabelsModelRepository
    {
        Task<LabelsModel> CreateAsync(LabelsModel label);
        Task<LabelsModel?> GetByIdAsync(string id);
        Task UpdateAsync(LabelsModel label);
        Task DeleteAsync(LabelsModel label);
        Task<IEnumerable<LabelsModel>> GetAllAsync();
    }
}
