using FutZoneFrontend.Components;
using FutZoneFrontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Authentication and Authorization
builder.Services.AddAuthenticationCore();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

// --- INICIO DE LA CONFIGURACIÓN DE MÚLTIPLES HTTPCLIENTS ---

// 1. Registro del Cliente para Autenticación (Login)
// Utiliza la configuración "AutenticacionUrl": https://apiautentificacion.onrender.com
builder.Services.AddHttpClient("AuthClient", client =>
{
    // Usamos el operador ?? para asegurar que la configuración existe.
    client.BaseAddress = new Uri(builder.Configuration["AutenticacionUrl"] ?? throw new InvalidOperationException("ERROR: La clave 'AutenticacionUrl' no está configurada."));
});

// 2. Registro del Cliente para la API Principal (Empresa/Publicidad)
// Utiliza la configuración "ApiBaseUrl": https://api-empresa-publicidad.onrender.com
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? throw new InvalidOperationException("ERROR: La clave 'ApiBaseUrl' no está configurada."));
});

// Nota: Al usar AddHttpClient, el servicio IHttpClientFactory se registra automáticamente.
// Ahora, tus servicios deben inyectar IHttpClientFactory.
// Los registros de servicios se mantienen, pero la inyección de dependencias dentro de ellos debe cambiar.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ITipoDeporteService, TipoDeporteService>();
builder.Services.AddScoped<IPropietarioService, PropietarioService>();

// --- FIN DE LA CONFIGURACIÓN DE MÚLTIPLES HTTPCLIENTS ---


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