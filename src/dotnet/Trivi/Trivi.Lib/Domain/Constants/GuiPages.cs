namespace Trivi.Lib.Domain.Constants;

public class GuiPages
{
    private const string Prefix = $"Views/Pages";

    public static string Login => Build("Auth/LoginPage");
    public static string Signup => Build("Auth/SignupPage");

    public static string Home => Build("Home/HomePage");

    public static string Collections => Build("Collections/CollectionsPage");

    public static string CollectionLayout => Build("Collection/_CollectionLayout");
    public static string CollectionSettings => Build("Collection/Settings/CollectionSettingsPage");
    public static string CollectionQuestions => Build("Collection/Questions/CollectionQuestionsPage");
    public static string CollectionSetup => Build("Collection/Setup/SetupPage");


    public static string GameJoin => Build("Games/Join/JoinGamePage");

    private static string Build(string path)
    {
        return $"{Prefix}/{path}.cshtml";
    }

}
