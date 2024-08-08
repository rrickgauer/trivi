using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Api;

[Route("api/responses")]
[ApiController]
public class ApiResponsesController(IResponseService responseService) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<ApiResponsesController>();

    private readonly IResponseService _responseService = responseService;

    [HttpPost("{questionId:shortAnswerQuestion}")]
    [ActionName(nameof(PostShortAnswerAsync))]
    [ServiceFilter<PostResponseFilter>]
    public async Task<IActionResult> PostShortAnswerAsync([FromRoute] QuestionId questionId, [FromBody] ResponseShortAnswerForm data)
    {
        ResponseShortAnswer response = data.ToResponse(questionId);

        var createResponse = await _responseService.CreateShortAnswerResponseAsync(response);

        if (!createResponse.Successful)
        {
            return BadRequest(createResponse);
        }

        return Ok(createResponse);
    }


    [HttpPost("{questionId:trueFalseQuestion}")]
    [ActionName(nameof(PostTrueFalseAsync))]
    [ServiceFilter<PostResponseFilter>]
    public async Task<IActionResult> PostTrueFalseAsync([FromRoute] QuestionId questionId, [FromBody] ResponseTrueFalseForm data)
    {
        ResponseTrueFalse response = data.ToResponse(questionId);

        var createResponse = await _responseService.CreateTrueFalseResponseAsync(response);

        if (!createResponse.Successful)
        {
            return BadRequest(createResponse);
        }

        return Ok(createResponse);
    }


    [HttpPost("{questionId:multipleChoiceQuestion}")]
    [ActionName(nameof(PostMultipleChoiceAsync))]
    [ServiceFilter<PostResponseFilter>]
    public async Task<IActionResult> PostMultipleChoiceAsync([FromRoute] QuestionId questionId, [FromBody] ResponseMultipleChoiceForm data)
    {
        ResponseMultipleChoice response = data.ToResponse(questionId);

        var createResponse = await _responseService.CreateMultipleChoiceResponseAsync(response);

        if (!createResponse.Successful)
        {
            return BadRequest(createResponse);
        }

        return Ok(createResponse);
    }




}
