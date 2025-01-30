using MSSTU.DB.Utility;
using SAC_eCommerce.Models.Classes;

namespace SAC_eCommerce.Models.Daos
{
    public class DaoProdotti
    {
        #region Inizializzazione
        private readonly Database _db;
        private readonly string _tabella;

        public DaoProdotti(IConfiguration configuration)
        {
            _db = new Database(configuration["db_name"], configuration["server_db"]);
            _tabella = configuration["tables.prodotti"];
        }
        #endregion
        public bool CreateRecord(Prodotto prodotto)
        {

            var query = $"INSERT INTO {_tabella} (Codice_SKU, Nome, Descrizione, Prezzo, Categoria, Stato, Data_Inserimento) " +
                        "VALUES (@Codice_SKU, @Nome, @Descrizione, @Prezzo, @Categoria, @Stato, @Data_Inserimento)";
            var parameters = new Dictionary<string, object>
            {
                {"@Codice_SKU", prodotto.Codice_SKU},
                {"@Nome", prodotto.Nome},
                {"@Descrizione", prodotto.Descrizione},
                {"@Prezzo", prodotto.Prezzo},
                {"@Categoria", prodotto.Categoria},
                {"@Stato", prodotto.Stato},
                {"@Data_Inserimento", prodotto.Data_Inserimento}
            };
            return _db.UpdateDb(query, parameters);

        }

        public bool UpdateRecord(Prodotto prodotto)
        {

            var query = $"UPDATE {_tabella} SET Codice_SKU = @Codice_SKU, Nome = @Nome, Descrizione = @Descrizione, Prezzo = @Prezzo, Categoria = @Categoria, Stato = @Stato, Data_Inserimento = @Data_Inserimento WHERE ID_Prodotto = @ID_Prodotto";
            var parameters = new Dictionary<string, object>
            {
                {"@ID_Prodotto", prodotto.Id},
                {"@Codice_SKU", prodotto.Codice_SKU},
                {"@Nome", prodotto.Nome},
                {"@Descrizione", prodotto.Descrizione},
                {"@Prezzo", prodotto.Prezzo},
                {"@Categoria", prodotto.Categoria},
                {"@Stato", prodotto.Stato},
                {"@Data_Inserimento", prodotto.Data_Inserimento}
            };
            return _db.UpdateDb(query, parameters);

        }

        public bool DeleteRecord(int id)
        {

            var query = $"DELETE FROM {_tabella} WHERE ID_Prodotto = @ID_Prodotto";
            var parameters = new Dictionary<string, object>
            {
                {"@ID_Prodotto", id}
            };
            return _db.UpdateDb(query, parameters);

        }
        public List<Prodotto> GetRecords()
        {
            var query = $"SELECT * FROM {_tabella}";
            var dataTable = _db.ReadDb(query);
            var prodotti = new List<Prodotto>();
            if (dataTable == null)
                return prodotti;

            foreach (var row in dataTable)
            {
                var prodotto = new Prodotto();
                prodotto.TypeSort(row);
                prodotti.Add(prodotto);
            }
            return prodotti;
        }
        public bool ProdottoExists(int id)
        {
            var query = $"SELECT * FROM {_tabella} WHERE ID_Prodotto = @ID_Prodotto";
            var parameters = new Dictionary<string, object>
            {
                {"@ID_Prodotto", id}
            };
            var result = _db.ReadDb(query, parameters);
            return result is { Count: > 0 };
        }
        public bool FindProdotto(int id)
        {
            var query = $"SELECT * FROM {_tabella} WHERE ID_Prodotto = @ID_Prodotto";
            var parameters = new Dictionary<string, object>
            {
                {"@ID_Prodotto", id}
            };
            var result = _db.ReadDb(query, parameters);
            return result is { Count: > 0 };
        }
    }
}
