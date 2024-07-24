using System.Data;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Mapping.Tables;

public class ViewGameMapper : TableMapper<ViewGame>
{
    public override ViewGame ToModel(DataRow row)
    {
        ViewGame result = new();

        result.Id                 = row.Field<string?>(GetColumnName(nameof(result.Id)));
        result.CollectionId       = row.Field<Guid?>(GetColumnName(nameof(result.CollectionId)));
        result.RandomizeQuestions = row.Field<bool>(GetColumnName(nameof(result.RandomizeQuestions)));
        result.QuestionTimeLimit  = row.Field<ushort?>(GetColumnName(nameof(result.QuestionTimeLimit)));
        result.CreatedOn          = row.Field<DateTime>(GetColumnName(nameof(result.CreatedOn)));
        result.StartedOn          = row.Field<DateTime?>(GetColumnName(nameof(result.StartedOn)));
        result.UserId             = row.Field<Guid?>(GetColumnName(nameof(result.UserId)));
        result.Status             = row.Field<GameStatus>(GetColumnName(nameof(result.Status)));

        return result;
    }
}
