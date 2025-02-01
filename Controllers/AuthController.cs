using Microsoft.AspNetCore.Mvc;
using MSSTU.DB.Utility;
using Newtonsoft.Json;
using SAC_eCommerce.Models.Classes;
using SAC_eCommerce.Models.Daos;

namespace SAC_eCommerce.Controllers;

public class AuthController : Controller
{
    private static int _loginAttempts;
    private static Utente? _loggedUser;

    private readonly ILogger<AuthController> _logger;
    private readonly DaoUtenti _daoUtenti;

    public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _daoUtenti = new DaoUtenti(configuration);
    }

    public IActionResult Login()
    {
        _logger.LogInformation($"Tentativo di accesso n. {_loginAttempts} - {DateTime.Now}");
        return View();
    }

    public IActionResult AutenticaUtente(Dictionary<string, string> parameters)
    {
        var email = parameters["email"];
        var password = parameters["password"];
        var rememberMe = parameters.ContainsKey("rememberMe") && parameters["rememberMe"] == "on";

        var userExists = _daoUtenti.UserExists(email, password);
        if (userExists)
        {
            var user = _daoUtenti.FindUser(email);
            _loggedUser = user;
            _loginAttempts = 0;

            HttpContext.Session.SetString("LoggedUser", JsonConvert.SerializeObject(user));
            if (rememberMe)
                HttpContext.Session.Set("SessionTimeout", BitConverter.GetBytes(TimeSpan.FromDays(30).Ticks));

            return RedirectToAction("Index", "Home");
        }

        _loginAttempts++;
        return RedirectToAction("Login");
    }

    [HttpPost]
    public IActionResult AggiungiUtente(Dictionary<string, string> parameters)
    {
        var email = parameters["email"];
        var password = parameters["password"];

        var userExists = _daoUtenti.UserExists(email, password);
        if (userExists)
        {
            return RedirectToAction("Login");
        }
        var user = new Utente();
        user.TypeSort(parameters);

        // Dati di default per evitare nulli
        user.Points = 0;
        user.Card_Number = Math.Abs(email.GetHashCode()).ToString();

        if (email.Contains("@dipendente.techretailspa.it"))
        {
            user.Role = "dipendente";
        } else if (email.Contains("@manager.techretailspa.it"))
        {
            user.Role = "manager";
        }
        else
        {
            user.Role = "user";
        }

        _daoUtenti.CreateRecord(user);
        _loggedUser = user;
        TempData["LoggedUser"] = JsonConvert.SerializeObject(user);
        // Chiama la action AutenticaUtente
        var parametersLogin = new Dictionary<string, string>
        {
            { "email", email },
            { "password", password }
        };
        return AutenticaUtente(parametersLogin);
    }

    public IActionResult Logout()
    {
        _logger.LogInformation($"Logout di {_loggedUser?.Email} - {DateTime.Now}");
        _loggedUser = null;
        _loginAttempts = 0;
        return View("../Home/Index");
    }

    public IActionResult Signup()
    {
        return View();
    }

}
