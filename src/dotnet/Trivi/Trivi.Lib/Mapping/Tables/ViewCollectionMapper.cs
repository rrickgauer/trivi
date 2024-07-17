using System.Data;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Mapping.Tables;

public class ViewCollectionMapper : TableMapper<ViewCollection>
{
    public override ViewCollection ToModel(DataRow row)
    {
        ViewCollection result = new()
        {
            Id = row.Field<Guid?>(GetColumnName(nameof(result.Id))),
            Name = row.Field<string?>(GetColumnName(nameof(result.Name))),
            UserId = row.Field<Guid?>(GetColumnName(nameof(result.UserId))),
            CreatedOn = row.Field<DateTime>(GetColumnName(nameof(result.CreatedOn))),
        };

        return result;
    }
}
