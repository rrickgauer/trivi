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

    public static string GameLobby => Build("Games/Game/Lobby/GameLobbyPage");

    public static string GameQuestionLayout => Build("Games/Game/Questions/_GameQuestionsLayout");
    public static string GameQuestionShortAnswer => Build("Games/Game/Questions/ShortAnswer/ShortAnswerGameQuestionPage");
    public static string GameQuestionTrueFalse => Build("Games/Game/Questions/TrueFalse/TrueFalseGameQuestionPage");
    public static string GameQuestionMulitpleChoice => Build("Games/Game/Questions/MultipleChoice/MultipleChoiceGameQuestionPage");

    public static string AdminLobby => Build("Games/Admin/Lobby/AdminLobbyPage");
    public static string AdminQuestion => Build("Games/Admin/Question/AdminQuestionPage");


    private static string Build(string path)
    {
        return $"{Prefix}/{path}.cshtml";
    }

}
