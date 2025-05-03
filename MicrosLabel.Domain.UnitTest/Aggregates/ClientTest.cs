using FluentAssertions;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using System.Drawing;

namespace MicrosLabel.Domain.UnitTest.Aggregates
{
    public class ClientTest
    {

        private readonly ClientsModel _client;

        public ClientTest()
        {
            _client = new ClientsModel();
        }

        //PERFECT CASES

        [Fact]
        public void Can_Create_Client_Perfect_Case_Both_Attributes_Constructor()
        {
            var client = new ClientsModel("ASDF", "ASDF", "ASDF");
            client.Id.Should().Be("ASDF");
            client.ClientCode.Should().Be("ASDF");
            client.Nombre.Should().Be("ASDF");
        }

        [Fact]
        public void Can_Create_Client_Perfect_Case_Id_Constructor()
        { 
            var client = new ClientsModel("ASDF", "ASDF");
            client.ClientCode.Should().Be("ASDF");
            client.Nombre.Should().Be("ASDF");
        }

        //WRONG CASES
        //
        //ID CONSTRUCTOR
        [Fact]
        public void CanT_Create_Client_Null_Id_Case_Id_Constructor()
        {
            Action action = () => new ClientsModel(null, "ASDF", "ASDF");
            action.Should().Throw<System.ArgumentNullException>();
        }
        
        [Fact]
        public void CanT_Create_Client_Null_ClientCode_Case_Id_Constructor()
        {
            Action action = () => new ClientsModel("ASDF", null, "ASDF");
            action.Should().Throw<System.ArgumentNullException>();
        }
        
        [Fact]
        public void CanT_Create_Client_Null_Name_Case_Id_Constructor()
        {
            Action action = () => new ClientsModel("ASDF", "ASDF", null);
            action.Should().Throw<System.ArgumentNullException>();
        }

        [Fact]
        public void CanT_Create_Client_Null_Id_Case_Both_Attributes_Constructor()
        {
            Action action = () => new ClientsModel(null, "ASDF");
            action.Should().Throw<System.ArgumentNullException>();
        }

        [Fact]
        public void CanT_Create_Client_Null_Nombre_Case_Both_Attributes_Constructor()
        {
            Action action = () => new ClientsModel("ASDF", null);
            action.Should().Throw<System.ArgumentNullException>();
        }
    }
}