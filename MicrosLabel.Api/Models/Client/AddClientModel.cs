using MicrosLabel.Application.Commands.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Api.Models.Client
{
    public class AddClientModel
    {
        public AddClientModel(string clientcode ,string nombre)
        {
            ClientCode = clientcode;
            Nombre = nombre;
        }

        [Required]
        [StringLength(100, ErrorMessage = "TemplateMaxLenght")]
        public string ClientCode {  get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "TemplateMaxLenght")]
        public string Nombre { get; set; }
    }
}
