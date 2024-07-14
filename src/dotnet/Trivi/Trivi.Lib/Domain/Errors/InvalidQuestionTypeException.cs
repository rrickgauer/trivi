using System.Text.Json;

namespace Trivi.Lib.Domain.Errors;

public class InvalidQuestionTypeException() : JsonException("Invalid question type")
{

}
