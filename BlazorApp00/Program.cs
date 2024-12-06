using BlazorApp00.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios para Razor Pages y Blazor
builder.Services.AddRazorPages(); 
builder.Services.AddServerSideBlazor();
// Configurar HttpClient con la dirección base desde appsettings.json
builder.Services.AddHttpClient("ConsumoApi", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"]; 
    if (string.IsNullOrEmpty(baseUrl))
    {
        throw new InvalidOperationException("La configuración 'ApiSettings:BaseUrl' no está definida.");
    }

    client.BaseAddress = new Uri(baseUrl);

});

// Registrar ApiService como servicio inyectable
builder.Services.AddScoped<ApiService>();
var app = builder.Build();

// Configuración del middleware en producción
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
// Middleware para HTTPS y recursos estáticos
app.UseHttpsRedirection(); 
app.UseStaticFiles();

app.UseRouting();

// Configuración de endpoints de Blazor
app.MapBlazorHub(); 
app.MapFallbackToPage("/_Host");

app.Run();

