using BlazorApp00.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios para Razor Pages y Blazor
builder.Services.AddRazorPages(); 
builder.Services.AddServerSideBlazor();
// Configurar HttpClient con la direcci�n base desde appsettings.json
builder.Services.AddHttpClient("ConsumoApi", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"]; 
    if (string.IsNullOrEmpty(baseUrl))
    {
        throw new InvalidOperationException("La configuraci�n 'ApiSettings:BaseUrl' no est� definida.");
    }

    client.BaseAddress = new Uri(baseUrl);

});

// Registrar ApiService como servicio inyectable
builder.Services.AddScoped<ApiService>();
var app = builder.Build();

// Configuraci�n del middleware en producci�n
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
// Middleware para HTTPS y recursos est�ticos
app.UseHttpsRedirection(); 
app.UseStaticFiles();

app.UseRouting();

// Configuraci�n de endpoints de Blazor
app.MapBlazorHub(); 
app.MapFallbackToPage("/_Host");

app.Run();

