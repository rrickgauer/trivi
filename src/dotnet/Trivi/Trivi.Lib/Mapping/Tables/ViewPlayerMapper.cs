using System.Data;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Mapping.Tables;

public class ViewPlayerMapper : TableMapper<ViewPlayer>
{
    public override ViewPlayer ToModel(DataRow row)
    {
        ViewPlayer result = new();

        result.Id = row.Field<Guid?>(GetColumnName(nameof(result.Id)));
        result.GameId = row.Field<string?>(GetColumnName(nameof(result.GameId)));
        result.Nickname = row.Field<string?>(GetColumnName(nameof(result.Nickname)));
        result.CreatedOn = row.Field<DateTime>(GetColumnName(nameof(result.CreatedOn)));
        result.CollectionId = row.Field<Guid>(GetColumnName(nameof(result.CollectionId)));
        result.GameStatus = (GameStatus)row.Field<ushort>(GetColumnName(nameof(result.GameStatus)));
        result.CollectionUserId = row.Field<Guid?>(GetColumnName(nameof(result.CollectionUserId)));

        return result;
    }
}
