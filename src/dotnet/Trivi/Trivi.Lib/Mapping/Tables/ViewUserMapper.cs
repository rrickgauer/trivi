using System.Data;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Mapping.Tables;

public class ViewUserMapper : TableMapper<ViewUser>
{
    public override ViewUser ToModel(DataRow row)
    {
        ViewUser result = new()
        {
            UserId        = row.Field<Guid?>(GetColumnName(nameof(result.UserId))),
            UserEmail     = row.Field<string?>(GetColumnName(nameof(result.UserEmail))),
            UserPassword  = row.Field<string?>(GetColumnName(nameof(result.UserPassword))),
            UserCreatedOn = row.Field<DateTime>(GetColumnName(nameof(result.UserCreatedOn))),
        };

        return result;
    }
}
