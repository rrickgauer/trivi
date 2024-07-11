using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Trivi.Lib.Domain.Configurations;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.Enums;
using Trivi.Lib.Filters;
using Trivi.Lib.Utility;


bool isProduction = true;

#if DEBUG
isProduction = false;
#endif


IConfigs config = isProduction ? new ConfigurationProduction() : new ConfigurationDev();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
    //options.Filters.Add<ValidationErrorFilter>();
    //options.Filters.Add<AccessTokenFilter>();

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

    //options.JsonSerializerOptions.Converters.Add(new ServiceDataResponseFactory());
});


// session management
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = GuiSessionKeys.SessionName;
    options.Cookie.IsEssential = true;
});



#region - Dependency Injection -

// inject the appropriate IConfigs instance
DependencyInjectionUtility.InjectConfigs(builder.Services, isProduction);

// inject the services into the web application
List<Assembly?> assemblies = new()
{
    Assembly.GetAssembly(typeof(IConfigs)),
    Assembly.GetExecutingAssembly(),
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

#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(config.StaticWebFilesPath),
    ServeUnknownFileTypes = true,
});


app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.UseSession();

app.Run();
