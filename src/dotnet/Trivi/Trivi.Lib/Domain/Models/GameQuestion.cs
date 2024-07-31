using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Models;

public class GameQuestion
{
    public QuestionId? QuestionId { get; set; }
    public string? GameId { get; set; }
    public GameQuestionStatus QuestionStatus { get; set; } = GameQuestionStatus.Pending;
}
