using MicrosLabel.Application.Commands.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.ClientCommand
{
    public class AddClientCommand : ICommand
    {
        public AddClientCommand(string clientCode, string nombre)
        {
            ClientCode = clientCode;
            Nombre = nombre;
        }

        public string ClientCode { get; set; }
        public string Nombre { get; set; }
    }
}
