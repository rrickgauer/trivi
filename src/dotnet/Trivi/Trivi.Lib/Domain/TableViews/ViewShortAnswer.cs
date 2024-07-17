using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.TableViews;

public class ViewShortAnswer : ViewQuestion
{
    [SqlColumn("question_correct_answer")]
    [CopyToProperty<ShortAnswer>(nameof(ShortAnswer.CorrectAnswer))]   
    public string? CorrectAnswer { get; set; }
}



