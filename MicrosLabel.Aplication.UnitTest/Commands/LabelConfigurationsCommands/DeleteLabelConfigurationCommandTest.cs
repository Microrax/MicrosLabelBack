using FluentAssertions;
using MicrosLabel.Application.Commands.LabelConfiguration;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;
using MicrosLabel.Reads.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Aplication.UnitTest.Commands.LabelConfigurationsCommands
{
    public class DeleteLabelConfigurationCommandTest
    {
        [Fact]
        private static async Task Handle_Works()
        {

            //ARRANGE
            string nombre = "hola";
            var config = new LabelConfigurations("001", nombre);
            var configuration = new LabelConfigurationsViewModel("id", "codclient", "discriminador");
            var configRepository = new Mock<ILabelConfigurationRepository>();
            var configQueryRepository = new Mock<ILabelConfigurationQueryRepository>();

            configQueryRepository.Setup(x => x.GetByBothId(config.Codclient, config.Discriminator))
                .Returns(Task.FromResult<LabelConfigurationsViewModel?>(configuration));

            var command = new DeleteLabelConfigurationCommand(config.Codclient, config.Discriminator);
            var sut = new DeleteLabelConfigurationCommandHandler(configRepository.Object, configQueryRepository.Object);

            //ACT
            await sut.HandleAsync(command);

            //ASSERT
        }

        [Fact]
        private static async Task Handle_Throws_Not_Exist_Config()
        {

            string nombre = "hola";
            var config = new LabelConfigurations("001", nombre);
            var configuration = new LabelConfigurationsViewModel("id", "codclient", "discriminador");
            var configRepository = new Mock<ILabelConfigurationRepository>();
            var configQueryRepository = new Mock<ILabelConfigurationQueryRepository>();

            configQueryRepository.Setup(x => x.GetByBothId(config.Codclient, config.Discriminator))
                .Returns(Task.FromResult<LabelConfigurationsViewModel?>(null));

            var command = new DeleteLabelConfigurationCommand(config.Codclient, config.Discriminator);
            var sut = new DeleteLabelConfigurationCommandHandler(configRepository.Object, configQueryRepository.Object);

            //ACT
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
