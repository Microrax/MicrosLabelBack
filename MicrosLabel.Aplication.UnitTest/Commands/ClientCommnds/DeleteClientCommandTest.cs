using Azure.Core.Extensions;
using FluentAssertions;
using MicrosLabel.Application.Commands.ClientCommand;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.Aggregates.LabelsConfigurations;
using MicrosLabel.Infrastructure.Repositories.QueryRepositories;
using MicrosLabel.Reads.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicrosLabel.Aplication.UnitTest.Commands.ClientCommnds
{
    public class DeleteClientCommandTest
    {
        [Fact]
        private static async Task Handle_Works()
        {
            //ARRANGE
            string nombre = "hola";
            var client = new ClientsModel("asdfasf", nombre, nombre);
            var conf = new LabelConfigurations("asdf", "asdf");

            var clientRepository = new Mock<IClientsModelRepository>();
            var clientconfigRepository = new Mock<IClientQueryRepository>();
            var configRepository = new Mock<ILabelConfigurationQueryRepository>();

            configRepository.Setup(x => x.GetByClientId(client.ClientCode))
                .Returns(Task.FromResult<LabelConfigurations?>(conf));

            clientconfigRepository.Setup(x => x.GetByIdAsync(client.Id))
                .Returns(Task.FromResult<ClientsModel?>(client));

            var command = new DeleteClientCommand(client.Id);
            var sut = new DeleteClientCommandHandler(clientRepository.Object, clientconfigRepository.Object, configRepository.Object);

            //ACT
            await sut.HandleAsync(command);

            //ASSERT
        }

        
        [Fact]
        public async Task Handle_Throws_If_Client_Not_Exist()
        {
            //ARRANGE
            string nombre = "hola";
            var client = new ClientsModel("asdfasf", nombre, nombre);
            var conf = new LabelConfigurations("asdf", "asdf");

            var clientRepository = new Mock<IClientsModelRepository>();
            var clientconfigRepository = new Mock<IClientQueryRepository>();
            var configRepository = new Mock<ILabelConfigurationQueryRepository>();

            configRepository.Setup(x => x.GetByClientId(client.ClientCode))
                .Returns(Task.FromResult<LabelConfigurations?>(conf));

            clientconfigRepository.Setup(x => x.GetByIdAsync(client.Id))
                .Returns(Task.FromResult<ClientsModel?>(null));

            var command = new DeleteClientCommand(client.Id);
            var sut = new DeleteClientCommandHandler(clientRepository.Object, clientconfigRepository.Object, configRepository.Object);

            //ACT
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
        
        [Fact]
        public async Task Handle_Throws_If_Relaction_Exist()
        {
            //ARRANGE
            string nombre = "hola";
            var client = new ClientsModel("asdfasf", nombre, nombre);
            var conf = new LabelConfigurations("asdf", "asdf");

            var clientRepository = new Mock<IClientsModelRepository>();
            var clientconfigRepository = new Mock<IClientQueryRepository>();
            var configRepository = new Mock<ILabelConfigurationQueryRepository>();

            configRepository.Setup(x => x.GetByClientId(client.ClientCode))
                .Returns(Task.FromResult<LabelConfigurations?>(null));

            clientconfigRepository.Setup(x => x.GetByIdAsync(client.Id))
                .Returns(Task.FromResult<ClientsModel?>(null));

            var command = new DeleteClientCommand(client.Id);
            var sut = new DeleteClientCommandHandler(clientRepository.Object, clientconfigRepository.Object, configRepository.Object);

            //ACT
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
