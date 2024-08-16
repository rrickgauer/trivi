using System.Data;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Mapping.Tables;

public class ViewResponseMultipleChoiceMapper : TableMapper<ViewResponseMultipleChoice>
{
    public override ViewResponseMultipleChoice ToModel(DataRow row)
    {
        ViewResponseMultipleChoice result = InheritanceUtility.GetParentProperties<ViewResponse, ViewResponseMultipleChoice, ViewResponseMapper>(row);

        result.AnswerGiven = row.Field<string?>(GetColumnName(nameof(result.AnswerGiven)));

        return result;
    }
}
