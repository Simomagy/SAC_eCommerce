using MSSTU.DB.Utility;
using System;

namespace SAC_eCommerce.Models
{
    public class Prodotto : Entity 
    {
        public int ID_Prodotto { get; set; }
        public string Codice_SKU { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public string Categoria { get; set; }
        public bool Stato { get; set; } = true; 
        public DateTime Data_Inserimento { get; set; } = DateTime.Now;

    }
}
