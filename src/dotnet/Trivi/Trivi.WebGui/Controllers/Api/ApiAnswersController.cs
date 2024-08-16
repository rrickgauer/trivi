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

    [HttpGet]
    [ActionName(nameof(GetAnswersAsync))]
    [ServiceFilter<GetQuestionFilter>]
    public async Task<IActionResult> GetAnswersAsync([FromRoute] QuestionId questionId)
    {
        var getanswers = await _answerService.GetAnswersAsync(questionId);

        return Ok(getanswers);
    }


    [HttpPut]
    [ActionName(nameof(PutAnswersAsync))]
    [ServiceFilter<GetQuestionFilter>]
    public async Task<IActionResult> PutAnswersAsync([FromRoute] QuestionId questionId, PutAnswersRequest answersRequest)
    {
        var replaceAnswers = await _answerService.ReplaceAnswersAsync(answersRequest);
        return FromServiceDataResponse(replaceAnswers);
    }


    [HttpGet("{answerId:answerId}")]
    [ActionName(nameof(GetAnswerAsync))]
    [ServiceFilter<GetAnswerFilter>]
    public async Task<IActionResult> GetAnswerAsync([FromRoute] QuestionId questionId, [FromRoute] string answerId)
    {
        var answer = _requestItems.Answer;

        return Ok(new ServiceResponse<ViewAnswer>(answer));
    }   

    [HttpDelete("{answerId:answerId}")]
    [ActionName(nameof(DeleteAnswerAsync))]
    [ServiceFilter<GetAnswerFilter>]
    public async Task<IActionResult> DeleteAnswerAsync([FromRoute] QuestionId questionId, [FromRoute] string answerId)
    {
        var deleteResult = await _answerService.DeleteAnswerAsync(answerId);
        return FromServiceResponse(deleteResult);
    }


    [HttpPut("{answerId:answerId}")]
    [ActionName(nameof(GetAnswerAsync))]
    [ServiceFilter<PutAnswerFilter>]
    public async Task<IActionResult> PutAnswerAync(PutAnswerRequest answerRequest)
    {
        var saveResult = await _answerService.SaveAnswerAsync((Answer)answerRequest);
        return FromServiceDataResponse(saveResult);
    }


}
