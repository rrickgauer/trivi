using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Hubs.Question.EndpointParms;

public class AdminUpdatePlayerQuestionResponsesParms
{
    public required List<ViewPlayerQuestionResponse> Responses { get; set; }
}
