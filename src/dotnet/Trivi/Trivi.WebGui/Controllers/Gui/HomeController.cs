using Microsoft.AspNetCore.Mvc;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("app")]
[ServiceFilter<LoginFirstRedirectFilter>]
public class HomeController : GuiController
{
    [HttpGet]
    [ActionName(nameof(HomePage))]
    public IActionResult HomePage()
    {
        return View(GuiPages.Home);
    }

}
