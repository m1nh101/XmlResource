using Microsoft.Extensions.Localization;
using MultiLanguage.Components;
using MultiLanguage.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddLocalization();

builder.Services.AddSingleton<IStringLocalizerFactory, XmlStringLocalizerFactory>();
builder.Services.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
builder.Services.AddTransient(typeof(IStringLocalizer), typeof(StringLocalizer));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures(["en-US", "vi-VN"])
    .AddSupportedUICultures(["en-US", "vi-VN"]));

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
