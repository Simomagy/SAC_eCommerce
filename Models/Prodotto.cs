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


        // Costruttore vuoto
        public Prodotto() { }

        // Costruttore con parametri
        public Prodotto( int id, int id_prodotto, string codice_sku, string nome, string descrizione, decimal prezzo, string categoria, bool stato, DateTime data_inserimento) : base(id)
        {
            ID_Prodotto = id_prodotto;
            Codice_SKU = codice_sku;
            Nome = nome;
            Descrizione = descrizione;
            Prezzo = prezzo;
            Categoria = categoria;
            Stato = stato;
            Data_Inserimento = data_inserimento;
        }


    }
}
