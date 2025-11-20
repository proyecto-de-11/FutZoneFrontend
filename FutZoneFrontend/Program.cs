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

// Add HttpClient and Auth Service
builder.Services.AddHttpClient("Auth", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:Auth"]!);
});
builder.Services.AddHttpClient("Canchas", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:Canchas"]!);
});
builder.Services.AddHttpClient("Publicidad", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:Publicidad"]!);
});



builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ITipoDeporteService, TipoDeporteService>();
builder.Services.AddScoped<IPropietarioService, PropietarioService>();
// Services stubs for endpoints that are referenced in API inventory
builder.Services.AddScoped<IDocumentosLegalesService, DocumentosLegalesService>();
builder.Services.AddScoped<IPreferenciasService, PreferenciasService>();
builder.Services.AddScoped<IAceptacionesService, AceptacionesService>();

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
