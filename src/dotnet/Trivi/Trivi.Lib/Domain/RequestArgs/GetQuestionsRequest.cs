using Microsoft.AspNetCore.Mvc;

namespace Trivi.Lib.Domain.RequestArgs;

public class GetQuestionsRequest
{
    [FromQuery(Name = "Collection")]
    public required Guid CollectionId { get; set; }
}
