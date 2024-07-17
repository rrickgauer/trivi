using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.RequestArgs;

public class PutAnswerRequest : ITableView<PutAnswerRequest, Answer>
{
    [FromRoute]
    public required QuestionId QuestionId { get; set; }

    [FromRoute]
    public required string AnswerId { get; set; }

    [FromBody]
    public required AnswerForm AnswerForm { get; set; }


    public static explicit operator Answer(PutAnswerRequest other)
    {
        return new()
        {
            AnswerText = other.AnswerForm.Answer,
            Id = other.AnswerId,
            QuestionId = other.QuestionId,
            IsCorrect = other.AnswerForm.IsCorrect,
        };
    }
}
