using MSSTU.DB.Utility;
using Newtonsoft.Json;
using SAC_eCommerce.Models.Classes;
namespace SAC_eCommerce.Models.Daos;

internal class DaoOrdini
{
    #region Inizializzazione

    private readonly Database _db;
    private readonly string? _tabellaOrdini;
    private readonly string? _tabellaUtenti;
    private readonly string? _tabellaProdotti;

    public DaoOrdini(IConfiguration configuration)
    {
        _db = new Database(configuration["db_name"], configuration["server_db"]);
        _tabellaOrdini = configuration["tables:ordini"];
        _tabellaUtenti = configuration["tables:utenti"];
        _tabellaProdotti = configuration["tables:prodotti"];
    }

    #endregion

    #region CRUD

    private List<Prodotto> GetDetailedProdottiList(string prodottiJson)
    {
        var prodottiList = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(prodottiJson);
        var detailedProdottiList = new List<Prodotto>();

        foreach (var prodottoDict in prodottiList)
        {
            var idProdotto = prodottoDict["Id"];
            var quantita = prodottoDict["Quantita"];

            var queryProdotto = $"SELECT * FROM {_tabellaProdotti} WHERE id_prodotto = @IdProdotto";
            var parametersProdotto = new Dictionary<string, object> { { "@IdProdotto", idProdotto } };
            var singleResponseProdotto = _db.ReadOneDb(queryProdotto, parametersProdotto);

            if (singleResponseProdotto != null)
            {
                var detailedProdotto = new Prodotto();
                detailedProdotto.TypeSort(singleResponseProdotto);
                detailedProdotto.Id = Convert.ToInt32(singleResponseProdotto["id_prodotto"]);
                detailedProdotto.Quantita = quantita;

                detailedProdottiList.Add(detailedProdotto);
            }
        }

        return detailedProdottiList;
    }

    public bool CreateRecord(Entity entity)
    {
        var ordine = (Ordine)entity;
        ordine.Totale = Math.Round(ordine.Prodotti.Sum(p => p.Prezzo * p.Quantita), 2);

        var parameters = new Dictionary<string, object>
        {
            { "@data", ordine.Data },
            { "@tipoOrdine", ordine.Tipo_Ordine.Replace("'", "''") },
            { "@totale", ordine.Totale },
            { "@stato", ordine.Stato.Replace("'", "''") },
            { "@prodotti", JsonConvert.SerializeObject(ordine.Prodotti).Replace("'", "''") },
            { "@id_Cliente", ordine.Utente.Id }
        };
        var query =
            $"INSERT INTO {_tabellaOrdini} (Data, Tipo_Ordine, Totale, Stato, Prodotti, Cliente) VALUES (@data, @tipoOrdine, @totale, @stato, @prodotti, @id_Cliente)";

        return _db.UpdateDb(query, parameters);
    }

    public List<Ordine> GetRecords()
    {
        var query =
            $"SELECT * FROM {_tabellaOrdini} JOIN {_tabellaUtenti} ON {_tabellaOrdini}.id_cliente = {_tabellaUtenti}.id_utente";
        var ordini = new List<Ordine>();
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return ordini;

        foreach (var singleResponse in fullResponse)
        {
            var ordine = new Ordine();
            ordine.Data = Convert.ToDateTime(singleResponse["data"]);
            ordine.Tipo_Ordine = singleResponse["tipo_ordine"].ToString();
            ordine.Totale = Convert.ToDouble(singleResponse["totale"]);
            ordine.Stato = singleResponse["stato"].ToString();
            ordine.Id = Convert.ToInt32(singleResponse["id_ordine"]);
            ordine.Utente = new Utente();
            ordine.Utente.Nome = singleResponse["nome"].ToString();
            ordine.Utente.Cognome = singleResponse["cognome"].ToString();
            ordine.Utente.Email = singleResponse["email"].ToString();
            ordine.Utente.Points = Convert.ToInt32(singleResponse["points"]);
            ordine.Utente.Card_Number = singleResponse["card_number"].ToString();
            ordine.Utente.Role = singleResponse["role"].ToString();
            ordine.Utente.Id = Convert.ToInt32(singleResponse["id_utente"]);

            var prodottiJson = singleResponse["prodotti"].ToString();
            ordine.Prodotti = GetDetailedProdottiList(prodottiJson);
            // Recalculate total
            var recalculatedTotal = ordine.Prodotti.Sum(p => p.Prezzo * p.Quantita);
            if (ordine.Totale != recalculatedTotal)
            {
                ordine.Totale = recalculatedTotal;
                UpdateRecord(ordine);
            }

            ordini.Add(ordine);
        }

        return ordini;
    }

    public bool UpdateRecord(Entity entity)
    {
        var ordine = (Ordine)entity;
        ordine.Totale = Math.Round(ordine.Prodotti.Sum(p => p.Prezzo * p.Quantita), 2);

        var parameters = new Dictionary<string, object>
        {
            { "@data", ordine.Data },
            { "@tipoOrdine", ordine.Tipo_Ordine.Replace("'", "''") },
            { "@totale", ordine.Totale },
            { "@stato", ordine.Stato.Replace("'", "''") },
            {
                "@prodotti",
                JsonConvert.SerializeObject(ordine.Prodotti.Select(p => new { p.Id, p.Quantita }).ToList())
                    .Replace("'", "''")
            },
            { "@id_Cliente", ordine.Utente.Id },
            { "@Id", ordine.Id }
        };
        var query =
            $"UPDATE {_tabellaOrdini} SET Data = @data, Tipo_Ordine = @tipoOrdine, Totale = @totale, Stato = @stato, Prodotti = @prodotti, ID_Cliente = @id_Cliente WHERE ID_Ordine = @Id";
        return _db.UpdateDb(query, parameters);
    }

    public bool DeleteRecord(int recordId)
    {
        var query = $"DELETE FROM {_tabellaOrdini} WHERE ID_Ordine = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        return _db.UpdateDb(query, parameters);
    }

    public Ordine? FindRecord(int recordId)
    {
        var query =
            $"SELECT * FROM {_tabellaOrdini} JOIN {_tabellaUtenti} ON {_tabellaOrdini}.id_cliente = {_tabellaUtenti}.id_utente WHERE ID_Ordine = @Id";
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

        var prodottiJson = singleResponse["prodotti"].ToString();
        ordine.Prodotti = GetDetailedProdottiList(prodottiJson);

        // Recalculate total
        var recalculatedTotal = Math.Round(ordine.Prodotti.Sum(p => p.Prezzo * p.Quantita), 2);
        if (ordine.Totale != recalculatedTotal)
        {
            ordine.Totale = recalculatedTotal;
            UpdateRecord(ordine);
        }

        return ordine;
    }

    public List<Ordine>? FindOrdersByUser(string email)
    {
        var query = $"SELECT * " +
                    $"FROM {_tabellaOrdini} JOIN {_tabellaUtenti} " +
                    $"ON {_tabellaOrdini}.id_cliente = {_tabellaUtenti}.id_utente " +
                    $"WHERE {_tabellaUtenti}.email = @Email";

        var ordini = new List<Ordine>();
        var parameters = new Dictionary<string, object> { { "@Email", email } };

        var fullResponse = _db.ReadDb(query, parameters);
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

            var prodottiJson = singleResponse["prodotti"].ToString();
            ordine.Prodotti = GetDetailedProdottiList(prodottiJson);

            ordini.Add(ordine);
        }

        return ordini;
    }

    #endregion
}
