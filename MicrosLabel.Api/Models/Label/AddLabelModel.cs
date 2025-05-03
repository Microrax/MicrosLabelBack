using MicrosLabel.Application.Commands.Configuration;
using System.ComponentModel.DataAnnotations;

namespace MicrosLabel.Api.Models.Label
{
    public class AddLabelModel
    {
        
        public AddLabelModel(int labelType, string template, string zpl, int dpi, string sql)
        {
            LabelType = labelType;
            Template = template;
            Zpl = zpl;
            Dpi = dpi;
            Sql = sql;
        }

        [Required(ErrorMessage = "LabelTypeRequiered")]
        [Range(0, 4)]
        public int LabelType { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "TemplateMaxLenght")]
        public string Template { get; set; }

        [Required]
        public string Zpl { get; set; }

        [Required(ErrorMessage = "LabelTypeRequiered")]
        [Range(1, 3000)]
        public int Dpi { get; set; }

        [Required]
        public string Sql { get; set; }
    }
}
