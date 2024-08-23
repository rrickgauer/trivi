using System.Data;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Mapping.Tables;

public class ViewResponseMapper : TableMapper<ViewResponse>
{
    public override ViewResponse ToModel(DataRow row)
    {
        ViewResponse result = new();

        result.Id                = row.Field<Guid?>(GetColumnName(nameof(result.Id)));
        result.QuestionId        = row.Field<string>(GetColumnName(nameof(result.QuestionId)))!;
        result.PlayerId          = row.Field<Guid?>(GetColumnName(nameof(result.PlayerId)));
        result.ResponseCreatedOn = row.Field<DateTime>(GetColumnName(nameof(result.ResponseCreatedOn)));
        result.GameId            = row.Field<string?>(GetColumnName(nameof(result.GameId)));
        result.QuestionPrompt    = row.Field<string?>(GetColumnName(nameof(result.QuestionPrompt)));
        result.QuestionPoints    = row.Field<ushort>(GetColumnName(nameof(result.QuestionPoints)));
        result.PlayerNickname    = row.Field<string?>(GetColumnName(nameof(result.PlayerNickname)));
        result.CollectionId      = row.Field<Guid?>(GetColumnName(nameof(result.CollectionId)));
        result.CollectionUserId  = row.Field<Guid?>(GetColumnName(nameof(result.CollectionUserId)));
        result.PointsAwarded     = row.Field<ushort>(GetColumnName(nameof(result.PointsAwarded)));
        result.IsCorrect         = row.Field<bool>(GetColumnName(nameof(result.IsCorrect)));

        return result;
    }
}
