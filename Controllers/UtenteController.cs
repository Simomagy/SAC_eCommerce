using Microsoft.AspNetCore.Mvc;

namespace SAC_eCommerce.Controllers;

public class UtenteController : Controller
{
    // GET
    public IActionResult Profilo()
    {
        return View();
    }
}
