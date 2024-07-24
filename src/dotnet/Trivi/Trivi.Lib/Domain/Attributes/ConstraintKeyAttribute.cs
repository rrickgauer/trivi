namespace Trivi.Lib.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ConstraintKeyAttribute(string key) : Attribute
{
    public string Key { get; } = key;
}
