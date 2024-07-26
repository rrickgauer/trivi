using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;


public class JoinGameAuthParms
{
    public required JoinGameForm JoinGameForm { get; set; }
}

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class JoinGameAuth(IGameService gameService, IPlayerService playerService, RequestItems requestItems) : IAsyncPermissionsAuth<JoinGameAuthParms>
{
    private readonly IGameService _gameService = gameService;
    private readonly IPlayerService _playerService = playerService;
    private readonly RequestItems _requestItems = requestItems;

    public async Task<ServiceResponse> HasPermissionAsync(JoinGameAuthParms data)
    {
        try
        {
            ServiceResponse result = new();

            var game = await CheckGameAsync(data, result);

            if (!result.Successful)
            {
                return result;
            }

            await CheckNicknameAsync(data, result);

            if (game != null)
            {
                _requestItems.Game = game;
            }

            return result;
        }
        catch(ServiceException ex)
        {
            return new(ex.Errors);
        }
    }


    private async Task<ViewGame?> CheckGameAsync(JoinGameAuthParms data, ServiceResponse response)
    {
        var getGame = await _gameService.GetGameAsync(data.JoinGameForm.GameId);

        getGame.ThrowIfError();

        if (getGame.Data is not ViewGame game)
        {
            response.AddError(ErrorCode.JoinGameNotFound);
            return null;
        }

        if (!game.Status.CanJoinGame())
        {
            response.AddError(ErrorCode.JoinGameAlreadyFinished);
            return null;
        }

        return game;
    }

    private async Task CheckNicknameAsync(JoinGameAuthParms data, ServiceResponse response)
    {
        var getPlayer = await _playerService.GetPlayerAsync(data.JoinGameForm.GameId, data.JoinGameForm.Nickname);

        getPlayer.ThrowIfError();

        if (getPlayer.Data is not null)
        {
            response.AddError(ErrorCode.JoinGameNicknameAlreadyTaken);
        }

        if (data.JoinGameForm.Nickname.Length < PlayerConstants.MinNicknameLength || data.JoinGameForm.Nickname.Length > PlayerConstants.MaxNicknameLength)
        {
            response.AddError(ErrorCode.JoinGameInvalidNicknameLength);
        }
    }
}
