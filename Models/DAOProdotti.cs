using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models
{
    public class DAOProdotti
    {
        #region Inizializzazione

        private readonly Database _db;
        private readonly string _tabella;

        private DaoProdotti(IConfiguration configuration)
        {
            _db = new Database(configuration["db_name"], configuration["server_db"]);
            _tabella = configuration["tables.prodotti"];
        }

        #endregion

        #region CRUD

        public bool CreateRecord(Entity entity)
        {
            var prodotto = (Prodotto)entity;
            var parametri = new Dictionary<string, object>
            {
                {"@codice_SKU", prodotto.Codice_SKU.Replace("'", "''")},
                {"@nome", prodotto.Nome.Replace("'", "''")},
                {"@descrizione", prodotto.Descrizione.Replace("'", "''")},
                {"@prezzo", prodotto.Prezzo},
                {"@categoria", prodotto.Categoria.Replace("'", "''")},
            };

            var query = $"INSERT INTO {_tabella} (Codice_SKU, Nome, Descrizione, Prezzo, Categoria) " +
                        $"VALUES (@codice_SKU, @nome, @descrizione, @prezzo, @categoria ) ";

            var response = _db.UpdateDb(query, parametri);

            return response;
        }

        public List<Prodotto> GetRecords()
        {
            const string query = $"SELECT * FROM {_tabella}"
            List<Prodotto> utenti = [];
            var fullResponse = _db.ReadDb(query);
            if (fullResponse == null)
                return utenti;

            foreach (var singleResponse in fullResponse)
            {
                var ut = new Prodotto();
                ut.TypeSort(singleResponse);

                utenti.Add(ut);
            }
            return utenti;
        }


        public bool UpdateRecord(Entity entity)
        {
            var prodotto = (Prodotto)entity;
            var parametri = new Dictionary<string, object>
            {
                {"@codice_SKU", prodotto.Codice_SKU.Replace("'", "''")},
                {"@nome", prodotto.Nome.Replace("'", "''")},
                {"@descrizione", prodotto.Descrizione.Replace("'", "''")},
                {"@prezzo", prodotto.Prezzo},
                {"@categoria", prodotto.Categoria.Replace("'", "''")},
            };

            var query = $"UPDATE {_tabella} SET " +
                        "Codice_SKU = @codice_SKU, " +
                        "Nome = @nome, " +
                        "Descrizione = @descrizione, " +
                        "Prezzo = @prezzo, " +
                        "Categoria = @categoria, " +
                       $"WHERE id_prodotto = {prodotto.Id}; ";

            var response = _db.UpdateDb(query, parametri);

            return response;
        }

        public bool DeleteRecord(int id)
        {
            string query = $"DELETE FROM {_tabella} WHERE id_prodotto = {id})";

            return _db.UpdateDb(query);
        }

        public Prodotto FindUser(int id)
        {
            const string query = $"SELECT * FROM {_tabella} where id = {id}";

            var singleResponse = _db.ReadOneDb(query);
            if (singleResponse == null)
                return null;

            var ut = new Prodotto();
            ut.TypeSort(singleResponse);

            return ut;
        }
        #endregion
    }
}
