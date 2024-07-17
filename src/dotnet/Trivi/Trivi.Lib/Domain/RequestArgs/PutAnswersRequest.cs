using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.RequestArgs;

public class PutAnswersRequest 
{
    [FromRoute]
    public required QuestionId QuestionId { get; set; }

    [FromBody]
    public required List<AnswerForm> AnswerForms { get; set; }


    public ServiceDataResponse<List<Answer>> ToModels()
    {
        var answers = AnswerForms.Select(a => new Answer()
        {
            AnswerText = a.Answer,
            Id = NanoIdUtility.BuildNanoId<Answer>(),
            IsCorrect = a.IsCorrect,
            QuestionId = QuestionId,
            CreatedOn = DateTime.UtcNow,
        });

        return answers.ToList();
    }
}
