using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.TableViews;

public class ViewResponseMultipleChoice : ViewResponse, ITableView<ViewResponseMultipleChoice, ResponseMultipleChoice>
{
    [SqlColumn("answer_given")]
    [CopyToProperty<ResponseTrueFalse>(nameof(ResponseMultipleChoice.AnswerGiven))]
    [JsonPropertyName("answer")]
    public string? AnswerGiven { get; set; }

    public static explicit operator ResponseMultipleChoice(ViewResponseMultipleChoice other) => other.CastToModel<ViewResponseMultipleChoice, ResponseMultipleChoice>();
}
