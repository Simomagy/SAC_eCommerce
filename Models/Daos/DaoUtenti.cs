using MSSTU.DB.Utility;
using SAC_eCommerce.Models.Classes;

namespace SAC_eCommerce.Models.Daos
{
    public class DaoUtenti
    {
        #region Inizializzazione

        private readonly Database _db;
        private readonly string _tabella;

        public DaoUtenti(IConfiguration configuration)
        {
            _db = new Database(configuration["db_name"], configuration["server_db"]);
            _tabella = configuration["tables:utenti"];
        }

        #endregion

        #region CRUD

        public bool CreateRecord(Entity entity)
        {
            var utente = (Utente)entity;
            var pwd = utente.Password;
            var parametri = new Dictionary<string, object>
            {
                {"@nome", utente.Nome.Replace("'", "''")},
                {"@cognome", utente.Cognome.Replace("'", "''")},
                {"@email", utente.Email.Replace("'", "''")},
                {"@points", utente.Points},
                {"@card_number", utente.Card_Number.Replace("'", "''")},
            };

            var query = $"INSERT INTO {_tabella} (Nome, Cognome, Email, Password, Points, Card_Number) " +
                        $"VALUES (@nome, @cognome, @email, HASHBYTES('SHA2_512', '{pwd}'), @points, @card_number ) ";

            var response = _db.UpdateDb(query, parametri);

            return response;
        }

        public List<Utente> GetRecords()
        {
            var query = $"SELECT * FROM {_tabella}";
            List<Utente> utenti = [];
            var fullResponse = _db.ReadDb(query);
            if (fullResponse == null)
                return utenti;

            foreach (var singleResponse in fullResponse)
            {
                var ut = new Utente();
                ut.TypeSort(singleResponse);

                utenti.Add(ut);
            }
            return utenti;
        }


        public bool UpdateRecord(Entity entity)
        {
            var utente = (Utente)entity;
            var pwd = utente.Password;
            var parametri = new Dictionary<string, object>
            {
                {"@nome", utente.Nome.Replace("'", "''")},
                {"@cognome", utente.Cognome.Replace("'", "''")},
                {"@email", utente.Email.Replace("'", "''")},
                {"@points", utente.Points},
                {"@card_number", utente.Card_Number.Replace("'", "''")},
            };

            var query =$"UPDATE {_tabella} SET " +
                        "Nome = @nome, " +
                        "Cognome = @cognome, " +
                        "Email = @email, " +
                       $"Password = HASHBYTES('SHA2_512', '{pwd}'), " +
                        "Points = @points, " +
                        "Card_Number = @card_number " +
                       $"WHERE id_cliente = {utente.Id}; ";

            var response = _db.UpdateDb(query, parametri);

            return response;
        }

        public bool DeleteRecord(int id)
        {
            var query = $"DELETE FROM {_tabella} WHERE id_utente = {id}";

            return _db.UpdateDb(query);
        }

        public Utente? FindUser(string email)
        {
            var query = $"SELECT * FROM {_tabella} WHERE email = '{email}'";
            var response = _db.ReadOneDb(query);

            if (response == null)
                return null;

            var utente = new Utente();
            utente.TypeSort(response);

            return utente;
        }

        public bool UserExists(string email, string password)
        {
            var query =
                $"SELECT * FROM {_tabella} WHERE Email = '{email}' AND Password = HASHBYTES('SHA2_512', '{password}')";
            var response = _db.ReadOneDb(query);

            return response != null;
        }
        #endregion
    }
}
