using MSSTU.DB.Utility;
using SAC_eCommerce.Models.Classes;

namespace SAC_eCommerce.Models.Daos
{
    public class DaoFeedbacks
    {

        #region Inizializzazione

        private readonly Database _db;
        private readonly string? _tabellaFeedbacks;
        private readonly string? _tabellaOrdini;
        private readonly string? _tabellaUtenti;

        public DaoFeedbacks(IConfiguration configuration)
        {
            _db = new Database(configuration["db_name"], configuration["server_db"]);
            _tabellaFeedbacks = configuration["tables:feedbacks"];
            _tabellaOrdini = configuration["tables:ordini"];
            _tabellaUtenti = configuration["tables:utenti"];
        }

        #endregion

        #region CRUD
        public bool CreateRecord(Entity entity)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "@data", ((Feedback)entity).Data_Inserimento },
                    { "@testo", ((Feedback)entity).Testo.Replace("'", "''") },
                    { "@valutazione", ((Feedback)entity).Valutazione },
                    { "@id_Ordine", ((Feedback)entity).Ordine.Id },
                    { "@id_Utente", ((Feedback)entity).Utente.Id }
                };
            string query =
                $"INSERT INTO {_tabellaFeedbacks} (Data_Inserimento, Testo, Valutazione, ID_Ordine, ID_Utente) VALUES (@data, @testo, @valutazione, @id_Ordine, @id_Utente)";

            return _db.UpdateDb(query, parameters);
        }

        public List<Feedback> GetRecords()
        {
            var query = $"SELECT * FROM {_tabellaFeedbacks} JOIN {_tabellaOrdini} ON {_tabellaFeedbacks}.id_ordine = {_tabellaOrdini}.id_ordine JOIN {_tabellaUtenti} ON {_tabellaFeedbacks}.id_utente = {_tabellaUtenti}.id_utente";
            List<Feedback> feedbacks = new List<Feedback>();
            var fullResponse = _db.ReadDb(query);
            if (fullResponse == null)
                return feedbacks;

            foreach (var singleResponse in fullResponse)
            {
                var feedback = new Feedback();
                feedback.TypeSort(singleResponse);
                feedback.Id = Convert.ToInt32(singleResponse["id_feedback"]);
                feedback.Ordine = new Ordine();
                feedback.Ordine.TypeSort(singleResponse);
                feedback.Ordine.Id = Convert.ToInt32(singleResponse["id_ordine"]);
                feedback.Utente = new Utente();
                feedback.Utente.TypeSort(singleResponse);
                feedback.Utente.Id = Convert.ToInt32(singleResponse["id_utente"]);

                feedbacks.Add(feedback);
            }
            return feedbacks;
        }

        public bool UpdateRecord(Entity entity)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "@data", ((Feedback)entity).Data_Inserimento },
                    { "@testo", ((Feedback)entity).Testo.Replace("'", "''") },
                    { "@valutazione", ((Feedback)entity).Valutazione },
                    { "@id_Ordine", ((Feedback)entity).Ordine.Id },
                    { "@id_Utente", ((Feedback)entity).Utente.Id }
                };
            string query =
               $"UPDATE {_tabellaFeedbacks} SET Data_Inserimento = @data, Testo = @testo, Valutazione = @valutazione, ID_Ordine = @id_Ordine, ID_Utente = @id_Utente WHERE ID_Feedback = @Id";

            return _db.UpdateDb(query, parameters);
        }

        public bool DeleteRecord(int recordId)
        {
            string query = $"DELETE FROM {_tabellaFeedbacks} WHERE ID_Feedback = @Id";
            var parameters = new Dictionary<string, object> { { "@Id", recordId } };
            return _db.UpdateDb(query, parameters);
        }

        public Feedback? FindRecord(int recordId)
        {
            string query = $"SELECT * FROM {_tabellaFeedbacks} JOIN {_tabellaOrdini} ON {_tabellaFeedbacks}.id_ordine = {_tabellaOrdini}.id_ordine JOIN {_tabellaUtenti} ON {_tabellaFeedbacks}.id_utente = {_tabellaUtenti}.id_utente WHERE ID_Feedback = @Id";
            var parameters = new Dictionary<string, object> { { "@Id", recordId } };
            var singleResponse = _db.ReadOneDb(query, parameters);
            if (singleResponse == null)
                return null;
            var feedback = new Feedback();
            feedback.TypeSort(singleResponse);
            feedback.Id = Convert.ToInt32(singleResponse["id_feedback"]);
            feedback.Ordine = new Ordine();
            feedback.Ordine.TypeSort(singleResponse);
            feedback.Ordine.Id = Convert.ToInt32(singleResponse["id_ordine"]);
            feedback.Utente = new Utente();
            feedback.Utente.TypeSort(singleResponse);
            feedback.Utente.Id = Convert.ToInt32(singleResponse["id_utente"]);

            return feedback;
        }
        #endregion
    }
}
