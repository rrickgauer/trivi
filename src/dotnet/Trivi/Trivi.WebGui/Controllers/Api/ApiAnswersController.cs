using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Api;

[ApiController]
[Route("api/questions/{questionId:multipleChoiceQuestion}/answers")]
[ServiceFilter<InternalApiAuthFilter>]
public class ApiAnswersController(IAnswerService answerService, RequestItems requestItems) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<ApiAnswersController>();

    private readonly IAnswerService _answerService = answerService;
    private readonly RequestItems _requestItems = requestItems;

    /// <summary>
    /// GET: /api/questions/:questionId/answers
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    [HttpGet]
    [ActionName(nameof(GetAnswersAsync))]
    [ServiceFilter<GetQuestionFilter>]
    public async Task<ActionResult<ServiceResponse<List<ViewAnswer>>>> GetAnswersAsync([FromRoute] QuestionId questionId)
    {
        var getAnswers = await _answerService.GetAnswersAsync(questionId);
        
        return getAnswers.ToAction();
    }

    /// <summary>
    /// PUT: /api/questions/:questionId/answers
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="answersRequest"></param>
    /// <returns></returns>
    [HttpPut]
    [ActionName(nameof(PutAnswersAsync))]
    [ServiceFilter<GetQuestionFilter>]
    public async Task<ActionResult<ServiceResponse<List<ViewAnswer>>>> PutAnswersAsync([FromRoute] QuestionId questionId, PutAnswersRequest answersRequest)
    {
        var replaceAnswers = await _answerService.ReplaceAnswersAsync(answersRequest);
        
        return replaceAnswers.ToAction();
    }


    /// <summary>
    /// GET: /api/questions/:questionId/answers/:answerId
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="answerId"></param>
    /// <returns></returns>
    [HttpGet("{answerId:answerId}")]
    [ActionName(nameof(GetAnswerAsync))]
    [ServiceFilter<GetAnswerFilter>]
    public async Task<ActionResult<ServiceResponse<ViewAnswer>>> GetAnswerAsync([FromRoute] QuestionId questionId, [FromRoute] string answerId)
    {
        var answer = _requestItems.Answer;

        ServiceResponse<ViewAnswer> result = new(answer);

        return result.ToAction();
    }

    /// <summary>
    /// DELETE: /api/questions/:questionId/answers/:answerId
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="answerId"></param>
    /// <returns></returns>
    [HttpDelete("{answerId:answerId}")]
    [ActionName(nameof(DeleteAnswerAsync))]
    [ServiceFilter<GetAnswerFilter>]
    public async Task<ActionResult<ServiceResponse>> DeleteAnswerAsync([FromRoute] QuestionId questionId, [FromRoute] string answerId)
    {
        var deleteResult = await _answerService.DeleteAnswerAsync(answerId);

        return deleteResult.ToAction();
    }

    /// <summary>
    /// PUT: /api/questions/:questionId/answers/:answerId
    /// </summary>
    /// <param name="answerRequest"></param>
    /// <returns></returns>
    [HttpPut("{answerId:answerId}")]
    [ActionName(nameof(GetAnswerAsync))]
    [ServiceFilter<PutAnswerFilter>]
    public async Task<ActionResult<ServiceResponse<ViewAnswer>>> PutAnswerAync(PutAnswerRequest answerRequest)
    {
        var saveResult = await _answerService.SaveAnswerAsync((Answer)answerRequest);

        return saveResult.ToAction();
    }
}
