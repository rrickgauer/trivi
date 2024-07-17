namespace Trivi.Lib.Domain.Constants;

public class NanoIdConstants
{
    public const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const int IdLength = 17;

    public const string QuestionIdRegex = @"\b(?:mc|tf|sa)_[A-Z0-9]{17}\b";
    public const string TrueFalseRegex = @"\b(?:tf)_[A-Z0-9]{17}\b";
    public const string ShortAnswerRegex = @"\b(?:sa)_[A-Z0-9]{17}\b";
    public const string MultipleChoiceRegex = @"\b(?:mc)_[A-Z0-9]{17}\b";

    public const string AnswerIdRegex = @"\b(?:mca)_[A-Z0-9]{17}\b";
}

