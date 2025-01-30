using Microsoft.AspNetCore.Http.HttpResults;
using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models.Classes
{
    public class Negozio : Entity
    {
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string Provincia { get; set; }
        public DateTime Data_Apertura { get; set; } = DateTime.Now;

    }
}
