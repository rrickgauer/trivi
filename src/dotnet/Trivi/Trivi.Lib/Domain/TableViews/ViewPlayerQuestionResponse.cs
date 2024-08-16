using Trivi.Lib.Domain.Attributes;

namespace Trivi.Lib.Domain.TableViews;

public class ViewPlayerQuestionResponse : ViewPlayer
{
    [SqlColumn("has_response")]
    public bool HasResponse { get; set; } = false;
}

