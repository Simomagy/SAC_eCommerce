using MSSTU.DB.Utility;
using SAC_eCommerce.Models.Classes;

namespace SAC_eCommerce.Models.Daos;

public class DaoOrdini 
{

    #region Inizializzazione

    private readonly Database _db;
    private readonly string? _tabella;
    private readonly string? _tabella2;

    public DaoOrdini(IConfiguration configuration)
    {
        _db = new Database(configuration["db_name"], configuration["server_db"]);
        _tabella = configuration["tables:ordini"];
        _tabella2 = configuration["tables:utenti"];
    }

    #endregion

    #region CRUD
    public bool CreateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@data", ((Ordine)entity).Data },
            { "@tipoOrdine", ((Ordine)entity).Tipo_Ordine.Replace("'", "''") },
            { "@totale", ((Ordine)entity).Totale },
            { "@stato", ((Ordine)entity).Stato.Replace("'", "''") },
            // TODO: Toglie il commento quando la classe Cliente è stata implementata
            // { "@id_Cliente", ((Ordine)entity).Cliente.Id },
            { "@id_LocazioneRitiro", ((Ordine)entity).ID_LocazioneRitiro }
            
        };
        string query =
            $"INSERT INTO {_tabella} (Data, Tipo_Ordine, Totale, Stato, Cliente, ID_LocazioneRitiro) VALUES (@data, @tipoOrdine, @totale, @stato, @id_Cliente, @id_LocazioneRitiro)";

        return _db.UpdateDb(query, parameters);
    }


    public List<Ordine> GetRecords()
    {
        var query = $"SELECT * FROM {_tabella} JOIN {_tabella2} ON {_tabella}.id_cliente = {_tabella2}.id_utente";
        List<Ordine> ordini = [];
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return ordini;

        foreach (var singleResponse in fullResponse)
        {
            var ordine = new Ordine();
            ordine.TypeSort(singleResponse);
            ordine.Id = Convert.ToInt32(singleResponse["id_ordine"]);
            ordine.Utente = new Utente();
            ordine.Utente.TypeSort(singleResponse);
            ordine.Utente.Id = Convert.ToInt32(singleResponse["id_utente"]);

            ordini.Add(ordine);
        }
        return ordini;
    }


    public bool UpdateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@data", ((Ordine)entity).Data },
            { "@tipoOrdine", ((Ordine)entity).Tipo_Ordine.Replace("'", "''") },
            { "@totale", ((Ordine)entity).Totale },
            { "@stato", ((Ordine)entity).Stato.Replace("'", "''") },
            { "@id_Cliente", ((Ordine)entity).Utente.Id },
            { "@id_LocazioneRitiro", ((Ordine)entity).ID_LocazioneRitiro }
        };
         string query =
            $"UPDATE {_tabella} SET Data = @data, Tipo_Ordine = @tipoOrdine, Totale = @totale, Stato = @stato, Cliente = @id_Cliente, LocazioneRitiro = @id_LocazioneRitiro WHERE ID_Ordine = @Id";

        return _db.UpdateDb(query, parameters);
    }

    public bool DeleteRecord(int recordId)
    {
        string query = $"DELETE FROM {_tabella} WHERE ID_Ordine = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        return _db.UpdateDb(query, parameters);
    }

    public Ordine? FindRecord(int recordId)
    {
        string query = $"SELECT * FROM {_tabella} JOIN {_tabella2} ON {_tabella}.id_cliente = {_tabella2}.id_utente WHERE ID_Ordine = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        var ordine = new Ordine();
        ordine.TypeSort(singleResponse);
        ordine.Id = Convert.ToInt32(singleResponse["id_ordine"]);
        ordine.Utente = new Utente();
        ordine.Utente.TypeSort(singleResponse);
        ordine.Utente.Id = Convert.ToInt32(singleResponse["id_utente"]);

        return ordine;
    }
    #endregion

    public List<Ordine>? FindOrdersByUser(string email)
    {
        string query = $"SELECT * " +
                       $"FROM {_tabella} JOIN {_tabella2} " +
                       $"ON {_tabella}.id_cliente = {_tabella2}.id_utente " +
                       $"WHERE {_tabella2}.email = '{email}'; ";

        List<Ordine> ordini = [];

        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return ordini;

        foreach (var singleResponse in fullResponse)
        {
            var ordine = new Ordine();
            ordine.TypeSort(singleResponse);
            ordine.Id = Convert.ToInt32(singleResponse["id_ordine"]);
            ordine.Utente = new Utente();
            ordine.Utente.TypeSort(singleResponse);
            ordine.Utente.Id = Convert.ToInt32(singleResponse["id_utente"]);

            ordini.Add(ordine);
        }
        return ordini;
    }
}
