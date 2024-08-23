using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Models;

public class Response
{
    public Guid? Id { get; set; }
    public QuestionId? QuestionId { get; set; }
    public Guid? PlayerId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public bool IsCorrect { get; set; } = false;
    public ushort PointsAwarded { get; set; } = 0;

    public void SetGrade(ResponseGrade? grade)
    {
        IsCorrect = grade?.IsCorrect ?? false;
        PointsAwarded = grade?.Points ?? 0;
    }
}
