using MicrosLabel.Application.Commands.Configuration;
using System.ComponentModel.DataAnnotations;

namespace MicrosLabel.Application.Commands.LabelsCommands
{
    public class AddLabelCommand : ICommand
    {
        
        public AddLabelCommand(int labelType, string template, string zpl, int dpi, string sql)
        {
            LabelType = labelType;
            Template = template;
            Zpl = zpl;
            Dpi = dpi;
            Sql = sql;
        }

        public int LabelType { get; set; }

        public string Template { get; set; }

        public string Zpl { get; set; }

        public int Dpi { get; set; }

        public string Sql { get; set; }
    }
}
