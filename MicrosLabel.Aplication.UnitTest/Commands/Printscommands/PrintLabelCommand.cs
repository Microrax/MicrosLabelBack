/*
using FluentAssertions;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Application.Commands.PrintLabelsCommand;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Reads.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Aplication.UnitTest.Commands.Printscommands
{
    public class PrintLabelCommand
    {
        [Fact]
        public async Task Handle_Works()
        {
            //ARRANGE
            var label = new LabelsModel("ADSF", 1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var config = new LabelConfigurations("ID1", "ID2");
            var labelModelRepo = new Mock<ILabelPrintQueryRepository>();
            var LabelQueryRepo = new Mock<ILabelQueryRepository>();
            var configQueryRepo = new Mock<ILabelConfigurationQueryRepository>();

            LabelQueryRepo.Setup(x => x.GetByIdAsync(label.Id))
                .Returns(Task.FromResult<LabelsModel?>(label));
            configQueryRepo.Setup(x => x.GetByDiscriminatorId(label.Id))
                .Returns(Task.FromResult<LabelConfigurations?>(config));

            var command = new Application.Commands.PrintLabelsCommand.PrintLabelCommand(label.Id);
            var sut = new PrintLabelCommandHandler(labelModelRepo.Object, LabelQueryRepo.Object, configQueryRepo.Object);

            //ACT
            await sut.HandleAsync(command);

            //ASSERT
        }

        [Fact]
        public async Task Handle_Throws_If_Label_Not_Exist()
        {
            //ARRANGE
            var label = new LabelsModel("ADSF", 1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var config = new LabelConfigurations("ID1", "ID2");
            var labelModelRepo = new Mock<ILabelPrintQueryRepository>();
            var LabelQueryRepo = new Mock<ILabelQueryRepository>();
            var configQueryRepo = new Mock<ILabelConfigurationQueryRepository>();

            LabelQueryRepo.Setup(x => x.GetByIdAsync(label.Id))
                .Returns(Task.FromResult<LabelsModel?>(null));
            configQueryRepo.Setup(x => x.GetByDiscriminatorId(label.Id))
                .Returns(Task.FromResult<LabelConfigurations?>(config));

            var command = new Application.Commands.PrintLabelsCommand.PrintLabelCommand(label.Id);
            var sut = new PrintLabelCommandHandler(labelModelRepo.Object, LabelQueryRepo.Object, configQueryRepo.Object);

            //ACTION
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Handle_Throws_If_LabelConfiguration_Not_Exist()
        {
            //ARRANGE
            var label = new LabelsModel("ADSF", 1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var config = new LabelConfigurations("ID1", "ID2");
            var labelModelRepo = new Mock<ILabelPrintQueryRepository>();
            var LabelQueryRepo = new Mock<ILabelQueryRepository>();
            var configQueryRepo = new Mock<ILabelConfigurationQueryRepository>();

            LabelQueryRepo.Setup(x => x.GetByIdAsync(label.Id))
                .Returns(Task.FromResult<LabelsModel?>(label));
            configQueryRepo.Setup(x => x.GetByDiscriminatorId(label.Id))
                .Returns(Task.FromResult<LabelConfigurations?>(null));

            var command = new Application.Commands.PrintLabelsCommand.PrintLabelCommand(label.Id);
            var sut = new PrintLabelCommandHandler(labelModelRepo.Object, LabelQueryRepo.Object, configQueryRepo.Object);

            //ACTION
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
*/