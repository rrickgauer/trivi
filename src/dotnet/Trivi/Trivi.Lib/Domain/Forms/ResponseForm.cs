using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Trivi.Lib.Domain.Forms;

public abstract class ResponseForm
{
    public abstract QuestionType QuestionType { get; }

    [BindRequired]
    public required Guid PlayerId { get; set; }
}




