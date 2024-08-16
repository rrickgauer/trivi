using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;

public class PlayGameAuthParms
{
    public required string GameId { get; set; }
    public required Guid PlayerId { get; set; }
}

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)] 
public class PlayGameAuth(IGameService gameService, IPlayerService playerService, RequestItems requestItems) : IAsyncPermissionsAuth<PlayGameAuthParms>
{
    private readonly IGameService _gameService = gameService;
    private readonly IPlayerService _playerService = playerService;
    private readonly RequestItems _requestItems = requestItems;

    public async Task<ServiceResponse> HasPermissionAsync(PlayGameAuthParms data)
    {
        try
        {
            ServiceResponse result = new();

            var game = await GetGameAsync(data);
            var player = await GetPlayerAsync(data);

            if (player.GameId != game.Id)
            {
                throw new NotFoundHttpResponseException();
            }

            _requestItems.Game = game;
            _requestItems.Player = player;

            return result;
        }
        catch(ServiceException ex)
        {
            return ex.Response;
        }
    }


    private async Task<ViewGame> GetGameAsync(PlayGameAuthParms data)
    {
        var getGame = await _gameService.GetGameAsync(data.GameId);

        return getGame.GetData();
    }

    private async Task<ViewPlayer> GetPlayerAsync(PlayGameAuthParms data)
    {
        var getPlayer = await _playerService.GetPlayerAsync(data.PlayerId);

        return getPlayer.GetData();
    }
}
