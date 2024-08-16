using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.Utility;

public static class ActionExecutingContextExtensions
{
    public static Guid GetCollectionIdFromUrl(this ActionExecutingContext context) => GetRequestRouteValue<Guid>(context, RouteKeys.CollectionId);
    public static QuestionId GetQuestionIdFromUrl(this ActionExecutingContext context) => GetRequestRouteValue<QuestionId>(context, RouteKeys.QuestionId);
    public static string GetAnswerIdFromUrl(this ActionExecutingContext context) => GetRequestRouteValue<string>(context, RouteKeys.AnswerId);
    public static string GetGameIdFromUrl(this ActionExecutingContext context) => GetRequestRouteValue<string>(context, RouteKeys.GameId);


    /// <summary>
    /// Get the specified request value with the matching key.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T GetRequestRouteValue<T>(ActionExecutingContext context, string key)
    {
        //return (T)context.ActionArguments[key]!;
        return (T)context.ActionArguments[key]!;
    }




    public static QuestionForm GetQuestionForm(this ActionExecutingContext context) =>  GetForm<QuestionForm>(context);
    public static PutAnswerRequest GetPutAnswerRequest(this ActionExecutingContext context) => GetForm<PutAnswerRequest>(context);
    public static NewGameForm GetNewGameForm(this ActionExecutingContext context) => GetForm<NewGameForm>(context);
    public static JoinGameForm GetJoinGameForm(this ActionExecutingContext context) => GetForm<JoinGameForm>(context);
    public static PlayGameGuiRequest GetPlayGameRequestForm(this ActionExecutingContext context) => GetForm<PlayGameGuiRequest>(context);
    public static ResponseForm GetResponseForm(this ActionExecutingContext context) => GetForm<ResponseForm>(context);


    public static T GetForm<T>(this ActionExecutingContext context)
    {
        return (T)context.ActionArguments.Values.First(a => a is T)!;
    }






    public static SessionManager GetSessionManager(this ActionExecutingContext context)
    {
        return new(context.HttpContext.Session);
    }

    public static Guid GetSessionClientId(this ActionExecutingContext context)
    {
        var sessionMgr = context.GetSessionManager();

        var clientId = sessionMgr.ClientId;

        if (!clientId.HasValue)
        {
            throw new ForbiddenHttpResponseException();
        }

        return clientId.Value;
    }


    public static Guid? GetSessionsClientIdNull(this ActionExecutingContext context)
    {
        var sessionMgr = context.GetSessionManager();
        return sessionMgr.ClientId;
    }

    public static bool TryGetSessionClientId(this ActionExecutingContext context, out Guid? clientId)
    {
        var sessionMgr = context.GetSessionManager();

        clientId = sessionMgr.ClientId;

        return clientId.HasValue;

    }


    public static bool NotAuthorized(this ActionExecutingContext context, ServiceResponse serviceResponse)
    {
        if (!serviceResponse.Successful)
        {
            context.ReturnBadServiceResponse(serviceResponse);
            return true;
        }

        return false;
    }


    public static void ReturnBadServiceResponse(this ActionExecutingContext context, ServiceResponse serviceResponse)
    {
        context.Result = new BadRequestObjectResult(serviceResponse);
    }
}
