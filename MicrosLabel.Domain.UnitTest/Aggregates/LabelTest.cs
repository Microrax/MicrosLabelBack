using FluentAssertions;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using System.Drawing;

namespace MicrosLabel.Domain.UnitTest.Aggregates
{
    public class LabelTest
    {

        private readonly LabelsModel _label;

        public LabelTest()
        {
            _label = new LabelsModel();
        }

        //NO ID TEST

        
        [Fact]
        public void Can_Create_Client_Perfect_Case_Both_Attributes_Constructor()
        {
            var label = new LabelsModel(1, "ASDF" ,"ASDF",1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            label.LabelType.Should().Be(1);
            label.Template.Should().Be("ASDF");
            label.Zpl.Should().Be("ASDF");
            label.Dpi.Should().Be(1234);
            label.Sql.Should().Be("SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            
        }

        [Theory]
        [InlineData(-1)]
        public void Cant_Create_Label_Negative_LabelType_Case_Non_Id_Constructor(int labeltype)
        {
            Action action =()=> new LabelsModel(labeltype, "ASDF", "ASDF", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            action.Should().Throw<System.ArgumentException>();

        }

        [Theory]
        [InlineData(5)]
        public void Cant_Create_Label_Out_Of_Range_LabelType_Case_Non_Id_Constructor(int labeltype)
        {
            Action action = () => new LabelsModel(labeltype, "ASDF", "ASDF", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            action.Should().Throw<System.ArgumentException>();

        }

        [Theory]
        [InlineData(-1)]
        public void Cant_Create_Label_Negative_Dpi_Case_Non_Id_Constructor(int dpi)
        {
            Action action = () => new LabelsModel(1234, "ASDF", "ASDF", dpi, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            action.Should().Throw<System.ArgumentException>();

        }

        [Theory]
        [InlineData(4555)]
        public void Cant_Create_Label_Out_Of_Range_Dpi_Case_Non_Id_Constructor(int dpi)
        {
            Action action = () => new LabelsModel(1234, "ASDF", "ASDF", dpi, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            action.Should().Throw<System.ArgumentException>();

        }

        [Theory]
        [InlineData(null)]
        public void Cant_Create_Label_Null_Template_Case_Non_Id_Constructor(string nullstring)
        {
            Action action = () => new LabelsModel(1234, nullstring, "ASDF", 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentNullException>(); ;
        }
        [Theory]
        [InlineData(null)]
        public void Cant_Create_Label_Null_Zpl_Case_Non_Id_Constructor(string nullstring)
        {
            Action action = () => new LabelsModel(1234, "ASDF", nullstring, 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentNullException>(); ;
        }

        [Theory]
        [InlineData(null)]
        public void Cant_Create_Label_Null_Sql_Case_Non_Id_Constructor(string nullstring)
        {
            Action action = () => new LabelsModel(1234, "ASDF", "ASDF", 1234, nullstring);
            action.Should().Throw<System.ArgumentNullException>(); ;
        }

        [Fact]
        public void Cant_Create_Label_SqlInyection_Case_Non_Id_Constructor()
        {
            Action action = () => new LabelsModel(1234, "ASDF", "ASDF", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10 DROP DATABASE");
            action.Should().Throw<System.ArgumentException>(); ;
        }

        [Fact]
        public void Cant_Create_LabeL_Query_Zpl_Data_Case_Non_Id_Constructor()
        {
            Action action = () => new LabelsModel(1234, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdASf FROM asdf WHERE ClientCode = 10 AND sku = 10");
            action.Should().Throw<System.ArgumentException>(); ;
        }


        [Fact]
        public void Cant_Create_LabeL_Query_Validation_Case_Non_Id_Constructor()
        {
            Action action = () => new LabelsModel(1234, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf  ClientCode = 10 AND  = 10");
            action.Should().Throw<System.ArgumentException>(); ;
        }

        [Fact]
        public void Cant_Create_Label_Empty_Template_Case_Non_Id_Constructor()
        {
            Action action = () => new LabelsModel(1234, "  ", "ASDF", 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentNullException>(); ;
        }

        [Fact]
        public void Cant_Create_Label_Empty_Zpl_Case_Non_Id_Constructor()
        {
            Action action = () => new LabelsModel(1234, "ASDF", "  ", 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentNullException>(); ;
        }

        [Fact]
        public void Cant_Create_Label_Empty_Sql_Case_Non_Id_Constructor()
        {
            Action action = () => new LabelsModel(1234, "ASDF", "ASDF", 1234, "    ");
            action.Should().Throw<System.ArgumentNullException>(); ;
        }






        //
        //
        //
        //ID TEST
        //
        //
        //


        [Fact]
        public void Can_Create_Client_Perfect_Case_Id_Constructor()
        {
            var label = new LabelsModel("ASDF",1, "ASDF", "ASDF", 1234, "SELECT ASDF from WHERE clientcode sku");
            label.Id.Should().Be("ASDF");
            label.LabelType.Should().Be(1);
            label.Template.Should().Be("ASDF");
            label.Zpl.Should().Be("ASDF");
            label.Dpi.Should().Be(1234);
            label.Sql.Should().Be("SELECT ASDF from WHERE clientcode sku");

        }

        [Theory]
        [InlineData(-1)]
        public void Cant_Create_Label_Negative_LabelType_Case_Id_Constructor(int labeltype)
        {
            Action action = () => new LabelsModel("ASDF",labeltype, "ASDF", "ASDF", 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentException>();

        }

        [Theory]
        [InlineData(24)]
        public void Cant_Create_Label_Out_Of_Range_LabelType_Case_Id_Constructor(int labeltype)
        {
            Action action = () => new LabelsModel("ASDF", labeltype, "ASDF", "ASDF", 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentException>();

        }

        [Theory]
        [InlineData(-1)]
        public void Cant_Create_Label_Negative_Dpi_Case_Id_Constructor(int labeltype)
        {
            Action action = () => new LabelsModel("ASDF",1234, "ASDF", "ASDF", labeltype, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentException>();

        }

        [Theory]
        [InlineData(4000)]
        public void Cant_Create_Label_Out_Of_Range_Dpi_Case_Id_Constructor(int dpi)
        {
            Action action = () => new LabelsModel("ASDF", 1234, "ASDF", "ASDF", dpi, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentException>();

        }

        [Theory]
        [InlineData(null)]
        public void Cant_Create_Label_Null_Template_Case_Id_Constructor(string nullstring)
        {
            Action action = () => new LabelsModel("ASDF",1234, nullstring, "ASDF", 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentNullException>(); ;
        }

        [Theory]
        [InlineData(null)]
        public void Cant_Create_Label_Null_Zpl_Case_Id_Constructor(string nullstring)
        {
            Action action = () => new LabelsModel("ASDF",1234, "ASDF", nullstring, 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentNullException>(); ;
        }

        [Theory]
        [InlineData(null)]
        public void Cant_Create_Label_Null_Sql_Case_Id_Constructor(string nullstring)
        {
            Action action = () => new LabelsModel("ASDF",1234, "ASDF", "ASDF", 1234, nullstring);
            action.Should().Throw<System.ArgumentNullException>(); ;
        }

        [Fact]
        public void Cant_Create_Label_SqlInyection_Case_Id_Constructor()
        {
            Action action = () => new LabelsModel("ASDF", 1234, "ASDF", "ASDF", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10 DROP DATABASE");
            action.Should().Throw<System.ArgumentException>(); ;
        }

        [Fact]
        public void Cant_Create_Label_Query_Zpl_Data_Case_Id_Constructor()
        {
            Action action = () => new LabelsModel("ASDF", 1234, "ASDF", "ASDF{{asdf}}", 1234, "SELECT ASASDFDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentException>(); ;
        }

        [Fact]
        public void Cant_Create_LabeL_Query_Validation_Case_Id_Constructor()
        {
            Action action = () => new LabelsModel(1234, "ASDF", "ASDF{{asdf}}", 1234, "SELECT asdf FROM asdf  ClientCode = 10 AND  = 10");
            action.Should().Throw<System.ArgumentException>(); ;
        }

        [Fact]
        public void Cant_Create_Label_Empty_Template_Case_Id_Constructor()
        {
            Action action = () => new LabelsModel("ASDF", 1234, "  ", "ASDF", 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentNullException>(); ;
        }

        [Fact]
        public void Cant_Create_Label_Empty_Zpl_Case_Id_Constructor()
        {
            Action action = () => new LabelsModel("ASDF", 1234, "ASDF", "  ", 1234, "SELECT ASDF from WHERE clientcode sku");
            action.Should().Throw<System.ArgumentNullException>(); ;
        }

        [Fact]
        public void Cant_Create_Label_Empty_Sql_Case_Id_Constructor()
        {
            Action action = () => new LabelsModel("ASDF", 1234, "ASDF", "ASDF", 1234, "    ");
            action.Should().Throw<System.ArgumentNullException>();
        }

        [Fact]
        public void CanT_Create_Client_Empty_Id_Case_Both_Attributes_Constructor()
        {
            Action action = () => new LabelsModel(" ", 1234, "ASDF", "ASDF", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10 DROP DATABASE");
            action.Should().Throw<System.ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        public void CanT_Create_Client_Null_Id_Case_Both_Attributes_Constructor(string nullstring)
        {
            Action action = () => new LabelsModel(nullstring, 1234, "ASDF", "ASDF", 1234, "SELECT asdf FROM asdf WHERE ClientCode = 10 AND sku = 10 DROP DATABASE");
            action.Should().Throw<System.ArgumentNullException>();
        }
    }
}