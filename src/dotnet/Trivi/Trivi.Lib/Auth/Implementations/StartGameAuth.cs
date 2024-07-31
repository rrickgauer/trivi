using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;

public class StartGameAuthParms
{
    public required string GameId { get; set; }
    public required Guid ClientId { get; set; }
}

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class StartGameAuth(IGameService gameService) : IAsyncPermissionsAuth<StartGameAuthParms>
{
    private readonly IGameService _gameService = gameService;

    public async Task<ServiceResponse> HasPermissionAsync(StartGameAuthParms data)
    {
        try
        {
            var game = await GetGameAsync(data);

            if (game.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }

            if (game.Status != GameStatus.Open)
            {
                return new(ErrorCode.GamesStartNonOpenGame);
            }

            return new();
        }
        catch(ServiceException ex)
        {
            return ex.Response;
        }
    }


    private async Task<ViewGame> GetGameAsync(StartGameAuthParms data)
    {
        var getgame = await _gameService.GetGameAsync(data.GameId);

        getgame.ThrowIfError();

        return getgame.GetData();
    }
}
