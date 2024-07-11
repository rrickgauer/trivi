using Microsoft.AspNetCore.Mvc;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("")]
public class LandingController : Controller
{
    [HttpGet]
    public IActionResult GetLandingPage()
    {

        return View("Views/Pages/Landing/LandingPage.cshtml");
    }

}
