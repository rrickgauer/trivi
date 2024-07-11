

using Trivi.Lib.Domain.Attributes;

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
