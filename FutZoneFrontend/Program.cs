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

<<<<<<< HEAD
// Add HttpClient and Auth Service
var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new InvalidOperationException("ApiBaseUrl configuration is missing. Please ensure appsettings.json or appsettings.{Environment}.json contains ApiBaseUrl configuration.");
}

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(600); // 10 minutos
});
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
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
=======
// --- INICIO DE LA CONFIGURACIÓN DE MÚLTIPLES HTTPCLIENTS ---

// 1. Registro del Cliente para Autenticación (Login/Register)
builder.Services.AddHttpClient("AuthClient", client =>
{
    // Esto asegura que la URL base se toma de la configuración.
    client.BaseAddress = new Uri(builder.Configuration["AutenticacionUrl"] ?? throw new InvalidOperationException("ERROR: La clave 'AutenticacionUrl' no está configurada."));
});

// 2. Registro del Cliente para la API Principal (Empresa/Publicidad)
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? throw new InvalidOperationException("ERROR: La clave 'ApiBaseUrl' no está configurada."));
});

// Registrar Blazored Local Storage
builder.Services.AddBlazoredLocalStorage(); 

// Registrar AuthService, inyectando el cliente nombrado "AuthClient"
// Usamos un delegado para obtener el cliente configurado de IHttpClientFactory
builder.Services.AddScoped<IAuthService, AuthService>(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var authClient = clientFactory.CreateClient("AuthClient");
    
    // CORRECCIÓN AMBIGÜEDAD (CS0104): Usamos el nombre completamente cualificado 
    // para asegurar que obtenemos la interfaz de Blazored.LocalStorage.
    var localStorage = sp.GetRequiredService<Blazored.LocalStorage.ILocalStorageService>();
    
    // Inyectamos el HttpClient configurado al constructor de AuthService
    return new AuthService(authClient, localStorage);
});

// Servicios restantes
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ITipoDeporteService, TipoDeporteService>();
builder.Services.AddScoped<IPropietarioService, PropietarioService>();
builder.Services.AddScoped<IDocumentosLegalesService, DocumentosLegalesService>();
builder.Services.AddScoped<IPreferenciasService, PreferenciasService>();
builder.Services.AddScoped<IAceptacionesService, AceptacionesService>();
// Registrar EmpresaService, inyectando el cliente nombrado "ApiClient" y IAuthService
builder.Services.AddScoped<FutZoneFrontend.Services.EmpresaService>(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    // Obtenemos el cliente configurado para la API de Empresas
    var apiClient = clientFactory.CreateClient("ApiClient"); 
    
    // Obtenemos el servicio de autenticación para acceder al userId
    var authService = sp.GetRequiredService<IAuthService>();

    // Inyectamos ambos al constructor de EmpresaService
    return new FutZoneFrontend.Services.EmpresaService(apiClient, authService);
});
// --- FIN DE LA CONFIGURACIÓN DE MÚLTIPLES HTTPCLIENTS ---

>>>>>>> origin/dev

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