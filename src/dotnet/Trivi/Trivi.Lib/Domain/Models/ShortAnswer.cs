using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Models;

public class ShortAnswer : Question
{
    public override QuestionType QuestionType => QuestionType.ShortAnswer;

    public string? CorrectAnswer { get; set; }


    public static ShortAnswer FromRequestForm(QuestionId questionId, ShortAnswerForm requestForm)
    {
        return new()
        {
            Id = questionId,
            CollectionId = requestForm.CollectionId,
            CorrectAnswer = requestForm.CorrectAnswer,
            Prompt = requestForm.Prompt,
            Points = requestForm.Points,
        };
    }
}