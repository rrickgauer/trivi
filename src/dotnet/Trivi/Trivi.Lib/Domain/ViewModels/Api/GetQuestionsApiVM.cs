using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Domain.ViewModels.Api;

public class GetQuestionsApiVM
{
    public required List<ViewQuestion> Questions { get; set; }
}
