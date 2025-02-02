using MSSTU.DB.Utility;
using SAC_eCommerce.Models.Classes;

namespace SAC_eCommerce.Models.Daos;

public class DaoInventario
{
    #region Inizializzazione

    private readonly Database _db;
    private readonly string? _tabella;
    private readonly DaoProdotti _daoProdotti;
    private readonly DaoNegozi _daoNegozio;

    public DaoInventario(IConfiguration configuration)
    {
        _db = new Database(configuration["db_name"], configuration["server_db"]);
        _tabella = configuration["tables:inventari"];
        _daoProdotti = new DaoProdotti(configuration);
        _daoNegozio = new DaoNegozi(configuration);
    }

    #endregion

    public List<Inventario> GetRecords()
    {
        var query = $"SELECT * FROM {_tabella}";
        var response = _db.ReadDb(query);
        var inventarioList = new List<Inventario>();
        if (response == null)
            return inventarioList;
        foreach (var record in response)
        {
            var inventario = new Inventario();
            inventario.TypeSort(record);
            inventario.Id = Convert.ToInt32(record["id_inventario"]);
            inventario.Prodotto = _daoProdotti.FindProdotto(Convert.ToInt32(record["id_prodotto"]));
            inventario.Negozio = _daoNegozio.FindNegozio(Convert.ToInt32(record["id_locazione"]));
            inventarioList.Add(inventario);
        }

        return inventarioList;
    }
    public bool CreateRecord(Entity entity)
    {
        var inventario = (Inventario)entity;
        var parameters = new Dictionary<string, object>
        {
            // TODO: Togliere il commento una volta implementata la classe
            { "@prodotto", inventario.Prodotto.Id },
            //{ "@negozio", inventario.Negozio.Id },
            { "@tipo_Locazione", inventario.Tipo_Locazione.Replace("'", "''") },
            { "@quantita", inventario.Quantita }
            
        };
        var query =
            $"INSERT INTO {_tabella} (ID_Prodotto, ID_Negozio, Tipo_Locazione, Quantita) VALUES (@prodotto, @negozio, @tipo_locazione, @quantita)";

        return _db.UpdateDb(query, parameters);
    }

    public bool UpdateRecord(Entity entity)
    {
        var inventario = (Inventario)entity;
        var parameters = new Dictionary<string, object>
        {
            // TODO: Togliere il commento una volta implementata la classe
            { "@prodotto", inventario.Prodotto.Id },
            //{ "@negozio", inventario.Negozio.Id },
            { "@tipo_Locazione", inventario.Tipo_Locazione.Replace("'", "''") },
            { "@quantita", inventario.Quantita }
        };
         var query =
            $"UPDATE {_tabella} SET ID_Prodotto = @prodotto, ID_Negozio = @negozio, Tipo_Locazione = @Tipo_Locazione, Quantita = @quantita  WHERE ID_Inventario = @Id";

        return _db.UpdateDb(query, parameters);
    }

    public bool DeleteRecord(int recordId)
    {
        var query = $"DELETE FROM {_tabella} WHERE ID_Inventario = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        return _db.UpdateDb(query, parameters);
    }

    public Entity? FindRecord(int recordId)
    {
        var query = $"SELECT * FROM {_tabella} WHERE ID_Inventario = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        Entity entity = new Ordine();
        entity.TypeSort(singleResponse);
        return entity;
    }
}
