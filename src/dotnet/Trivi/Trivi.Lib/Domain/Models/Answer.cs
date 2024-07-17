using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Models;

public class Answer : INanoIdPrefix
{
    public static string NanoIdPrefix => "mca";

    public string? Id { get; set; }
    public QuestionId? QuestionId { get; set; }
    public string? AnswerText { get; set; }
    public bool IsCorrect { get; set; } = false;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
