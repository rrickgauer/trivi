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
        return Ok("Home page");
    }

    [HttpGet("testing/{pageName}")]
    [ActionName(nameof(TestingPage))]
    public IActionResult TestingPage([FromRoute] string pageName)
    {
        return Ok($"{pageName}");
    }

}
