
using System.Data;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Mapping.Tables;

namespace Deadit.Lib.Mapping.Tables;

public class ErrorMessageTableMapper : TableMapper<ErrorMessage>
{
    public override ErrorMessage ToModel(DataRow row)
    {
        ErrorMessage errorMessage = new()
        {
            Id = row.Field<ulong?>(GetColumnName(nameof(ErrorMessage.Id))),
            Message = row.Field<string?>(GetColumnName(nameof(ErrorMessage.Message)))
        };

        return errorMessage;
    }
}
