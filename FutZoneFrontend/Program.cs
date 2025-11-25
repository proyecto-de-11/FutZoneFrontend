using FutZoneFrontend.Components;
using FutZoneFrontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Configuration;
using System.Net.Http; // Necesario para IHttpClientFactory
using Blazored.LocalStorage; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Authentication and Authorization
builder.Services.AddAuthenticationCore();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Add HttpClient and Auth Service
var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new InvalidOperationException("ApiBaseUrl configuration is missing. Please ensure appsettings.json or appsettings.{Environment}.json contains ApiBaseUrl configuration.");
}

// Register AuthService with HttpClient factory and Blazored LocalStorage
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600); // 10 minutos
});
builder.Services.AddScoped<IAuthService>(sp => sp.GetRequiredService<AuthService>());
builder.Services.AddScoped<FutZoneFrontend.Services.ILocalStorageService, LocalStorageService>();
builder.Services.AddHttpClient<IRolService, RolService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpClient<ITipoDeporteService, TipoDeporteService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpClient<IMembresiaService, MembresiaService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpClient<IPropietarioService, PropietarioService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpClient<IDashboardService, DashboardService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpClient<ICanchasService, CanchasService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpClient<IReservasService, ReservasService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
// Services stubs for endpoints that are referenced in API inventory
builder.Services.AddHttpClient<IDocumentosLegalesService, DocumentosLegalesService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpClient<IPreferenciasService, PreferenciasService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpClient<IAceptacionesService, AceptacionesService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpClient<EmpresaService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();