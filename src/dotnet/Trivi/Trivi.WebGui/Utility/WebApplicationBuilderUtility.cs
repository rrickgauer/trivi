using System.Reflection;
using Trivi.Lib.Domain.Configurations;
using Trivi.Lib.Filters;
using Trivi.Lib.JsonConverters;
using Trivi.Lib.Utility;

namespace Trivi.WebGui.Utility;

public static class WebApplicationBuilderUtility
{
    public static WebApplicationBuilder SetupWebGui(this WebApplicationBuilder builder, bool isProduction)
    {
        builder.Services.Configure<RouteOptions>(options =>
        {
            options.ConfigureRouteConstraints();
        });


        // Add services to the container.
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<HttpResponseExceptionFilter>();
            options.Filters.Add<ValidationErrorFilter>();

            options.SuppressAsyncSuffixInActionNames = false;
        })

        .AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Add("/{0}.cshtml");
            options.ViewLocationFormats.Add("/");
        })

        // https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-8.0#disable-automatic-400-response
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressMapClientErrors = true;
        })

        .AddJsonOptions(options =>
        {
            if (!isProduction)
            {
                options.JsonSerializerOptions.WriteIndented = true;
            }

            options.JsonSerializerOptions.Converters.Add(new ServiceDataResponseFactory());
        });


        builder.Services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = true;
            //options.KeepAliveInterval = TimeSpan.FromSeconds(60);
        });

        // session management
        builder.Services.AddDistributedMemoryCache();

        builder.Services.AddSession(options =>
        {
            options.Cookie.Name = GuiSessionKeys.SessionName;
            options.Cookie.IsEssential = true;
        });

        builder.InjectDependencies(isProduction);

        return builder;
    }

    private static WebApplicationBuilder InjectDependencies(this WebApplicationBuilder builder, bool isProduction)
    {
        // inject the appropriate IConfigs instance
        DependencyInjectionUtility.InjectConfigs(builder.Services, isProduction);

        // inject the services into the web application
        List<Assembly?> assemblies = new()
        {
            Assembly.GetAssembly(typeof(IConfigs)),
            Assembly.GetCallingAssembly(),
        };

        InjectionProject projectTypes = InjectionProject.Always | InjectionProject.WebGui;

        foreach (var assembly in assemblies)
        {
            if (assembly != null)
            {
                DependencyInjectionUtility.InjectServicesIntoAssembly(builder.Services, projectTypes, assembly);
            }
        }

        // additional services to inject
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return builder;
    }
}
