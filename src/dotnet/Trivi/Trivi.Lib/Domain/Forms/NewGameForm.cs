using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Trivi.Lib.Domain.Forms;

public class NewGameForm
{

    [BindRequired]
    public required Guid CollectionId { get; set; }

    [BindRequired]
    public required bool RandomizeQuestions { get; set; }

    [BindRequired]
    public required ushort? QuestionTimeLimit { get; set; }
}
