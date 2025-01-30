using Microsoft.AspNetCore.Mvc;
using SAC_eCommerce.Models.Classes;

namespace SAC_eCommerce.Views.Components;

public class NavbarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Utente user)
    {
        return View(user);
    }
}
