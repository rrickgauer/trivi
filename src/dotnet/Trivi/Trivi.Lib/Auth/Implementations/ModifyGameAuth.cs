using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;


public class ModifyGameAuthParms
{
    public required string GameId { get; set; }
    public required Guid ClientId { get; set; }
}

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class ModifyGameAuth(IGameService gameService, RequestItems requestItems) : IAsyncPermissionsAuth<ModifyGameAuthParms>
{
    private readonly IGameService _gameService = gameService;
    private readonly RequestItems _requestItems = requestItems;

    public async Task<ServiceResponse> HasPermissionAsync(ModifyGameAuthParms data)
    {
        try
        {
            var game = await GetGameAsync(data);

            if(game.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }

            _requestItems.Game = game;

            return new();
        }
        catch(ServiceException ex)
        {
            return new(ex.Errors);
        }
    }


    private async Task<ViewGame> GetGameAsync(ModifyGameAuthParms data)
    {
        var getGame = await _gameService.GetGameAsync(data.GameId);
        
        getGame.ThrowIfError();

        return getGame.GetData();
    }
}
