using System.Runtime.CompilerServices;

namespace Trivi.Lib.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class PrefixAttribute(string prefix, [CallerMemberName] string caller="") : Attribute
{
    public string Prefix { get; } = prefix;
    public string EnumName { get; } = caller;
}
