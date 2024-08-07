using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.TableViews;

public class ViewResponseTrueFalse : ViewResponse, ITableView<ViewResponseTrueFalse, ResponseTrueFalse>
{

    [SqlColumn("answer_given")]
    [CopyToProperty<ResponseTrueFalse>(nameof(ResponseTrueFalse.AnswerGiven))]
    [JsonPropertyName("answer")]
    public bool AnswerGiven { get; set; } = false;


    [SqlColumn("correct_answer")]
    [JsonIgnore]
    public bool CorrectAnswer { get; set; } = false;


    public static explicit operator ResponseTrueFalse(ViewResponseTrueFalse other) => other.CastToModel<ViewResponseTrueFalse, ResponseTrueFalse>();
}
