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
            { "@tipoOrdine", ((Ordine)entity).TipoOrdine.Replace("'", "''") },
            { "@totale", ((Ordine)entity).Totale },
            { "@stato", ((Ordine)entity).Stato.Replace("'", "''") },
            { "@id_Cliente", ((Ordine)entity).ID_Cliente },
            { "@id_LocazioneRitiro", ((Ordine)entity).ID_LocazioneRitiro }
            
        };
        const string query =
            "INSERT INTO Ordine (data, tipoOrdine, totale, stato, idCliente, idLocazioneRitiro) VALUES (@data, @tipoOrdine, @totale, @stato, @id_Cliente, @id_LocazioneRitiro)";

        return _db.UpdateDb(query, parameters);
    }

    public bool UpdateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@data", ((Ordine)entity).Data },
            { "@tipoOrdine", ((Ordine)entity).TipoOrdine.Replace("'", "''") },
            { "@totale", ((Ordine)entity).Totale },
            { "@stato", ((Ordine)entity).Stato.Replace("'", "''") },
            { "@id_Cliente", ((Ordine)entity).ID_Cliente },
            { "@id_LocazioneRitiro", ((Ordine)entity).ID_LocazioneRitiro }
        };
        const string query =
            "UPDATE Ordine SET data = @data, tipoOrdine = @tipoOrdine, totale = @totale, stato = @stato, idCliente = @id_Cliente, idLocazioneRitiro = @id_LocazioneRitiro, WHERE Id = @Id";

        return _db.UpdateDb(query, parameters);
    }

    public bool DeleteRecord(int recordId)
    {
        const string query = "DELETE FROM Ordine WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        return _db.UpdateDb(query, parameters);
    }

    public Entity? FindRecord(int recordId)
    {
        const string query = "SELECT * FROM Ordine WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        Entity entity = new Ordine();
        entity.TypeSort(singleResponse);
        return entity;
    }
}