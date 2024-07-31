using System.Data;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Mapping.Tables;

public class ViewResponseSAMapper : TableMapper<ViewResponseShortAnswer>
{
    public override ViewResponseShortAnswer ToModel(DataRow row)
    {
        ViewResponseShortAnswer result = InheritanceUtility.GetParentProperties<ViewResponse,  ViewResponseShortAnswer, ViewResponseMapper>(row);

        result.AnswerGiven = row.Field<string?>(GetColumnName(nameof(result.AnswerGiven)));
        result.CorrectAnswer = row.Field<string?>(GetColumnName(nameof(result.CorrectAnswer)));

        return result;
    }
}
