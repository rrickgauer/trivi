using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Models;

public class MultipleChoice : Question
{
    public override QuestionType QuestionType => QuestionType.MultipleChoice;

    public static MultipleChoice FromRequestForm(QuestionId questionId, MultipleChoiceForm form)
    {
        return new()
        {
            Id = questionId,
            CollectionId = form.CollectionId,
            Prompt = form.Prompt,
        };
    }
}
