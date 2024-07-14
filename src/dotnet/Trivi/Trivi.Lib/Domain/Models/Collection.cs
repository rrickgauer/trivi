using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Models;

public class Collection
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public Guid? UserId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public static Collection FromForm(CollectionForm form, Guid? userId)
    {
        return new()
        {
            Id = GuidUtility.New(),
            Name = form.Name,
            UserId = userId,
            CreatedOn = DateTime.UtcNow,
        };
    }
}
