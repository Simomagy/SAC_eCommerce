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
}
