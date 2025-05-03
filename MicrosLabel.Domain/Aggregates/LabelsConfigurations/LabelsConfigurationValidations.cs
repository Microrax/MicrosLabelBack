using MicrosLabel.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicrosLabel.Domain.Aggregates.LabelsConfigurations
{
    public class LabelsConfigurationValidations
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
