using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SAC_eCommerce.Models.Classes;
using SAC_eCommerce.Models.Daos;

namespace SAC_eCommerce.Controllers;

public class UtenteController : Controller
{
    private readonly DaoProdotti _daoProdotti;
    private readonly DaoOrdini _daoOrdini;
    private readonly DaoUtenti _daoUtenti;

    public UtenteController(IConfiguration configuration)
    {
        _daoProdotti = new DaoProdotti(configuration);
        _daoOrdini = new DaoOrdini(configuration);
        _daoUtenti = new DaoUtenti(configuration);
    }

    public IActionResult Profilo()
    {
        var userJson = HttpContext.Session.GetString("LoggedUser");
        if (userJson != null)
        {
            var ordini = _daoOrdini.FindOrdersByUser(JsonConvert.DeserializeObject<Utente>(userJson).Email);
            ViewBag.Ordini = ordini;
            var user = JsonConvert.DeserializeObject<Utente>(userJson);
            return View(user);
        }

        return RedirectToAction("Login", "Auth");
    }

    public IActionResult Prodotti()
    {
        var entities = _daoProdotti.GetRecords();
        var prodotti = entities.Cast<Prodotto>().ToList();

        var userJson = HttpContext.Session.GetString("LoggedUser");
        if (userJson == null) return View(prodotti);

        var user = JsonConvert.DeserializeObject<Utente>(userJson);
        ViewBag.User = user;

        return View(prodotti);
    }

    public IActionResult Impostazioni()
    {
        var userJson = HttpContext.Session.GetString("LoggedUser");
        if (userJson != null)
        {
            var user = JsonConvert.DeserializeObject<Utente>(userJson);
            return View(user);
        }

        return RedirectToAction("Login", "Auth");
    }

    public IActionResult ModificaUtente(Dictionary<string, string> parameters)
    {
        var userJson = HttpContext.Session.GetString("LoggedUser");
        if (userJson != null)
        {
            var user = JsonConvert.DeserializeObject<Utente>(userJson);
            user.Nome = parameters["Nome"];
            user.Cognome = parameters["Cognome"];
            user.Email = parameters["Email"];
            user.Password = parameters["Password"];
            _daoUtenti.UpdateRecord(user);
            HttpContext.Session.SetString("LoggedUser", JsonConvert.SerializeObject(user));
            return RedirectToAction("Profilo");
        }

        return RedirectToAction("Login", "Auth");
    }

    public IActionResult Compra(int id)
    {
        var userJson = HttpContext.Session.GetString("LoggedUser");
        if (userJson != null)
        {
            var user = JsonConvert.DeserializeObject<Utente>(userJson);
            var prodotto = _daoProdotti.FindProdotto(id);
            var ordine = new Ordine
            {
                Data = DateTime.Now,
                Tipo_Ordine = "Acquisto Online",
                Stato = "In Elaborazione",
                Totale = prodotto.Prezzo,
                Utente = user,
                Prodotti = new List<Prodotto> { prodotto }
            };
            _daoOrdini.CreateRecord(ordine);
            return RedirectToAction("Prodotti");

        }
        return RedirectToAction("Login", "Auth");
    }
}
