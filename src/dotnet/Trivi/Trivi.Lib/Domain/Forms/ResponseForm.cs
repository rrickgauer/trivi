using Microsoft.AspNetCore.Mvc.ModelBinding;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Forms;

public abstract class ResponseForm
{
    public abstract QuestionType QuestionType { get; }

    [BindRequired]
    public required Guid PlayerId { get; set; }
}

public class ResponseShortAnswerForm : ResponseForm
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


public class ResponseTrueFalseForm : ResponseForm
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




