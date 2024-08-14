using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IPlayerService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class PlayerService(IPlayerRepository playerRepository, ITableMapperService tableMapperService) : IPlayerService
{
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly ITableMapperService _tableMapperService = tableMapperService;

    public async Task<ServiceResponse<List<ViewPlayer>>> GetPlayersInGameAsync(string gameId)
    {
        try
        {
            var table = await _playerRepository.SelectAllPlayersInGameAsync(gameId);
            return _tableMapperService.ToModels<ViewPlayer>(table);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceResponse<ViewPlayer>> GetPlayerAsync(Guid playerId)
    {
        try
        {
            ServiceResponse<ViewPlayer> result = new();

            var row = await _playerRepository.SelectPlayerAsync(playerId);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewPlayer>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceResponse<ViewPlayer>> GetPlayerAsync(string gameId, string nickname)
    {
        try
        {
            ServiceResponse<ViewPlayer> result = new();

            var row = await _playerRepository.SelectPlayerAsync(gameId, nickname);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewPlayer>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }


    public async Task<ServiceResponse<ViewPlayer>> CreatePlayerAsync(Player player)
    {
        try
        {
            await _playerRepository.InsertPlayerAsync(player);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }

        return await GetPlayerAsync(player.Id!.Value);
    }

}
