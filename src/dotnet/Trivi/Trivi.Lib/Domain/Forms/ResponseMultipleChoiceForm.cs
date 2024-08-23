using Microsoft.AspNetCore.Mvc.ModelBinding;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Forms;

public class ResponseMultipleChoiceForm : ResponseForm, IResponseForm<ResponseMultipleChoice>
{
    public override QuestionType QuestionType => QuestionType.MultipleChoice;

    [BindRequired]
    public required string Answer { get; set; }

    public ResponseMultipleChoice ToResponse(QuestionId questionId)
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




