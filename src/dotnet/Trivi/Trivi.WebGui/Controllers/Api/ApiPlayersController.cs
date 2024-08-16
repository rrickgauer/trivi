using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Api;

[ApiController]
[Route("api/players")]
public class ApiPlayersController(IPlayerService playerService) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<ApiPlayersController>();

    private readonly IPlayerService _playerService = playerService;

    /// <summary>
    /// POST: /api/players
    /// </summary>
    /// <param name="playerForm"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName(nameof(PostPlayerAsync))]
    [ServiceFilter<JoinGameFilter>]
    public async Task<ActionResult<ServiceResponse<ViewPlayer>>> PostPlayerAsync([FromBody] JoinGameForm playerForm)
    {
        var player = Player.From(playerForm);

        var createPlayer = await _playerService.CreatePlayerAsync(player);

        return createPlayer.ToActionCreated(createPlayer?.Data!.UriApi);
    }
}
