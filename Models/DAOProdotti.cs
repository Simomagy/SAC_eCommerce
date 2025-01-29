using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models
{
    public class DAOProdotti
    {
        #region Inizializzazione

        private readonly Database _db;
        private readonly string _tabella;

        private DAOProdotti(IConfiguration configuration)
        {
            _db = new Database(configuration["db_name"], configuration["server_db"]);
            _tabella = configuration["tables.prodotti"];
        }

        #endregion
        public bool CreateRecord(Prodotto prodotto)
        {
            try
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
            catch (Exception ex)
            {
                // Log exception
                return false;
            }
        }

        public bool DeleteRecord(int id)
        {
            try
            {
                var query = $"DELETE FROM {_tabella} WHERE ID_Prodotto = @ID_Prodotto";
                var parameters = new Dictionary<string, object>
                    {
                        {"@ID_Prodotto", id}
                    };

                return _db.UpdateDb(query, parameters);
            }
            catch (Exception ex)
            {
                // Log exception
                return false;
            }
        }

        public List<Prodotto> GetRecords()
        {
            try
            {
                var query = $"SELECT * FROM {_tabella}";
                var dataTable = _db.ReadDb(query);

                var prodotti = new List<Prodotto>();
                foreach (var row in dataTable)
                {
                    var prodotto = new Prodotto
                    {
                        ID_Prodotto = Convert.ToInt32(row["ID_Prodotto"]),
                        Codice_SKU = row["Codice_SKU"].ToString(),
                        Nome = row["Nome"].ToString(),
                        Descrizione = row["Descrizione"].ToString(),
                        Prezzo = Convert.ToDecimal(row["Prezzo"]),
                        Categoria = row["Categoria"].ToString(),
                        Stato = Convert.ToBoolean(row["Stato"]),
                        Data_Inserimento = Convert.ToDateTime(row["Data_Inserimento"])
                    };
                    prodotti.Add(prodotto);
                }

                return prodotti;
            }
            catch (Exception ex)
            {
                // Log exception
                return new List<Prodotto>();
            }
        }

        public bool ProdottoExists(int id)
        {
            try
            {
                var query = $"SELECT COUNT(*) as Count FROM {_tabella} WHERE ID_Prodotto = @ID_Prodotto";
                var parameters = new Dictionary<string, object>
                {
                    {"@ID_Prodotto", id}
                };
                var result = _db.ReadDb(query, parameters);
                return result != null && result.Count > 0 && Convert.ToInt32(result[0]["Count"]) > 0;
            }
            catch (Exception ex)
            {
                // Log exception
                return false;
            }
        }

        public bool FindProdotto(int id)
        {
            try
            {
                var query = $"SELECT * FROM {_tabella} WHERE ID_Prodotto = @ID_Prodotto";
                var parameters = new Dictionary<string, object>
                {
                    {"@ID_Prodotto", id}
                };
                var result = _db.ReadDb(query, parameters);
                return result != null && result.Count > 0;
            }
            catch (Exception ex)
            {
                // Log exception
                return false;
            }
        }
    }
}
