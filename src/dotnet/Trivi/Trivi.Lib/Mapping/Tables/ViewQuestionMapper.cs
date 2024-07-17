using System.Data;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Mapping.Tables;

public class ViewQuestionMapper : TableMapper<ViewQuestion>
{
    public override ViewQuestion ToModel(DataRow row)
    {
        ViewQuestion result = new();

        result.QuestionId           = row.Field<string>(GetColumnName(nameof(result.QuestionId)))!;
        result.QuestionCollectionId = row.Field<Guid?>(GetColumnName(nameof(result.QuestionCollectionId)));
        result.QuestionPrompt       = row.Field<string?>(GetColumnName(nameof(result.QuestionPrompt)));
        result.QuestionCreatedOn    = row.Field<DateTime>(GetColumnName(nameof(result.QuestionCreatedOn)));
        result.QuestionUserId       = row.Field<Guid?>(GetColumnName(nameof(result.QuestionUserId)));
        result.QuestionType         = row.Field<QuestionType>(GetColumnName(nameof(result.QuestionType)));

        return result;
    }
}



public class ViewShortAnswerMapper : TableMapper<ViewShortAnswer>
{
    public override ViewShortAnswer ToModel(DataRow row)
    {
        ViewShortAnswer result = InheritanceUtility.GetParentProperties<ViewQuestion, ViewShortAnswer, ViewQuestionMapper>(row);

        result.QuestionCorrectAnswer = row.Field<string?>(GetColumnName(nameof(result.QuestionCorrectAnswer)));

        return result;

    }
}


public class ViewTrueFalseMapper : TableMapper<ViewTrueFalse>
{
    public override ViewTrueFalse ToModel(DataRow row)
    {
        ViewTrueFalse result = InheritanceUtility.GetParentProperties<ViewQuestion, ViewTrueFalse, ViewQuestionMapper>(row);

        result.QuestionCorrectAnswer = row.Field<bool>(GetColumnName(nameof(result.QuestionCorrectAnswer)));

        return result;

    }
}


public class ViewMultipleChoiceMapper : TableMapper<ViewMultipleChoice>
{
    public override ViewMultipleChoice ToModel(DataRow row)
    {
        ViewMultipleChoice result = InheritanceUtility.GetParentProperties<ViewQuestion, ViewMultipleChoice, ViewQuestionMapper>(row);

        //result.QuestionCorrectAnswer = row.Field<bool>(GetColumnName(nameof(result.QuestionCorrectAnswer)));

        return result;

    }
}