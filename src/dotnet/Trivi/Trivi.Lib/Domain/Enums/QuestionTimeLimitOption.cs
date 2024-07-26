using System.ComponentModel;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Enums;

public enum QuestionTimeLimitOption
{
    [Description("None")]
    None = 1,

    [Description("15 seconds")]
    Fifteen = 2,

    [Description("30 seconds")]
    Thirty = 3,

    [Description("60 seconds")]
    Sixty = 4,
}


public static class QuestionTimeLimitOptionExtensions
{
    public static string GetRadioLabel(this QuestionTimeLimitOption option)
    {
        var attr = AttributeUtility.GetEnumAttribute<QuestionTimeLimitOption, DescriptionAttribute>(option);
        return attr.Description;
    }
}
