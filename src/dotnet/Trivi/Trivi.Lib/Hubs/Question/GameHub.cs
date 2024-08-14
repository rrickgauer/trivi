using Microsoft.AspNetCore.SignalR;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Hubs.Question.EndpointParms;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Hubs.Question;

public interface IGameHubClientEvents
{
    public Task AdminUpdatePlayerQuestionResponses(AdminUpdatePlayerQuestionResponsesParms data);
    public Task ShowErrors(List<ErrorMessage> errors);
    public Task NavigateToPage(NavigateToPageParms data);
    public Task DisplayToast(DisplayToastParms data);
}

public class GameHub(IResponseService responseService, IGameHubConnectionService gameHubConnectionService) : Hub<IGameHubClientEvents>
{
    private readonly IResponseService _responseService = responseService;
    private readonly IGameHubConnectionService _gameHubConnectionService = gameHubConnectionService;

    public async Task PlayerConnectAsync(PlayerConnectParms data)
    {
        _gameHubConnectionService.StorePlayerConnectionId(data, Context.ConnectionId);
    }

    public async Task AdminConnectAsync(AdminJoinParms data)
    {
        _gameHubConnectionService.StoreAdminConnectionId(data.GameId, Context.ConnectionId);
        await UpdateAdminPlayerStatusesAsync(data.GameId, data.QuestionId);
    }

    

    public async Task AdminSendPlayerMessageAsync(AdminSendPlayerMessageParms data)
    {
        if (!_gameHubConnectionService.TryGetGameFromConnectionId(Context.ConnectionId, out var connections))
        {
            return;
        }

        if (connections!.Players.TryGetValue(data.PlayerId, out var playerConnectionId))
        {
            await Clients.Client(playerConnectionId).DisplayToast(new()
            {
                Message = data.Message,
            });
        }        
    }


    public async Task AdminSendAllPlayersMessageAsync(AdminSendAllPlayersMessageParms data)
    {
        if (!_gameHubConnectionService.TryGetGameFromConnectionId(Context.ConnectionId, out var connections))
        {
            return;
        }

        await Clients.Clients(connections!.PlayerConnectionIds).DisplayToast(new()
        {
            Message = data.Message,
        });
    }



    private async Task UpdateAdminPlayerStatusesAsync(string gameId, QuestionId questionId)
    {
        if (!_gameHubConnectionService.TryGetGameAdminConnectionId(gameId, out var adminConnectionId))
        {
            return;
        }

        var getResponses = await _responseService.GetPlayerQuestionResponsesAsync(gameId, questionId);

        if (!getResponses.Successful)
        {
            await Clients.Client(adminConnectionId).ShowErrors(getResponses.Errors.ToErrorMessages());
            return;
        }

        await Clients.Client(adminConnectionId).AdminUpdatePlayerQuestionResponses(new()
        {
            Responses = getResponses.Data ?? new(),
        });
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _gameHubConnectionService.RemoveConnection(Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);
    }



}
