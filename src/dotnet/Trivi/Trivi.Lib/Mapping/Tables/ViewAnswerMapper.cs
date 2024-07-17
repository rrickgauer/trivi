using System.Data;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Mapping.Tables;

public class ViewAnswerMapper : TableMapper<ViewAnswer>
{
    public override ViewAnswer ToModel(DataRow row)
    {
        ViewAnswer result = new();

        result.Id            = row.Field<string?>(GetColumnName(nameof(result.Id)));
        result.QuestionId    = row.Field<string>(GetColumnName(nameof(result.QuestionId)))!;
        result.AnswerText    = row.Field<string?>(GetColumnName(nameof(result.AnswerText)));
        result.IsCorrect     = row.Field<bool>(GetColumnName(nameof(result.IsCorrect)));
        result.CreatedOn     = row.Field<DateTime>(GetColumnName(nameof(result.CreatedOn)));
        result.CollectionId  = row.Field<Guid?>(GetColumnName(nameof(result.CollectionId)));
        result.UserId        = row.Field<Guid?>(GetColumnName(nameof(result.UserId)));

        return result;
    }
}
