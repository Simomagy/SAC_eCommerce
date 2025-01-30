using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models
{
    public class Prodotto : Entity
    {
        //PROPERTIES
        public string Codice_SKU { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public string Categoria { get; set; }
        public bool Stato { get; set; }
        public DateTime Data_Inserimento { get; set; }
    }
}
