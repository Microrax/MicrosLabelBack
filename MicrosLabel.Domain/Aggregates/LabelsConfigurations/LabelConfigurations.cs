using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.LabelsConfigurations;
using System.ComponentModel.DataAnnotations;

namespace MicrosLabel.Domain.Aggregates.ClientConfigurations
{
    public class LabelConfigurations : Aggregate
    {
        public LabelConfigurations(string codclient, string discriminator/*, string priority*/)
        {
            LabelsConfigurationValidations.notnull(codclient);
            LabelsConfigurationValidations.notnull(discriminator);

            Codclient = codclient;
            Discriminator = discriminator;
            //Priority = priority;
        }

        public LabelConfigurations(string id, string codclient, string discriminator/*, string priority*/)
        {
            Id = id;
            Codclient = codclient;
            Discriminator = discriminator;
            //Priority = priority;
        }

        public LabelConfigurations()
        {

        }

        public string Codclient { get; set; }

        public string Discriminator { get; set; }

        /*
        [Required]
        [StringLength(100, ErrorMessage = "TemplateMaxLenght")]
        public string Priority { get; set; }
        */
    }
}