using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Trivi.Lib.Domain.Constants;

namespace Trivi.Lib.Domain.Forms;



public abstract class QuestionForm
{
    [BindRequired]
    public required string Prompt { get; set; }

    [BindRequired]
    public required Guid CollectionId { get; set; }

    [BindRequired]
    [Range(QuestionConstants.MinimumPointsValue, QuestionConstants.MaximumPointsValue)]
    public required ushort Points { get; set; }
}


public class TrueFalseForm : QuestionForm
{
    [BindRequired]
    public required bool CorrectAnswer { get; set; }
}


public class ShortAnswerForm : QuestionForm
{
    [BindRequired]
    public required string CorrectAnswer { get; set; }
}

public class MultipleChoiceForm : QuestionForm
{

}