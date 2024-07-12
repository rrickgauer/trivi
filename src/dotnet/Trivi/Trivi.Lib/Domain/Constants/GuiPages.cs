namespace Trivi.Lib.Domain.Constants;

public class GuiPages
{
    private const string Prefix = $"Views/Pages";

    public static string Login => Build("Auth/LoginPage");
    public static string Signup => Build("Auth/SignupPage");


    private static string Build(string path)
    {
        return $"{Prefix}/{path}.cshtml";
    }



}
