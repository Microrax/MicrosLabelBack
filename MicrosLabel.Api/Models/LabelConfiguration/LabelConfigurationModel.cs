using MicrosLabel.Application.Commands.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Api.Models.LabelConfiguration
{
    public class LabelConfigurationModel
    {
        public LabelConfigurationModel(string codclient, string discriminator/*, string priority*/)
        {
            Codclient = codclient;
            Discriminator = discriminator;
            //Priority = priority;
        }

        [Required]
        public string Codclient { get; set; }

        [Required]
        public string Discriminator { get; set; }
        //public string Priority { get; set; }
    }
}
