using System.ComponentModel.DataAnnotations;

namespace MicrosLabel.Domain.Aggregates.Clients
{
    public class ClientsModel : Aggregate
    { 

        public ClientsModel(string clientCode, string nombre) 
        {
            ClientsValidation.NotNull(nombre);
            ClientsValidation.NotNull(clientCode);

            ClientCode = clientCode;
            Nombre = nombre;
        }

        public ClientsModel(string id, string clientCode, string nombre)
        {
            ClientsValidation.NotNull(id);
            ClientsValidation.NotNull(clientCode);
            ClientsValidation.NotNull(nombre);

            Id = id; 
            ClientCode = clientCode;
            Nombre = nombre;

        }

        public ClientsModel()
        {

        }

        public string ClientCode { get; set; }
        public string Nombre { get; set; }
    }
}
