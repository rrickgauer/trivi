using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Domain.ViewModels.Gui;

public class GameQuestionLayoutModel
{
    public required string PageTitle { get; set; }
    public required ViewGame Game { get; set; }
}

public class GameQuestionLayoutModel<T>(T pageModel) : GameQuestionLayoutModel
{
    public T PageModel { get; } = pageModel;
}



public abstract class GameQuestionVM<TQuestion> where TQuestion : ViewQuestion
{
    public abstract TQuestion Question { get; set; }
}

public class ShortAnswerGameQuestionVM : GameQuestionVM<ViewShortAnswer>
{
    public override required ViewShortAnswer Question { get; set; }
}


public class TrueFalseGameQuestionVM : GameQuestionVM<ViewTrueFalse>
{
    public override required ViewTrueFalse Question { get; set; }
}


public class MultipleChoiceGameQuestionVM : GameQuestionVM<ViewMultipleChoice>
{
    public override required ViewMultipleChoice Question { get; set; }
}

