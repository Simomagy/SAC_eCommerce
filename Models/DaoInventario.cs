namespace SAC_eCommerce.Models;
using MSSTU.DB.Utility;
public class DaoInventario
{
    #region Inizializzazione

    private readonly Database _db;
    private readonly string? _tabella;

    private DaoInventario(IConfiguration configuration)
    {
        _db = new Database(configuration["db_name"], configuration["server_db"]);
        _tabella = configuration["tables.inventario"];
    }

    #endregion
    
    
    public bool CreateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            //{ "@prodotto", ((Inventario)entity.Prodotto.Id },
            //{ "@negozio", ((Inventario)entity).Negozio.Id },
            { "@tipo_Locazione", ((Inventario)entity).Tipo_Locazione.Replace("'", "''") },
            { "@quantita", ((Inventario)entity).Quantita }
            
        };
        string query =
            $"INSERT INTO {_tabella} (Prodotto, Negozio, Tipo_Locazione, Quantita) VALUES (@prodotto, @negozio, @tipo_locazione, @quantita)";

        return _db.UpdateDb(query, parameters);
    }

    public bool UpdateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            //{ "@prodotto", ((Inventario)entity.Prodotto.Id },
            //{ "@negozio", ((Inventario)entity).Negozio.Id },
            { "@tipo_Locazione", ((Inventario)entity).Tipo_Locazione.Replace("'", "''") },
            { "@quantita", ((Inventario)entity).Quantita }
        };
         string query =
            $"UPDATE {_tabella} SET Prodotto = @prodotto, Negozio = @negozio, Tipo_Locazione = @Tipo_Locazione, Quantita = @quantita,  WHERE ID_Inventario = @Id";

        return _db.UpdateDb(query, parameters);
    }

    public bool DeleteRecord(int recordId)
    {
        string query = $"DELETE FROM {_tabella} WHERE ID_Inventario = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        return _db.UpdateDb(query, parameters);
    }

    public Entity? FindRecord(int recordId)
    {
        string query = $"SELECT * FROM {_tabella} WHERE ID_Inventario = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        Entity entity = new Ordine();
        entity.TypeSort(singleResponse);
        return entity;
    }
}