using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Domain.Aggregates.Prints
{
    public class LabelPrintModel
    {
        public LabelPrintModel(string id) { 

            PrintsValidations.notnull(id);

            Id = id;
        }

        public string Id { get; set; }
    }
}
