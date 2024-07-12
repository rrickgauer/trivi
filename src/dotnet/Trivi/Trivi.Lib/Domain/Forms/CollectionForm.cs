using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Trivi.Lib.Domain.Forms;

public class CollectionForm
{
    [BindRequired]
    public required string Name { get; set; }
}
