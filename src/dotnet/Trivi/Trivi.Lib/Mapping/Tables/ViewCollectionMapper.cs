using System.Data;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Mapping.Tables;

public class ViewCollectionMapper : TableMapper<ViewCollection>
{
    public override ViewCollection ToModel(DataRow row)
    {
        ViewCollection result = new()
        {
            CollectionId = row.Field<Guid?>(GetColumnName(nameof(result.CollectionId))),
            CollectionName = row.Field<string?>(GetColumnName(nameof(result.CollectionName))),
            CollectionUserId = row.Field<Guid?>(GetColumnName(nameof(result.CollectionUserId))),
            CollectionCreatedOn = row.Field<DateTime>(GetColumnName(nameof(result.CollectionCreatedOn))),
        };

        return result;
    }
}
