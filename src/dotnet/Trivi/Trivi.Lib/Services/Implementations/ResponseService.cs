using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IResponseService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class ResponseService(IResponseRepository responseRepository, ITableMapperService tableMapperService, IAnswerService answerService) : IResponseService
{
    #region - Private members -

    private readonly IResponseRepository _responseRepository = responseRepository;
    private readonly ITableMapperService _tableMapperService = tableMapperService;
    private readonly IAnswerService _answerService = answerService;

    #endregion

    #region - Get response -

    public async Task<ServiceDataResponse<ViewResponse>> GetResponseAsync(PlayerQuestionResponse responseData)
    {
        return responseData.QuestionId.QuestionType switch
        {
            QuestionType.ShortAnswer => await GetResponseAsync(GetShortAnswerAsync, responseData),
            QuestionType.TrueFalse => await GetResponseAsync(GetTrueFalseAsync, responseData),
            QuestionType.MultipleChoice => await GetResponseAsync(GetMultipleChoiceAsync, responseData),
            _ => throw new NotImplementedException(),
        };
    }

    private delegate Task<ServiceDataResponse<TView>> GetFunctionCallback<TView>(PlayerQuestionResponse parms);

    private static async Task<ServiceDataResponse<ViewResponse>> GetResponseAsync<TResponse>(GetFunctionCallback<TResponse> callback, PlayerQuestionResponse callbackParms) where TResponse : ViewResponse
    {
        var serviceResult = await callback(callbackParms);

        if (!serviceResult.Successful)
        {
            return new(serviceResult.Errors);
        }

        return new(serviceResult.Data);
    }

    #endregion

    #region - Get Short Answers -

    public async Task<ServiceDataResponse<List<ViewResponseShortAnswer>>> GetShortAnswersAsync(string gameId, QuestionId questionId)
    {
        try
        {
            var table = await _responseRepository.SelectShortAnswersAsync(gameId, questionId);
            return _tableMapperService.ToModels<ViewResponseShortAnswer>(table);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }


    public async Task<ServiceDataResponse<ViewResponseShortAnswer>> GetShortAnswerAsync(Guid responseId)
    {
        try
        {
            ServiceDataResponse<ViewResponseShortAnswer> result = new();

            var row = await _responseRepository.SelectShortAnswerAsync(responseId);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewResponseShortAnswer>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewResponseShortAnswer>> GetShortAnswerAsync(PlayerQuestionResponse responseData)
    {
        try
        {
            ServiceDataResponse<ViewResponseShortAnswer> result = new();

            var row = await _responseRepository.SelectShortAnswerAsync(responseData);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewResponseShortAnswer>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    #endregion

    #region - Get True False -

    public async Task<ServiceDataResponse<ViewResponseTrueFalse>> GetTrueFalseAsync(Guid responseId)
    {
        try
        {
            ServiceDataResponse<ViewResponseTrueFalse> result = new();

            var row = await _responseRepository.SelectTrueFalseAsync(responseId);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewResponseTrueFalse>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewResponseTrueFalse>> GetTrueFalseAsync(PlayerQuestionResponse responseData)
    {
        try
        {
            ServiceDataResponse<ViewResponseTrueFalse> result = new();

            var row = await _responseRepository.SelectTrueFalseAsync(responseData);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewResponseTrueFalse>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    #endregion

    #region - Get Multiple Choice -

    public async Task<ServiceDataResponse<ViewResponseMultipleChoice>> GetMultipleChoiceAsync(Guid responseId)
    {
        try
        {
            ServiceDataResponse<ViewResponseMultipleChoice> result = new();

            var row = await _responseRepository.SelectMultipleChoiceAsync(responseId);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewResponseMultipleChoice>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewResponseMultipleChoice>> GetMultipleChoiceAsync(PlayerQuestionResponse responseData)
    {
        try
        {
            ServiceDataResponse<ViewResponseMultipleChoice> result = new();

            var row = await _responseRepository.SelectMultipleChoiceAsync(responseData);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewResponseMultipleChoice>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }


    #endregion

    #region - Create response -

    public async Task<ServiceDataResponse<ViewResponseShortAnswer>> CreateShortAnswerResponseAsync(ResponseShortAnswer response)
    {
        try
        {
            await _responseRepository.CreateResponseAsync(response);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }

        if (response.Id is Guid responseId)
        {
            return await GetShortAnswerAsync(responseId);
        }

        return new();
    }

    public async Task<ServiceDataResponse<ViewResponseTrueFalse>> CreateTrueFalseResponseAsync(ResponseTrueFalse response)
    {
        try
        {
            await _responseRepository.CreateResponseAsync(response);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }

        if (response.Id is Guid responseId)
        {
            return await GetTrueFalseAsync(responseId);
        }

        return new();
    }

    public async Task<ServiceDataResponse<ViewResponseMultipleChoice>> CreateMultipleChoiceResponseAsync(ResponseMultipleChoice response)
    {
        var getValidation = await ValidateNewMultipleChoiceAsync(response);

        if (!getValidation.Successful)
        {
            return new(getValidation.Errors);
        }


        try
        {
            await _responseRepository.CreateResponseAsync(response);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }

        if (response.Id is Guid responseId)
        {
            return await GetMultipleChoiceAsync(responseId);
        }

        return new();
    }

    private async Task<ServiceResponse> ValidateNewMultipleChoiceAsync(ResponseMultipleChoice response)
    {

        var getAnswers = await _answerService.GetAnswersAsync(response.QuestionId!);

        if (!getAnswers.Successful)
        {
            return new(getAnswers.Errors);
        }

        var answerIds = getAnswers.Data?.Select(a => a.Id).ToList() ?? new();

        
        if (!answerIds.Contains(response.AnswerGiven))
        {
            return new(ErrorCode.ResponsesInvalidMultipleChoiceAnswerId);
        }

        return new();
    }


    #endregion
}
