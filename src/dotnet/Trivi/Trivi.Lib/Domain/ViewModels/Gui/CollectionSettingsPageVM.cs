using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Domain.ViewModels.Gui;


public class CollectionPageLayoutModel
{
    public required string PageTitle { get; set; }
    public required ViewCollection Collection { get; set; }
    public required ActiveCollectionPage ActivePage { get; set; }
}

public class CollectionPageLayoutModel<T>(T pageModel) : CollectionPageLayoutModel
{
    public T PageModel { get; } = pageModel;
}

public class CollectionSettingsPageVM
{

}


public class CollectionQuestionsPageVM
{

}


