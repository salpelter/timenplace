using BlazorBlueprint.Components;
using Microsoft.AspNetCore.Components;
using TimeNPlace.Components;
using TimeNPlace.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddControllers();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazorBlueprintComponents();

builder.Services.AddScoped(sp =>
{
    var navigation = sp.GetRequiredService<NavigationManager>();
    return new HttpClient()
    {
        BaseAddress = new Uri(navigation.BaseUri)
    };
});

builder.Services.AddHttpClient<GeosearchService>(client =>
{
    client.BaseAddress = new Uri("https://commons.wikimedia.org/w/api.php");
    client.DefaultRequestHeaders.Add("User-Agent", "time-n-place-crawler-bot/0.1 (User: amusable.wick197@passfwd.com)");
    // TODO: do i need Accept-Encoding: gzip?
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();
app.UseAntiforgery();

app.UseDefaultFiles();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();