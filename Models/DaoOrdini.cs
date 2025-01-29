namespace SAC_eCommerce.Models;
using MSSTU.DB.Utility;
public class DaoOrdini 
{

    #region Inizializzazione

    private readonly Database _db;
    private readonly string _tabella;

    private DaoOrdini(IConfiguration configuration)
    {
        _db = new Database(configuration["db_name"], configuration["server_db"]);
        _tabella = configuration["tables.ordini"];
    }

    #endregion
    
    public bool CreateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@data", ((Ordine)entity).Data },
            { "@tipoOrdine", ((Ordine)entity).Tipo_Ordine.Replace("'", "''") },
            { "@totale", ((Ordine)entity).Totale },
            { "@stato", ((Ordine)entity).Stato.Replace("'", "''") },
            { "@id_Cliente", ((Ordine)entity).Cliente.Id },
            { "@id_LocazioneRitiro", ((Ordine)entity).ID_LocazioneRitiro }
            
        };
        const string query =
            "INSERT INTO {_tabella} (Data, Tipo_Ordine, Totale, Stato, Cliente, ID_LocazioneRitiro) VALUES (@data, @tipoOrdine, @totale, @stato, @id_Cliente, @id_LocazioneRitiro)";

        return _db.UpdateDb(query, parameters);
    }

    public bool UpdateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@data", ((Ordine)entity).Data },
            { "@tipoOrdine", ((Ordine)entity).Tipo_Ordine.Replace("'", "''") },
            { "@totale", ((Ordine)entity).Totale },
            { "@stato", ((Ordine)entity).Stato.Replace("'", "''") },
            { "@id_Cliente", ((Ordine)entity).Cliente.Id },
            { "@id_LocazioneRitiro", ((Ordine)entity).ID_LocazioneRitiro }
        };
        const string query =
            "UPDATE {_tabella} SET Data = @data, Tipo_Ordine = @tipoOrdine, Totale = @totale, Stato = @stato, Cliente = @id_Cliente, LocazioneRitiro = @id_LocazioneRitiro WHERE ID_Ordine = @Id";

        return _db.UpdateDb(query, parameters);
    }

    public bool DeleteRecord(int recordId)
    {
        const string query = "DELETE FROM {_tabella} WHERE ID_Ordine = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        return _db.UpdateDb(query, parameters);
    }

    public Entity? FindRecord(int recordId)
    {
        const string query = "SELECT * FROM {_tabella} WHERE ID_Ordine = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        Entity entity = new Ordine();
        entity.TypeSort(singleResponse);
        return entity;
    }
}