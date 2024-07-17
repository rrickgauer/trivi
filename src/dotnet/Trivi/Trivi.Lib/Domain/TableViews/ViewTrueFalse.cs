using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.TableViews;

public class ViewTrueFalse : ViewQuestion
{
    [SqlColumn("question_correct_answer")]
    [CopyToProperty<TrueFalse>(nameof(TrueFalse.CorrectAnswer))]
    public bool CorrectAnswer { get; set; } = true;
}



