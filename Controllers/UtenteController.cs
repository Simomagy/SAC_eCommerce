using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SAC_eCommerce.Models.Classes;
using SAC_eCommerce.Models.Daos;

namespace SAC_eCommerce.Controllers;

public class UtenteController : Controller
{
    private readonly DaoProdotti _daoProdotti;

    public UtenteController(IConfiguration configuration)
    {
        _daoProdotti = new DaoProdotti(configuration);
    }

    public IActionResult Profilo()
    {
        if (TempData["LoggedUser"] != null)
        {
            var userJson = TempData["LoggedUser"].ToString();
            var user = JsonConvert.DeserializeObject<Utente>(userJson);
            return View(user);
        }

        return RedirectToAction("Login", "Auth");
    }

    public IActionResult Prodotti()
    {
        var prodotti = _daoProdotti.GetRecords();
        Utente user = null;

        if (TempData["LoggedUser"] != null)
        {
            var userJson = TempData["LoggedUser"].ToString();
            user = JsonConvert.DeserializeObject<Utente>(userJson);
        }

        ViewBag.User = user;
        return View(prodotti);
    }
}
