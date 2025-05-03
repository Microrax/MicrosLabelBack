using FluentAssertions;
using MicrosLabel.Application.Commands.LabelsCommands;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Infrastructure.Repositories;
using MicrosLabel.Reads.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Aplication.UnitTest.Commands.LabelsCommand
{
    public class PutLabelCommandTest
    {
        [Fact]
        private static async Task Handle_Works()
        {
            //ARRANGE
            var label = new LabelsModel("ADSF", 1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var labelRepo = new Mock<ILabelsModelRepository>();
            var labelConfigRepo = new Mock<ILabelQueryRepository>();

            labelConfigRepo.Setup(x => x.GetByIdAsync(label.Id))
                .Returns(Task.FromResult<LabelsModel?>(label));

            var command = new PutLabelCommand(label.Id, label.LabelType, label.Template, label.Zpl, label.Dpi, label.Sql);
            var sut = new PutLabelCommandHandler(labelRepo.Object, labelConfigRepo.Object);

            //ACT
            await sut.HandleAsync(command);
        
            //ASSERT
        }

        [Fact]
        public async Task Handle_Throws_If_Label_Not_Exist()
        {
            //ARRANGE
            var label = new LabelsModel("ADSF", 1, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var labelRepo = new Mock<ILabelsModelRepository>();
            var labelConfigRepo = new Mock<ILabelQueryRepository>();

            labelConfigRepo.Setup(x => x.GetByIdAsync(label.Id))
                .Returns(Task.FromResult<LabelsModel?>(null));

            var command = new PutLabelCommand(label.Id, label.LabelType, label.Template, label.Zpl, label.Dpi, label.Sql);
            var sut = new PutLabelCommandHandler(labelRepo.Object, labelConfigRepo.Object);

            //ACT
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
