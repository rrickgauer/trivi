using System.Collections.Concurrent;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Hubs.Lobby;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IGamePoolService>(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class GamePoolService(IGameService gameService, IPlayerService playerService) : IGamePoolService
{
    private static readonly ConcurrentDictionary<string, ConnectionsPool> _gamesPool = new();

    private readonly IGameService _gameService = gameService;
    private readonly IPlayerService _playerService = playerService;

    public ConnectionsPool GetConnectionPool(string gameId)
    {
        if (!_gamesPool.TryGetValue(gameId, out ConnectionsPool? value))
        {
            value = new(gameId);
            _gamesPool[gameId] = value;
        }

        return value;
    }

    public async Task<ServiceDataResponse<ConnectionsPool>> AddPlayerToGameAsync(string gameId, Guid playerId)
    {
        var gamepool = GetConnectionPool(gameId);

        if (gamepool.ContainsPlayer(playerId))
        {
            return gamepool;
        }

        var getPlayer = await _playerService.GetPlayerAsync(playerId);

        if (!getPlayer.Successful)
        {
            return new(getPlayer.Errors);
        }

        ViewPlayer player = getPlayer.Data!;

        gamepool.Players.Add(playerId, player);

        return gamepool;

    }
}

