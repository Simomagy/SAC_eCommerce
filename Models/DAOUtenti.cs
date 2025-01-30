using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models
{
    public class DAOUtenti
    {
        #region Inizializzazione

        private readonly Database _db;
        private readonly string _tabella;

        private DaoUtenti(IConfiguration configuration)
        {
            _db = new Database(configuration["db_name"], configuration["server_db"]);
            _tabella = configuration["tables.utenti"];
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
            const string query = $"SELECT * FROM {_tabella}"
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
            string query = $"DELETE FROM {_tabella} WHERE id_utente = {id})";

            return _db.UpdateDb(query);
        }

        public Utente FindUser(int id)
        {
            const string query = $"SELECT * FROM {_tabella} where id = {id}";

            var singleResponse = _db.ReadOneDb(query);
            if (singleResponse == null)
                return null;

            var ut = new Utente();
            ut.TypeSort(singleResponse);

            return ut;
        }
        #endregion
    }
}
