using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Domain.Other;

public class ResponseGrade
{
    public required bool IsCorrect { get; set; } 
    public required ushort Points { get; set; } 


    public static ResponseGrade Incorrect()
    {
        return new()
        {
            IsCorrect = false,
            Points = 0,
        };
    }

    public static ResponseGrade Correct(ViewQuestion question)
    {
        return new()
        {
            IsCorrect = true,
            Points = question.Points,
        };
    }
}
