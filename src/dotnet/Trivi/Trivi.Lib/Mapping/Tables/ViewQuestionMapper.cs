using System.Data;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Mapping.Tables;

public class ViewQuestionMapper : TableMapper<ViewQuestion>
{
    public override ViewQuestion ToModel(DataRow row)
    {
        ViewQuestion result = new();

        result.Id           = row.Field<string>(GetColumnName(nameof(result.Id)))!;
        result.CollectionId = row.Field<Guid?>(GetColumnName(nameof(result.CollectionId)));
        result.Prompt       = row.Field<string?>(GetColumnName(nameof(result.Prompt)));
        result.Points       = row.Field<ushort>(GetColumnName(nameof(result.Points)));
        result.CreatedOn    = row.Field<DateTime>(GetColumnName(nameof(result.CreatedOn)));
        result.UserId       = row.Field<Guid?>(GetColumnName(nameof(result.UserId)));
        result.QuestionType = row.Field<QuestionType>(GetColumnName(nameof(result.QuestionType)));

        return result;
    }
}



public class ViewShortAnswerMapper : TableMapper<ViewShortAnswer>
{
    public override ViewShortAnswer ToModel(DataRow row)
    {
        ViewShortAnswer result = InheritanceUtility.GetParentProperties<ViewQuestion, ViewShortAnswer, ViewQuestionMapper>(row);

        result.CorrectAnswer = row.Field<string?>(GetColumnName(nameof(result.CorrectAnswer)));

        return result;

    }
}


public class ViewTrueFalseMapper : TableMapper<ViewTrueFalse>
{
    public override ViewTrueFalse ToModel(DataRow row)
    {
        ViewTrueFalse result = InheritanceUtility.GetParentProperties<ViewQuestion, ViewTrueFalse, ViewQuestionMapper>(row);

        result.CorrectAnswer = row.Field<bool>(GetColumnName(nameof(result.CorrectAnswer)));

        return result;

    }
}


public class ViewMultipleChoiceMapper : TableMapper<ViewMultipleChoice>
{
    public override ViewMultipleChoice ToModel(DataRow row)
    {
        ViewMultipleChoice result = InheritanceUtility.GetParentProperties<ViewQuestion, ViewMultipleChoice, ViewQuestionMapper>(row);

        return result;

    }
}