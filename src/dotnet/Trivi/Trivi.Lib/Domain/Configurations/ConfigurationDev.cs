namespace Trivi.Lib.Domain.Configurations;

public class ConfigurationDev : ConfigurationProduction, IConfigs
{
    public override bool IsProduction => false;
}

