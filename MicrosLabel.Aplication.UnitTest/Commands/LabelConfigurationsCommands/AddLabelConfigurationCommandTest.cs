using FluentAssertions;
using MicrosLabel.Application.Commands.ClientCommand;
using MicrosLabel.Application.Commands.LabelConfiguration;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.Aggregates.LabelsConfigurations;
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
    public class AddLabelConfigurationCommandTest
    {
        [Fact]
        private static async Task Handle_Works()
        {

            //ARRANGE
            string nombre = "hola";
            var config = new LabelConfigurations("001", nombre);
            var client = new ClientsModel("001", nombre);
            var label = new LabelsModel(1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var labelConfigurationRepository = new Mock<ILabelConfigurationRepository>();
            var labelConfigurationQueryRepository = new Mock<ILabelConfigurationQueryRepository>();
            var labelQueryRepository = new Mock<ILabelQueryRepository>();
            var clientQueryRepository = new Mock<IClientQueryRepository>();


            labelQueryRepository.Setup(x => x.GetByIdAsync(config.Discriminator))
                .Returns(Task.FromResult<LabelsModel?>(label));
            clientQueryRepository.Setup(x => x.GetByIdAsync(config.Codclient))
                .Returns(Task.FromResult<ClientsModel?>(client));
            labelConfigurationQueryRepository.Setup(x => x.GetByBothId(config.Codclient, config.Discriminator))
                .Returns(Task.FromResult<LabelConfigurationsViewModel?>(null));


            var command = new AddLabelConfigurationCommand(config.Codclient,config.Discriminator);
            var sut = new AddLabelConfigurationCommandHandler(labelConfigurationRepository.Object, labelConfigurationQueryRepository.Object,
                labelQueryRepository.Object, clientQueryRepository.Object);

            //ACT
            await sut.HandleAsync(command);

            //ASSERT
        }

        [Fact]
        private static async Task Handle_Throws_Not_Exist_Label()
        {

            //ARRANGE
            string nombre = "hola";
            var config = new LabelConfigurations("001", nombre);
            var client = new ClientsModel("001", nombre);
            var label = new LabelsModel(1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var labelConfigurationRepository = new Mock<ILabelConfigurationRepository>();
            var labelConfigurationQueryRepository = new Mock<ILabelConfigurationQueryRepository>();
            var labelQueryRepository = new Mock<ILabelQueryRepository>();
            var clientQueryRepository = new Mock<IClientQueryRepository>();


            labelQueryRepository.Setup(x => x.GetByIdAsync(config.Discriminator))
                .Returns(Task.FromResult<LabelsModel?>(null));
            clientQueryRepository.Setup(x => x.GetByIdAsync(config.Codclient))
                .Returns(Task.FromResult<ClientsModel?>(client));
            labelConfigurationQueryRepository.Setup(x => x.GetByBothId(config.Codclient, config.Discriminator))
                .Returns(Task.FromResult<LabelConfigurationsViewModel?>(null));


            var command = new AddLabelConfigurationCommand(config.Codclient, config.Discriminator);
            var sut = new AddLabelConfigurationCommandHandler(labelConfigurationRepository.Object, labelConfigurationQueryRepository.Object,
                labelQueryRepository.Object, clientQueryRepository.Object);

            //ACT
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        private static async Task Handle_Throws_Not_Exist_Client()
        {

            //ARRANGE
            string nombre = "hola";
            var config = new LabelConfigurations("001", nombre);
            var client = new ClientsModel("001", nombre);
            var label = new LabelsModel(1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var labelConfigurationRepository = new Mock<ILabelConfigurationRepository>();
            var labelConfigurationQueryRepository = new Mock<ILabelConfigurationQueryRepository>();
            var labelQueryRepository = new Mock<ILabelQueryRepository>();
            var clientQueryRepository = new Mock<IClientQueryRepository>();


            labelQueryRepository.Setup(x => x.GetByIdAsync(config.Discriminator))
                .Returns(Task.FromResult<LabelsModel?>(label));
            clientQueryRepository.Setup(x => x.GetByIdAsync(config.Codclient))
                .Returns(Task.FromResult<ClientsModel?>(null));
            labelConfigurationQueryRepository.Setup(x => x.GetByBothId(config.Codclient, config.Discriminator))
                .Returns(Task.FromResult<LabelConfigurationsViewModel?>(null));


            var command = new AddLabelConfigurationCommand(config.Codclient, config.Discriminator);
            var sut = new AddLabelConfigurationCommandHandler(labelConfigurationRepository.Object, labelConfigurationQueryRepository.Object,
                labelQueryRepository.Object, clientQueryRepository.Object);

            //ACT
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        private static async Task Handle_Throws_Not_Exist_Label_Configuration()
        {

            //ARRANGE
            string nombre = "hola";
            var config = new LabelConfigurations("001", nombre);
            var configuration = new LabelConfigurationsViewModel("id", "codclient", "discriminador");
            var client = new ClientsModel("001", nombre);
            var label = new LabelsModel(1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var labelConfigurationRepository = new Mock<ILabelConfigurationRepository>();
            var labelConfigurationQueryRepository = new Mock<ILabelConfigurationQueryRepository>();
            var labelQueryRepository = new Mock<ILabelQueryRepository>();
            var clientQueryRepository = new Mock<IClientQueryRepository>();


            labelQueryRepository.Setup(x => x.GetByIdAsync(config.Discriminator))
                .Returns(Task.FromResult<LabelsModel?>(label));
            clientQueryRepository.Setup(x => x.GetByIdAsync(config.Codclient))
                .Returns(Task.FromResult<ClientsModel?>(client));
            labelConfigurationQueryRepository.Setup(x => x.GetByBothId(config.Codclient, config.Discriminator))
                .Returns(Task.FromResult<LabelConfigurationsViewModel?>(configuration));


            var command = new AddLabelConfigurationCommand(config.Codclient, config.Discriminator);
            var sut = new AddLabelConfigurationCommandHandler(labelConfigurationRepository.Object, labelConfigurationQueryRepository.Object,
                labelQueryRepository.Object, clientQueryRepository.Object);

            //ACT
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
