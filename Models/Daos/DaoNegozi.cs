using MSSTU.DB.Utility;
using SAC_eCommerce.Models.Classes;

namespace SAC_eCommerce.Models.Daos
{
    public class DaoNegozi
    {
        #region Inizializzazione
        private readonly Database _db;
        private readonly string _tabella;

        public DaoNegozi(IConfiguration configuration)
        {
            _db = new Database(configuration["db_name"], configuration["server_db"]);
            _tabella = configuration["tables:negozi"];
        }
        #endregion

        public bool CreateRecord(Entity entity)
        {
            var negozio = (Negozio)entity;
            var query = $"INSERT INTO {_tabella} (Nome, Indirizzo, Citta, CAP, Provincia, Data_Apertura) " +
                        "VALUES (@Nome, @Indirizzo, @Citta, @CAP, @Provincia, @Data_Apertura)";
            var parameters = new Dictionary<string, object>
                            {
                                {"@Nome", negozio.Nome},
                                {"@Indirizzo", negozio.Indirizzo},
                                {"@Citta", negozio.Citta},
                                {"@CAP", negozio.CAP},
                                {"@Provincia", negozio.Provincia},
                                {"@Data_Apertura", negozio.Data_Apertura}
                            };
            return _db.UpdateDb(query, parameters);
        }

        public bool UpdateRecord(Entity entity)
        {
            var negozio = (Negozio)entity;
            var query = $"UPDATE {_tabella} SET Nome = @Nome, Indirizzo = @Indirizzo, Citta = @Citta, CAP = @CAP, Provincia = @Provincia, Data_Apertura = @Data_Apertura WHERE ID_Negozio = @ID_Negozio";
            var parameters = new Dictionary<string, object>
                            {
                                {"@Nome", negozio.Nome},
                                {"@Indirizzo", negozio.Indirizzo},
                                {"@Citta", negozio.Citta},
                                {"@CAP", negozio.CAP},
                                {"@Provincia", negozio.Provincia},
                                {"@Data_Apertura", negozio.Data_Apertura},
                            };
            return _db.UpdateDb(query, parameters);
        }

        public bool DeleteRecord(int id)
        {
            var query = $"DELETE FROM {_tabella} WHERE ID_Negozio = @ID_Negozio";
            var parameters = new Dictionary<string, object>
                            {
                                {"@ID_Negozio", id}
                            };
            return _db.UpdateDb(query, parameters);
        }

        public List<Negozio> GetRecords()
        {
            var query = $"SELECT * FROM {_tabella}";
            var dataTable = _db.ReadDb(query);
            var negozi = new List<Negozio>();
            if (dataTable == null)
                return negozi;

            foreach (var row in dataTable)
            {
                var negozio = new Negozio();
                negozio.TypeSort(row);
                negozi.Add(negozio);
            }
            return negozi;
        }

        public bool NegozioExists(int id)
        {
            var query = $"SELECT * FROM {_tabella} WHERE ID_Negozio = @ID_Negozio";
            var parameters = new Dictionary<string, object>
                            {
                                {"@ID_Negozio", id}
                            };
            var result = _db.ReadDb(query, parameters);
            return result is { Count: > 0 };
        }

        public Negozio? FindNegozio(int id)
        {
            var query = $"SELECT * FROM {_tabella} WHERE ID_Negozio = @ID_Negozio";
            var parameters = new Dictionary<string, object>
                            {
                                {"@ID_Negozio", id}
                            };
            var result = _db.ReadDb(query, parameters);
            if (result == null)
                return null;
            var negozio = new Negozio();
            negozio.TypeSort(result[0]);
            negozio.Id = Convert.ToInt32(result[0]["id_negozio"]);
            return negozio;
        }
    }
}
