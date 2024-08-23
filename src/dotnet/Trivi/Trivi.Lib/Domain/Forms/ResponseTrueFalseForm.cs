using Microsoft.AspNetCore.Mvc.ModelBinding;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Forms;

public class ResponseTrueFalseForm : ResponseForm, IResponseForm<ResponseTrueFalse>
{
    public override QuestionType QuestionType => QuestionType.TrueFalse;

    [BindRequired]
    public required bool Answer { get; set; }

    public ResponseTrueFalse ToResponse(QuestionId questionId)
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




