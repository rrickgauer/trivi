using Microsoft.Extensions.FileProviders;
using Trivi.Lib.Domain.Configurations;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Hubs.Lobby;
using Trivi.Lib.Hubs.Question;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Utility;



bool isProduction = true;

#if DEBUG
isProduction = false;

// DO THIS SO YOU DON'T HAVE TO LOG IN EVERY TIME
SessionManager.TESTING_MASTER_USER_ID = new(@"00000000-0000-0000-0000-000000000000");

// testing game id = mwN5mcux

#endif


IConfigs config = isProduction ? new ConfigurationProduction() : new ConfigurationDev();

var builder = WebApplication.CreateBuilder(args);

builder.SetupWebGui(isProduction);


var app = builder.Build();

#region - Load the error messages into memory -

var errorMessages = app.Services.GetRequiredService<IErrorMessageService>();
await errorMessages.LoadStaticErrorMessagesAsync();

#endregion

#region - Build and run the web application -

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



app.MapHub<GameLobbyHub>("/hubs/game-lobby", options =>
{
    options.AllowStatefulReconnects = true;
});


app.MapHub<GameHub>("/hubs/game-question", options =>
{
    options.AllowStatefulReconnects = true;
});

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.UseSession();

app.Run();

#endregion


