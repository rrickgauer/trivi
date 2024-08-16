using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Hubs.Question.EndpointParms;

public class AdminJoinParms
{
    public required string GameId { get; set; }
    public required QuestionId QuestionId { get; set; }
}
