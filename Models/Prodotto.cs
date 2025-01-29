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


        //COSTRUTTORI
        public Prodotto(int id, string codice_SKU, string nome, string descrizione, double prezzo, 
                        string categoria, bool stato, DateTime data_Inserimento) : base (id)
        {
            Codice_SKU = codice_SKU;
            Nome = nome;
            Descrizione = descrizione;
            Prezzo = prezzo;
            Categoria = categoria;
            Stato = stato;
            Data_Inserimento = data_Inserimento;
        }
        public Prodotto() { }



    }
}
