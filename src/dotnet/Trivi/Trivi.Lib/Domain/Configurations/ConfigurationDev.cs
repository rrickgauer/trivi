namespace Trivi.Lib.Domain.Configurations;

public class ConfigurationDev : ConfigurationProduction, IConfigs
{
    public override bool IsProduction => false;

    public override uint MinimumUserPasswordLength => 1;

    public override string StaticWebFilesPath => @"C:\xampp\htdocs\files\trivi\src\dotnet\Trivi\Trivi.WebGui\wwwroot";
}

