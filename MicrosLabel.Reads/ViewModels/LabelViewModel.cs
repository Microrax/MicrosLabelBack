using MicrosLabel.Domain.Aggregates.Labels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Reads.ViewModels
{
    public class LabelViewModel
    {
        public LabelViewModel(string labelType, string template, string zpl, int dpi, string sql)
        {
         
            LabelType = labelType;
            Template = template;
            Zpl = zpl;
            Dpi = dpi;
            Sql = sql;
        }
        public string Id { get; set; }

        public string LabelType { get; set; }

        public string Template { get; set; }

        public string Zpl { get; set; }

        public int Dpi { get; set; }

        public string Sql { get; set; }
    }
}
