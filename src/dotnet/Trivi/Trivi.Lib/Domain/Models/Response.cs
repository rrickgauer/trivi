using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Models;

public class Response
{
    public Guid? Id { get; set; }
    public QuestionId? QuestionId { get; set; }
    public Guid? PlayerId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
