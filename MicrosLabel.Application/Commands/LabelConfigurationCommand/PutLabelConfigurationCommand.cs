using MicrosLabel.Application.Commands.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Application.Commands.LabelConfiguration
{
    public class PutLabelConfigurationCommand : ICommand
    {
        public PutLabelConfigurationCommand(string codclient, string discriminator/*, string priority*/)
        {
            Codclient = codclient;
            Discriminator = discriminator;
            //Priority = priority;
        }

        public string Codclient { get; set; }

        public string Discriminator { get; set; }
        //public string Priority { get; set; }
    }
}

