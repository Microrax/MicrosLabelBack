using MicrosLabel.Application.Commands.Configuration;
using System.ComponentModel.DataAnnotations;

namespace MicrosLabel.Application.Commands.LabelsCommands
{
    public class DeleteLabelCommand : ICommand
    {

        public DeleteLabelCommand(string Id)
        {
            id = Id;
        }

        public string id { get; set; }
    }
}
