using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.JsonConverters;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Enums;


[JsonConverter(typeof(QuestionTypeJsonConverter))]
public enum QuestionType : ushort
{
    [Prefix("mc")]
    MultipleChoice = 1,

    [Prefix("tf")]
    TrueFalse = 2,

    [Prefix("sa")]
    ShortAnswer = 3,
}


public static class QuestionTypeExtensions
{

    public static string GetNewNanoId(this QuestionType questionType)
    {
        return $"{questionType.GetPrefix()}_{NanoIdUtility.New()}";
    }

    public static string GetPrefix(this QuestionType questionType)
    {
        var attr = AttributeUtility.GetEnumAttribute<QuestionType, PrefixAttribute>(questionType);

        return attr.Prefix;
    }



    public static QuestionType ParsePrefix(string prefix)
    {
        QuestionType qt;


        if (prefix == QuestionType.MultipleChoice.GetPrefix())
        {
            qt = QuestionType.MultipleChoice;
        }

        else if (prefix == QuestionType.ShortAnswer.GetPrefix())
        {
            qt = QuestionType.ShortAnswer;
        }

        else if (prefix == QuestionType.TrueFalse.GetPrefix())
        {
            qt = QuestionType.TrueFalse;
        }
        else
        {
            throw new InvalidQuestionTypeException();
        }

        return qt;
    }

}
