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
public class ResponseService(IResponseRepository responseRepository, ITableMapperService tableMapperService) : IResponseService
{
    private readonly IResponseRepository _responseRepository = responseRepository;
    private readonly ITableMapperService _tableMapperService = tableMapperService;

    public async Task<ServiceDataResponse<List<ViewResponseShortAnswer>>> GetShortAnswerResponsesAync(string gameId, QuestionId questionId)
    {
        try
        {
            var table = await _responseRepository.SelectShortAnswerResponsesAsync(gameId, questionId);
            return _tableMapperService.ToModels<ViewResponseShortAnswer>(table);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }


    public async Task<ServiceDataResponse<ViewResponseShortAnswer>> GetShortAnswerResponseAsync(Guid responseId)
    {
        try
        {
            ServiceDataResponse<ViewResponseShortAnswer> result = new();

            var row = await _responseRepository.SelectShortAnswerResponseAsync(responseId);

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

    public async Task<ServiceDataResponse<ViewResponseShortAnswer>> GetShortAnswerResponseAsync(PlayerQuestionResponse responseData)
    {
        try
        {
            ServiceDataResponse<ViewResponseShortAnswer> result = new();

            var row = await _responseRepository.SelectShortAnswerResponseAsync(responseData);

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

    public async Task<ServiceDataResponse<ViewResponse>> GetResponseAsync(PlayerQuestionResponse responseData)
    {
        switch(responseData.QuestionId.QuestionType)
        {
            case QuestionType.ShortAnswer:
                var getShortAnswer = await GetShortAnswerResponseAsync(responseData);

                if (!getShortAnswer.Successful)
                {
                    return new(getShortAnswer.Errors);
                }

                return new(getShortAnswer.Data);

            default:
                throw new NotImplementedException();
        }
    }

    public async Task<ServiceDataResponse<ViewResponseShortAnswer>> CreateShortAnswerResponseAsync(ResponseShortAnswer response)
    {
        try
        {
            await _responseRepository.CreateResponseAsync(response);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }
        catch(Exception ex)
        {
            string message = ex.Message;

            int x = 10;

            throw;
        }

        if (response.Id is Guid responseId)
        {
            return await GetShortAnswerResponseAsync(responseId);
        }

        return new();
    }
}
