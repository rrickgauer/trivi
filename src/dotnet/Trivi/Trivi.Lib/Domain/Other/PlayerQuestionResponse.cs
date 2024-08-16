namespace Trivi.Lib.Domain.Other;

public class PlayerQuestionResponse
{
    public required QuestionId QuestionId { get; set; }
    public required Guid PlayerId { get; set; }
}
