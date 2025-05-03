using FluentAssertions;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Prints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Domain.UnitTest.Aggregates
{
    public class LabelPrintTest
    {
        private readonly LabelPrintModel _print;

        public LabelPrintTest()
        {
            _print = new LabelPrintModel("ID");
        }

        //PERFECT CASES

        [Fact]
        public void Can_Print_Perfect_Case()
        {
            var print = new LabelPrintModel("ID");
            print.Id.Should().Be("ID");
        }

        //
        //WRONG CASES
        [Fact]
        public void Cant_Print_Label_Null_Id()
        {
            Action action = () => new LabelPrintModel(null);
            action.Should().Throw<System.ArgumentNullException>();
        }
    }
}
