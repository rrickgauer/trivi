using System.Data;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Mapping.Tables;

public class ViewGameQuestionMapper : TableMapper<ViewGameQuestion>
{
    public override ViewGameQuestion ToModel(DataRow row)
    {
        ViewGameQuestion result = InheritanceUtility.GetParentProperties<ViewQuestion, ViewGameQuestion, ViewQuestionMapper>(row);

        result.GameId = row.Field<string?>(GetColumnName(nameof(result.GameId)));
        result.QuestionStatus = row.Field<GameQuestionStatus>(GetColumnName(nameof(result.QuestionStatus)));
        result.GameStatus = row.Field<GameStatus>(GetColumnName(nameof(result.GameStatus)));

        return result;
    }
}
