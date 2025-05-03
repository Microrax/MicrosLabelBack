using FluentAssertions;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Domain.UnitTest.Aggregates
{
    public class LabelConfigurationTest
    {
        private readonly LabelConfigurations _config;

        public LabelConfigurationTest()
        {
            _config = new LabelConfigurations();
        }

        //PERFECT CASE
        [Fact]
        public void Can_Create_Client_Perfect_Case_Both_Attributes_Constructor()
        {
            var configuration = new LabelConfigurations("ASDF", "ASDF");
            configuration.Codclient.Should().Be("ASDF");
            configuration.Discriminator.Should().Be("ASDF");
        }
        //WRONG CASES
        [Fact]
        public void Cant_Create_Configuration_Null_ClientCode_Both_Attributes_Constructor()
        {
            Action action = () => new ClientsModel(null, "ASDF");
            action.Should().Throw<System.ArgumentNullException>();
        }
        
        [Fact]
        public void Cant_Create_Configuration_Null_Discriminator_Both_Attributes_Constructor()
        {
            Action action = () => new ClientsModel("ASDF", null);
            action.Should().Throw<System.ArgumentNullException>();
        }
    }
}
