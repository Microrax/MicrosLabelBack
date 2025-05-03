using MicrosLabel.Application.Commands.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.ClientCommand
{
    public class DeleteClientCommand : ICommand
    {

        public DeleteClientCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
