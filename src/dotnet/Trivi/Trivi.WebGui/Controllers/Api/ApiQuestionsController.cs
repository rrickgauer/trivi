using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Domain.ViewModels.Api;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Implementations;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Api;


[ApiController]
[Route("api/questions")]
[ServiceFilter<InternalApiAuthFilter>]
public class ApiQuestionsController(GetQuestionsApiVMService getQuestionsVMService, IQuestionService questionService, RequestItems requestItems) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<ApiQuestionsController>();

    private readonly GetQuestionsApiVMService _getQuestionsVMService = getQuestionsVMService;
    private readonly IQuestionService _questionService = questionService;
    private readonly RequestItems _requestItems = requestItems;

    /// <summary>
    /// GET: /api/questions
    /// </summary>
    /// <param name="queryParms"></param>
    /// <returns></returns>
    [HttpGet]
    [ActionName(nameof(GetQuestionsAsync))]
    public async Task<ActionResult<ServiceResponse<GetQuestionsApiVM>>> GetQuestionsAsync(GetQuestionsRequest queryParms)
    {
        var getViewModel = await _getQuestionsVMService.GetViewModelAsync(new()
        {
            CollectionId = queryParms.CollectionId,
        });

        return getViewModel.ToAction();
    }


    /// <summary>
    /// GET: /api/questions/:questionId
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    [HttpGet("{questionId:questionId}")]
    [ActionName(nameof(GetQuestionAsync))]
    [ServiceFilter<GetQuestionFilter>]
    public async Task<IActionResult> GetQuestionAsync([FromRoute] QuestionId questionId)
    {
        return Ok(new ServiceResponse<object>(_requestItems.Question as object));
    }

    /// <summary>
    /// DELETE /api/questions/:questionId
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    [HttpDelete("{questionId:questionId}")]
    [ActionName(nameof(DeleteQuestionAsync))]
    [ServiceFilter<GetQuestionFilter>]
    public async Task<ActionResult<ServiceResponse>> DeleteQuestionAsync([FromRoute] QuestionId questionId)
    {
        var deleteQuestion = await _questionService.DeleteQuestionAsync(questionId);

        return deleteQuestion.ToAction();
    }




    /// <summary>
    /// PUT: /api/questions/:sa_questionId
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="questionForm"></param>
    /// <returns></returns>
    [HttpPut("{questionId:shortAnswerQuestion}")]
    [ActionName(nameof(PutShortAnswerAsync))]
    [ServiceFilter<SaveQuestionFilter>]
    public async Task<ActionResult<ServiceResponse<ViewShortAnswer>>> PutShortAnswerAsync([FromRoute] QuestionId questionId, [FromBody] ShortAnswerForm questionForm)
    {
        var question = ShortAnswer.FromRequestForm(questionId, questionForm);

        var saveResult = await _questionService.SaveShortAnswerAsync(question);

        return saveResult.ToAction();
    }

    /// <summary>
    /// PUT: /api/questions/:tf_questionId
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="questionForm"></param>
    /// <returns></returns>
    [HttpPut("{questionId:trueFalseQuestion}")]
    [ActionName(nameof(PutTrueFalseAsync))]
    [ServiceFilter<SaveQuestionFilter>]
    public async Task<ActionResult<ServiceResponse<ViewTrueFalse>>> PutTrueFalseAsync([FromRoute] QuestionId questionId, [FromBody] TrueFalseForm questionForm)
    {
        var question = TrueFalse.FromRequestForm(questionId, questionForm);

        var saveResult = await _questionService.SaveTrueFalseAsync(question);
        
        return saveResult.ToAction();
    }


    /// <summary>
    /// PUT: /api/questions/:mc_questionId
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="questionForm"></param>
    /// <returns></returns>
    [HttpPut("{questionId:multipleChoiceQuestion}")]
    [ActionName(nameof(PutMultipleChoiceAsync))]
    [ServiceFilter<SaveQuestionFilter>]
    public async Task<ActionResult<ServiceResponse<ViewMultipleChoice>>> PutMultipleChoiceAsync([FromRoute] QuestionId questionId, [FromBody] MultipleChoiceForm questionForm)
    {
        var question = MultipleChoice.FromRequestForm(questionId, questionForm);

        var saveResult = await _questionService.SaveMultipleChoiceAsync(question);

        return saveResult.ToAction();
    }
}
