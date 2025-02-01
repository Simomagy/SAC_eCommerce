using MSSTU.DB.Utility;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.Xml;

namespace SAC_eCommerce.Models.Classes
{
    public class Feedback : Entity
    {
        public Ordine Ordine { get; set; }
        public Utente Utente { get; set; }
        public byte Valutazione { get; set; }
        public string Testo { get; set; }
        public DateTime Data_Inserimento { get; set; } = DateTime.Now;
    }
}
