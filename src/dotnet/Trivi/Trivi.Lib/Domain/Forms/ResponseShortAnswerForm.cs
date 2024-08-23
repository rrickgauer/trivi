using Microsoft.AspNetCore.Mvc.ModelBinding;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Forms;

public class ResponseShortAnswerForm : ResponseForm, IResponseForm<ResponseShortAnswer>
{
    public override QuestionType QuestionType => QuestionType.ShortAnswer;

    [BindRequired]
    public required string Answer { get; set; }


    public ResponseShortAnswer ToResponse(QuestionId questionId)
    {
        return new()
        {
            QuestionId = questionId,
            PlayerId = PlayerId,
            Id = GuidUtility.New(),
            AnswerGiven = Answer,
        };
    }
}




