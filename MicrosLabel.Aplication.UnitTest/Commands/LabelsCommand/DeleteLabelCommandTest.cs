using FluentAssertions;
using MicrosLabel.Application.Commands.ClientCommand;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;
using MicrosLabel.Reads.ViewModels;
using Moq;

namespace MicrosLabel.Aplication.UnitTest.Commands.LabelsCommand
{
    public class DeleteLabelCommandTest
    {
        [Fact]
        public async Task Handle_Works()
        {
            //ARRANGE
            var label = new LabelsModel("ADSF", 1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");

            var labelModelRepo = new Mock<ILabelsModelRepository>();
            var LabelQueryRepo = new Mock<ILabelQueryRepository>();
            var configQueryRepo = new Mock<ILabelConfigurationQueryRepository>();

            configQueryRepo.Setup(x => x.GetByDiscriminatorId(label.Id))
                .Returns(Task.FromResult<LabelConfigurations?>(null));
            LabelQueryRepo.Setup(x => x.GetByIdAsync(label.Id))
                .Returns(Task.FromResult<LabelsModel?>(label));

            var command = new DeleteLabelCommand(label.Id);
            var sut = new DeleteLabelCommandHandler(labelModelRepo.Object, LabelQueryRepo.Object, configQueryRepo.Object);

            //ACT
            await sut.HandleAsync(command);
        
            //ASSERT
        }

        [Fact]
        public async Task Handle_Throws_If_Label_Not_Exist()
        {
            //ARRANGE
            var label = new LabelsModel("ADSF", 1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");

            var labelModelRepo = new Mock<ILabelsModelRepository>();
            var LabelQueryRepo = new Mock<ILabelQueryRepository>();
            var configQueryRepo = new Mock<ILabelConfigurationQueryRepository>();

            configQueryRepo.Setup(x => x.GetByDiscriminatorId(label.Id))
                .Returns(Task.FromResult<LabelConfigurations?>(null));
            LabelQueryRepo.Setup(x => x.GetByIdAsync(label.Id))
                .Returns(Task.FromResult<LabelsModel?>(null));

            var command = new DeleteLabelCommand(label.Id);
            var sut = new DeleteLabelCommandHandler(labelModelRepo.Object, LabelQueryRepo.Object, configQueryRepo.Object);

            //ACTION
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
        
        [Fact]
        public async Task Handle_Throws_If_Configuration_Exist()
        {
            //ARRANGE
            var label = new LabelsModel("ADSF", 1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var configuration = new LabelConfigurations("codclient", "discriminador");

            var labelModelRepo = new Mock<ILabelsModelRepository>();
            var LabelQueryRepo = new Mock<ILabelQueryRepository>();
            var configQueryRepo = new Mock<ILabelConfigurationQueryRepository>();

            configQueryRepo.Setup(x => x.GetByDiscriminatorId(label.Id))
                .Returns(Task.FromResult<LabelConfigurations?>(configuration));
            LabelQueryRepo.Setup(x => x.GetByIdAsync(label.Id))
                .Returns(Task.FromResult<LabelsModel?>(label));

            var command = new DeleteLabelCommand(label.Id);
            var sut = new DeleteLabelCommandHandler(labelModelRepo.Object, LabelQueryRepo.Object, configQueryRepo.Object);

            //ACTION
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
