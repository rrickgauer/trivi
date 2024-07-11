using Trivi.Lib.Domain.Enums;

namespace Trivi.Lib.Domain.Attributes;

public abstract class AutoInjectBaseAttribute(AutoInjectionType autoInjectionType, InjectionProject project) : Attribute
{
    public AutoInjectionType AutoInjectionType { get; protected set; } = autoInjectionType;
    public InjectionProject Project { get; protected set; } = project;
    public abstract Type? InterfaceType { get; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class AutoInjectAttribute(AutoInjectionType autoInjectionType, InjectionProject project) : AutoInjectBaseAttribute(autoInjectionType, project)
{
    public override Type? InterfaceType => null;
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class AutoInjectAttribute<T>(AutoInjectionType autoInjectionType, InjectionProject project) : AutoInjectBaseAttribute(autoInjectionType, project)
{
    public override Type? InterfaceType => typeof(T);
}
