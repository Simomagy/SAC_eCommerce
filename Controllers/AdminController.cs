using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SAC_eCommerce.Models.Classes;
using SAC_eCommerce.Models.Daos;

namespace SAC_eCommerce.Controllers;

public class AdminController : Controller
{
    private readonly DaoProdotti _daoProdotti;
    private readonly DaoOrdini _daoOrdini;
    private readonly DaoUtenti _daoUtenti;

    public AdminController(IConfiguration configuration)
    {
        _daoProdotti = new DaoProdotti(configuration);
        _daoOrdini = new DaoOrdini(configuration);
        _daoUtenti = new DaoUtenti(configuration);
    }

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
}
