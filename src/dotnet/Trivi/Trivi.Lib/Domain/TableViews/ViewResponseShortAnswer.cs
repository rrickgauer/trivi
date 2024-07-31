using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.TableViews;

public class ViewResponseShortAnswer : ViewResponse, ITableView<ViewResponseShortAnswer, ResponseShortAnswer>
{
    [SqlColumn("answer_given")]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.AnswerGiven))]
    [JsonPropertyName("answer")]
    public string? AnswerGiven { get; set; }

    [SqlColumn("correct_answer")]
    [JsonIgnore]
    public string? CorrectAnswer { get; set; }


    public static explicit operator ResponseShortAnswer(ViewResponseShortAnswer other) => other.CastToModel<ViewResponseShortAnswer, ResponseShortAnswer>();
}
