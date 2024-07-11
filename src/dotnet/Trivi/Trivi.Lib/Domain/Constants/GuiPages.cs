namespace Trivi.Lib.Domain.Constants;

public class GuiPages
{
    private const string Prefix = $"Views/Pages";

    public static string Login => Build("Auth/LoginPage");


    private static string Build(string path)
    {
        return $"{Prefix}/{path}.cshtml";
    }



}
