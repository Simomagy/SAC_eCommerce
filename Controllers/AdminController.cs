using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SAC_eCommerce.Models.Classes;
using SAC_eCommerce.Models.Daos;
using SAC_eCommerce.Models.ViewModels;

namespace SAC_eCommerce.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    private readonly DaoProdotti _daoProdotti;
    private readonly DaoOrdini _daoOrdini;
    private readonly DaoUtenti _daoUtenti;
    private readonly DaoInventario _daoInventario;

    public AdminController(IConfiguration configuration)
    {
        _daoProdotti = new DaoProdotti(configuration);
        _daoOrdini = new DaoOrdini(configuration);
        _daoUtenti = new DaoUtenti(configuration);
        _daoInventario = new DaoInventario(configuration);
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        var userJson = HttpContext.Session.GetString("LoggedUser");
        if (userJson != null)
        {
            var user = JsonConvert.DeserializeObject<Utente>(userJson);
            ViewBag.User = user;
            var ordini = _daoOrdini.GetRecords();
            ViewBag.Ordini = ordini;
        }

        return View();
    }

    [HttpGet("clienti")]
    public IActionResult Clienti()
    {
        var userJson = HttpContext.Session.GetString("LoggedUser");
        if (userJson != null)
        {
            var user = JsonConvert.DeserializeObject<Utente>(userJson);
            ViewBag.User = user;
            var clienti = _daoUtenti.GetRecords();
            return View(clienti);
        }

        return RedirectToAction("Login", "Auth");
    }

    [HttpPost("elimina-account")]
    public IActionResult EliminaAccount(int id)
    {
        _daoUtenti.DeleteRecord(id);
        return RedirectToAction("Clienti");
    }

    [HttpGet("prodotti")]
    public IActionResult Prodotti(int page = 1, int pageSize = 10)
    {
        var userJson = HttpContext.Session.GetString("LoggedUser");
        if (userJson == null) return RedirectToAction("Login", "Auth");
        var user = JsonConvert.DeserializeObject<Utente>(userJson);
        ViewBag.User = user;

        var prodotti = _daoProdotti.GetRecords().Cast<Prodotto>().ToList();
        var inventari = _daoInventario.GetRecords();

        var prodottiConDisponibilita = prodotti.Select(p => new ProdottoConDisponibilitaViewModel
        {
            Prodotto = p,
            DisponibilitaMagazzino =
                inventari.Where(i => i.Prodotto.Id == p.Id && i.Negozio.Id == 1).Sum(i => i.Quantita),
            DisponibilitaNegozi = inventari.Where(i => i.Prodotto.Id == p.Id && i.Negozio.Id != 1).Sum(i => i.Quantita)
        }).OrderBy(p => p.Prodotto.Nome).ToList();

        var pagedProdotti = prodottiConDisponibilita.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        ViewBag.TotalPages = (int)Math.Ceiling((double)prodottiConDisponibilita.Count / pageSize);
        ViewBag.CurrentPage = page;
        return View(pagedProdotti);
    }

    [HttpPost("elimina-prodotto/{id}")]
    public IActionResult EliminaProdotto(int id)
    {
        _daoProdotti.DeleteRecord(id);
        return RedirectToAction("Prodotti");
    }

    [HttpPost("aggiungi-prodotto")]
    public IActionResult AggiungiProdotto([FromBody] ProdottoConDisponibilitaViewModel nuovoProdotto)
    {
        var prodotto = new Prodotto
        {
            Nome = nuovoProdotto.Prodotto.Nome,
            Descrizione = nuovoProdotto.Prodotto.Descrizione,
            Prezzo = nuovoProdotto.Prodotto.Prezzo,
            Categoria = nuovoProdotto.Prodotto.Categoria,
            Quantita = nuovoProdotto.Prodotto.Quantita,
            Data_Inserimento = DateTime.Now
        };

        var result = _daoProdotti.CreateRecord(prodotto);

        if (result)
        {
            var inventarioMagazzino = new Inventario
            {
                Prodotto = prodotto,
                Negozio = new Negozio { Id = 1 }, // Assuming 1 is the ID for the warehouse
                Tipo_Locazione = "Magazzino",
                Quantita = nuovoProdotto.DisponibilitaMagazzino
            };

            var inventarioNegozi = new Inventario
            {
                Prodotto = prodotto,
                Negozio = new Negozio { Id = 2 }, // Assuming 2 is the ID for the store
                Tipo_Locazione = "Negozio",
                Quantita = nuovoProdotto.DisponibilitaNegozi
            };

            _daoInventario.CreateRecord(inventarioMagazzino);
            _daoInventario.CreateRecord(inventarioNegozi);

            return Ok();
        }

        return BadRequest("Errore durante l'aggiunta del prodotto.");
    }

    [HttpGet("ordini")]
    public IActionResult Ordini()
    {
        var userJson = HttpContext.Session.GetString("LoggedUser");
        if (userJson == null) return RedirectToAction("Login", "Auth");
        var user = JsonConvert.DeserializeObject<Utente>(userJson);
        ViewBag.User = user;

        var ordini = _daoOrdini.GetRecords();
        return View(ordini);
    }
}
