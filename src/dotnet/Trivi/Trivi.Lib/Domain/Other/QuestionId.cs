using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.JsonConverters;

namespace Trivi.Lib.Domain.Other;

[JsonConverter(typeof(QuestionIdConverter))]
public class QuestionId : IParsable<QuestionId>
{
    public QuestionType QuestionType { get; set; }
    public string Id { get; set; }

    public QuestionId(string id,  QuestionType questionType)
    {
        ValidateId(id);

        Id = id;
        QuestionType = questionType;
    }

    public QuestionId(string id)
    {
        ValidateId(id);
        
        Id = id;
        QuestionType = QuestionTypeExtensions.ParsePrefix(id.Substring(0,2));
    }


    private static void ValidateId(string id)
    {
        if (!Regex.IsMatch(id, NanoIdConstants.QuestionIdRegex))
        {
            throw new QuestionIdFormatException();
        }
    }





    public override string ToString()
    {
        return Id;
    }

    public static QuestionId Parse(string value, IFormatProvider? provider)
    {
        if (!TryParse(value, provider, out var result))
        {
            throw new ArgumentException("Could not parse supplied value.", nameof(value));
        }

        return result;
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out QuestionId result)
    {
        result = null!;

        if (string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        try
        {
            result = new(s);
            return true;
        }
        catch(QuestionIdFormatException)
        {
            return false;
        }

    }

    public static implicit operator QuestionType(QuestionId questionId)
    {
        return questionId.QuestionType;
    }


    public static implicit operator string(QuestionId questionId)
    {
        return questionId.Id;
    }

    public static implicit operator QuestionId(string id)
    {
        return new QuestionId(id);
    }


    public static bool operator ==(QuestionId? left, QuestionId? right)
    {

        var leftId = left?.Id ?? string.Empty;
        var rightId = right?.Id ?? string.Empty;

        return leftId.Equals(rightId);
    }

    public static bool operator !=(QuestionId? left, QuestionId? right)
    {
        var leftId = left?.Id ?? string.Empty;
        var rightId = right?.Id ?? string.Empty;

        return !leftId.Equals(rightId);
    }

}
