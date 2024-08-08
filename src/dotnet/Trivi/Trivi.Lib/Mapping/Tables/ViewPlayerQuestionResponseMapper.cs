using System.Data;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Mapping.Tables;

public class ViewPlayerQuestionResponseMapper : TableMapper<ViewPlayerQuestionResponse>
{
    public override ViewPlayerQuestionResponse ToModel(DataRow row)
    {
        ViewPlayerQuestionResponse result = InheritanceUtility.GetParentProperties<ViewPlayer, ViewPlayerQuestionResponse, ViewPlayerMapper>(row);

        result.HasResponse = (row.Field<int>(GetColumnName(nameof(result.HasResponse)))).ToNativeBool();

        return result;
    }
}
