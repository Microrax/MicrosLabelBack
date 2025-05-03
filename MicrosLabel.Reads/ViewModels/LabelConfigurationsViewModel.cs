using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Reads.ViewModels
{
    public class LabelConfigurationsViewModel
    {
        public LabelConfigurationsViewModel(string id, string codclient, string discriminator) 
        { 
            Id = id;   
            CodClient = codclient;  
            Discriminator = discriminator;
        }

        public string Id { get; set; }
        public string CodClient { get ; set; }
        public string Discriminator {  get; set;}
    }
}
