using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Api;

[ApiController]
[Route("api/players")]
public class ApiPlayersController(IPlayerService playerService) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix(nameof(ApiPlayersController));

    private readonly IPlayerService _playerService = playerService;

    [HttpPost]
    [ActionName(nameof(PostPlayerAsync))]
    [ServiceFilter<JoinGameFilter>]
    public async Task<IActionResult> PostPlayerAsync([FromBody] JoinGameForm playerForm)
    {
        var player = Player.From(playerForm);

        var createPlayer = await _playerService.CreatePlayerAsync(player);

        if (!createPlayer.Successful)
        {
            return BadRequest(createPlayer);
        }
        
        return Created(createPlayer?.Data!.UriApi, createPlayer);
    }
}
