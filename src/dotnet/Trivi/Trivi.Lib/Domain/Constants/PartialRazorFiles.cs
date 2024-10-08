﻿namespace Trivi.Lib.Domain.Constants;

public class PartialRazorFiles
{
    private const string IncludesDirectory = @"~/Views/Includes";

    public static string Header        => Build(@"_Header");
    public static string Footer        => Build(@"_Footer");
    public static string MessageBoxes  => Build("_MessageBoxes");
    public static string Navbar        => Build("_Navbar");
    public static string PageBottom    => Build("_PageBottom");
    public static string PageLoading   => Build("_PageLoading");
    public static string SpinnerCenter => Build("_SpinnerCenter");
    public static string AlertsPageTop => Build("_AlertsPageTop");
    public static string BundleTop     => Build("_IncludeBundle");
    public static string Toasts        => Build("_ToastsContainer");

    private static string Build(string path)
    {
        return $"{IncludesDirectory}/{path}.cshtml";
    }


}
