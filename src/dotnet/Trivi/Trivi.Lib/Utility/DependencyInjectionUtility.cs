using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Configurations;
using Trivi.Lib.RouteConstraints;

namespace Trivi.Lib.Utility;

public static class DependencyInjectionUtility
{

    public static void InjectConfigs(IServiceCollection services, bool isProduction)
    {
        if (isProduction)
        {
            services.AddSingleton<IConfigs, ConfigurationProduction>();
        }
        else
        {
            services.AddSingleton<IConfigs, ConfigurationDev>();
        }
    }

    #region - Auto Inject -

    public static void InjectServicesIntoAssembly(IServiceCollection services, InjectionProject projectType, Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes().Where(t => t.IsClass && t.GetCustomAttribute<AutoInjectBaseAttribute>() != null).ToList() ?? new List<Type>();

        foreach (var serviceType in serviceTypes)
        {
            InjectService(services, projectType, serviceType);
        }
    }

    private static void InjectService(IServiceCollection services, InjectionProject project, Type serviceType)
    {
        if (serviceType.GetCustomAttribute<AutoInjectBaseAttribute>() is not AutoInjectBaseAttribute attr)
        {
            return;
        }

        if ((attr.Project & project) == 0)
        {
            return;
        }

        if (attr.InterfaceType != null)
        {
            GetInterfaceInjectionMethod(services, attr)(attr.InterfaceType, serviceType);
        }
        else
        {
            GetInjectionMethod(services, attr)(serviceType);
        }
    }

    private static Func<Type, IServiceCollection> GetInjectionMethod(IServiceCollection services, AutoInjectBaseAttribute attr)
    {
        return attr.AutoInjectionType switch
        {
            AutoInjectionType.Singleton => services.AddSingleton,
            AutoInjectionType.Scoped => services.AddScoped,
            AutoInjectionType.Transient => services.AddTransient,
            _ => throw new NotImplementedException(),
        };
    }

    private static Func<Type, Type, IServiceCollection> GetInterfaceInjectionMethod(IServiceCollection services, AutoInjectBaseAttribute attr)
    {
        return attr.AutoInjectionType switch
        {
            AutoInjectionType.Singleton => services.AddSingleton,
            AutoInjectionType.Scoped => services.AddScoped,
            AutoInjectionType.Transient => services.AddTransient,
            _ => throw new NotImplementedException(),
        };
    }

    #endregion


    public static void ConfigureRouteConstraints(this RouteOptions options)
    {
        var assembly = Assembly.GetAssembly(typeof(QuestionIdRouteConstraint));

        if (assembly == null)
        {
            return;
        }

        var customConstraints = assembly.GetTypes().Where(t => t.IsClass && t.GetCustomAttribute<ConstraintKeyAttribute>() != null);

        foreach ( var constraintType in customConstraints)
        {
            string key = constraintType.GetCustomAttribute<ConstraintKeyAttribute>()!.Key;
            options.ConstraintMap.Add(key, constraintType);
        }
    }


    public static bool IsTypeOf<T>(this Type type)
    {
        return typeof(T).IsAssignableFrom(type);
    }



}
