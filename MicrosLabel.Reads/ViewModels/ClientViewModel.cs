using MicrosLabel.Domain.Aggregates.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicrosLabel.Reads.ViewModels
{
    public class ClientsViewModel
    {
        public ClientsViewModel(string id, string clientCode, string nombre)
        {

            Id = id;
            ClientCode = clientCode;
            Nombre = nombre;

        }
         public string Id { get; set; }
             
        public string ClientCode { get; set; }

        public string Nombre { get; set; }
    }
}
