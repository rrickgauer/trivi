
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.Responses;

public class ValidationFailureResponse
{
    public List<ValidationFailureErrorMessage> Errors { get; set; } = new();
    public object? Data { get; set; } = null;

    public ValidationFailureResponse(ModelStateDictionary modelStateDict)
    {
        Errors = new()
        {
            new(modelStateDict),
        };
    }
}
