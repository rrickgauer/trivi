using System.Data;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Mapping.Tables;

public class ViewResponseTrueFalseMapper : TableMapper<ViewResponseTrueFalse>
{
    public override ViewResponseTrueFalse ToModel(DataRow row)
    {
        ViewResponseTrueFalse result = InheritanceUtility.GetParentProperties<ViewResponse, ViewResponseTrueFalse, ViewResponseMapper>(row);

        result.AnswerGiven = row.Field<bool>(GetColumnName(nameof(result.AnswerGiven)));
        result.CorrectAnswer = row.Field<bool>(GetColumnName(nameof(result.CorrectAnswer)));

        return result;
    }
}
