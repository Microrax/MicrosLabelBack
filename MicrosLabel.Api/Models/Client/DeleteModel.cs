using MicrosLabel.Application.Commands.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Api.Models.Client
{
    public class DeleteModel
    {

        public DeleteModel(string id)
        {
            Id = id;
        }

        [Required]
        public string Id { get; set; }
    }
}
