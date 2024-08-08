using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Domain.ViewModels.Gui;

public class AdminQuestionViewModel
{
    public required ViewGame Game { get; set; }
    public required ViewGameQuestion GameQuestion { get; set; }
    public required List<ViewPlayerQuestionResponse> PlayerQuestionResponses { get; set; }
}
