using MicrosLabel.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Domain.Aggregates.Prints
{
    public class PrintsValidations
    {
        public static void notnull(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentNullException(Resources.Null);
            }
        }
    }
}
