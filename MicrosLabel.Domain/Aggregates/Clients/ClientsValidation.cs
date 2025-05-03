using MicrosLabel.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Domain.Aggregates.Clients
{
    public class ClientsValidation
    {
        public static void NotNull(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(Resources.Null);
            }
        }
    }
}
