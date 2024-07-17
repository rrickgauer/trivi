using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using Trivi.Lib.Domain.Constants;

namespace Trivi.Lib.Domain.Forms;

public class AnswerForm
{
    [BindRequired]
    public required string Answer { get; set; }

    [BindRequired]
    public required bool IsCorrect { get; set; }
}


