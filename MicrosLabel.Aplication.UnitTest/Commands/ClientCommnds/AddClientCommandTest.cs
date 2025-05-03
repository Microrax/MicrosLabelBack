using FluentAssertions;
using MicrosLabel.Application.Commands.ClientCommand;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Reads.Queries;
using Moq;

namespace MicrosLabel.Aplication.UnitTest.Commands
{
    public class AddClientCommandTest
    {

        [Fact]
        private static async Task Handle_Works()
        {

            //ARRANGE
            string nombre = "hola";
            var client = new ClientsModel("001", nombre);
            var clientRepository = new Mock<IClientsModelRepository>();
            var clientQueryRepository = new Mock<IClientQueryRepository>();


            clientQueryRepository.Setup(x => x.GetByClientCodeAsync(client.ClientCode))
                .Returns(Task.FromResult<ClientsModel?>(null));
            clientQueryRepository.Setup(x => x.GetByNombreAsync(client.Nombre))
                .Returns(Task.FromResult<ClientsModel?>(null));


            var command = new AddClientCommand(client.ClientCode, client.Nombre);
            var sut = new AddClientCommandHandler(clientRepository.Object, clientQueryRepository.Object);

            //ACT
            await sut.HandleAsync(command);

            //ASSERT
        }

        [Fact]
        private static async Task Handle_Throws_Exist_ClientCode()
        {
            //ARRANGE
            string nombre = "hola";
            var client = new ClientsModel("001", nombre);
            var clientRepository = new Mock<IClientsModelRepository>();
            var clientQueryRepository = new Mock<IClientQueryRepository>();


            clientQueryRepository.Setup(x => x.GetByClientCodeAsync(client.ClientCode))
                .Returns(Task.FromResult<ClientsModel?>(client));
            clientQueryRepository.Setup(x => x.GetByNombreAsync(client.Nombre))
                .Returns(Task.FromResult<ClientsModel?>(null));


            var command = new AddClientCommand(client.ClientCode, client.Nombre);
            var sut = new AddClientCommandHandler(clientRepository.Object, clientQueryRepository.Object);

            //ACT
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
        
        [Fact]
        private static async Task Handle_Throws_Exist_Client()
        {
            //ARRANGE
            string nombre = "hola";
            var client = new ClientsModel("001", nombre);
            var clientRepository = new Mock<IClientsModelRepository>();
            var clientQueryRepository = new Mock<IClientQueryRepository>();


            clientQueryRepository.Setup(x => x.GetByClientCodeAsync(client.ClientCode))
                .Returns(Task.FromResult<ClientsModel?>(null));
            clientQueryRepository.Setup(x => x.GetByNombreAsync(client.Nombre))
                .Returns(Task.FromResult<ClientsModel?>(client));


            var command = new AddClientCommand(client.ClientCode, client.Nombre);
            var sut = new AddClientCommandHandler(clientRepository.Object, clientQueryRepository.Object);

            //ACT
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
