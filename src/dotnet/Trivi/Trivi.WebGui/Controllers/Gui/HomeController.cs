using Microsoft.AspNetCore.Mvc;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("app")]
public class HomeController : GuiController
{
    public IActionResult Index()
    {
        return Ok("Home page");
    }
}
