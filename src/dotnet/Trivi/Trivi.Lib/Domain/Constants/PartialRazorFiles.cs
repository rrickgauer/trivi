namespace Trivi.Lib.Domain.Constants;

public class PartialRazorFiles
{
    private const string IncludesDirectory = @"~/Views/Includes";

   
    public static string Header => BuildIncludeFile(@"_Header");
    public static string Footer => BuildIncludeFile(@"_Footer");

    private static string BuildIncludeFile(string path)
    {
        return $"{IncludesDirectory}/{path}.cshtml";
    }


}
