namespace Trivi.Lib.Domain.TableViews;

public class ViewMultipleChoice : ViewQuestion
{
    public List<ViewAnswer> Answers { get; set; } = new();
}



