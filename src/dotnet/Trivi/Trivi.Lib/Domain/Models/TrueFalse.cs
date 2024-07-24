using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Models;

public class TrueFalse : Question
{
    public override QuestionType QuestionType => QuestionType.TrueFalse;

    public bool CorrectAnswer { get; set; } = true;


    public static TrueFalse FromRequestForm(QuestionId questionId, TrueFalseForm form)
    {
        return new()
        {
            Id = questionId,
            CollectionId = form.CollectionId,
            CorrectAnswer = form.CorrectAnswer,
            Prompt = form.Prompt,
            Points = form.Points,
        };
    }
}
