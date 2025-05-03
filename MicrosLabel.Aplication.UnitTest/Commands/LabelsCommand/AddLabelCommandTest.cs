using FluentAssertions;
using MicrosLabel.Application.Commands.ClientCommand;
using MicrosLabel.Application.Commands.LabelsCommands;
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

namespace MicrosLabel.Aplication.UnitTest.Commands.LabelsCommand
{
    public class AddLabelCommandTest
    {
        [Fact]
        private static async Task Handle_Works()
        {
            //ARRANGE
            var lbabel = new LabelsModel(1, "ASDF{{asdf}}", "asdf", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var label = new LabelViewModel("asdf", "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");

            var labelRepo = new Mock<ILabelsModelRepository>();
            var labelqueryRepo = new Mock<ILabelQueryRepository>();

            labelqueryRepo.Setup(x => x.ExistLabel(lbabel.LabelType, lbabel.Template, lbabel.Dpi))
               .Returns(Task.FromResult<LabelViewModel?>(null));

            var command = new AddLabelCommand(lbabel.LabelType, lbabel.Template, lbabel.Zpl, lbabel.Dpi, lbabel.Sql);
            var sut = new AddLabelCommandHandler(labelRepo.Object, labelqueryRepo.Object);

            //ACT
            await sut.HandleAsync(command);
        
            //ASERT
        }

        [Fact]
        private static async Task Handle_Throws_If_Label_Exist()
        {
            //ARRANGE
            var lbabel = new LabelsModel(1, "ASDF{{asdf}}", "asdf", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            var label = new LabelViewModel("asdf", "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");

            var labelRepo = new Mock<ILabelsModelRepository>();
            var labelqueryRepo = new Mock<ILabelQueryRepository>();

            labelqueryRepo.Setup(x => x.ExistLabel(lbabel.LabelType, lbabel.Template, lbabel.Dpi))
               .Returns(Task.FromResult<LabelViewModel?>(label));

            var command = new AddLabelCommand(lbabel.LabelType, lbabel.Template, lbabel.Zpl, lbabel.Dpi, lbabel.Sql);
            var sut = new AddLabelCommandHandler(labelRepo.Object, labelqueryRepo.Object);

            //ACTION
            Func<Task> action = async () => await sut.HandleAsync(command);

            //ASSERT
            await action.Should().ThrowAsync<InvalidOperationException>();
        }


    }
}
