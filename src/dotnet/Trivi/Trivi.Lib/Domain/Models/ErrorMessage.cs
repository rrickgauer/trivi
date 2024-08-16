

using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Services.Implementations;

namespace Trivi.Lib.Domain.Models;

public class ErrorMessage
{
    [SqlColumn("id")]
    public ulong? Id { get; set; }

    [SqlColumn("message")]
    public string? Message { get; set; }


    public static explicit operator ErrorCode(ErrorMessage message)
    {
        ArgumentNullException.ThrowIfNull(message.Id);
        return (ErrorCode)message.Id;
    }
}


public static class ErrorMessageExtensions
{
    public static List<ErrorMessage> ToErrorMessages(this IEnumerable<ErrorCode> errors)
    {
        return ErrorMessageService.ToErrorMessages(errors);
    }
}