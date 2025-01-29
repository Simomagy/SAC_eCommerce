using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models
{
    public class Utente : Entity
    {
        //PROPERTIES
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Points { get; set; }
        public string Card_Number { get; set; }
        public DateTime Data_Registrazione { get; set; }


        //COSTRUTTORI
        public Utente(int id, string nome, string cognome, string email, string password, 
                      string role, int points, string card_Number, DateTime data_Registrazione) : base (id)
        {
            Nome = nome;
            Cognome = cognome;
            Email = email;
            Password = password;
            Role = role;
            Points = points;
            Card_Number = card_Number;
            Data_Registrazione = data_Registrazione;
        }
        public Utente() { }


        //METODI
        public override string ToString()
        {
            return $"\nDati Utente:" +
                   $"\n\tNominativo: {Nome} {Cognome}" +
                   $"\n\tEmail: {Email}" +
                   $"\n\tPassword: {Password}" +
                   $"\n\tRuolo: {Role}" +
                   $"\n\tPunti accumulati: {Points}" +
                   $"\n\tCarta numero: {Card_Number}" +
                   $"\n\tData Registrazione: {Data_Registrazione}";
        }

    }
}
