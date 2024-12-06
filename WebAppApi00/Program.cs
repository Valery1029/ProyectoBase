using Microsoft.OpenApi.Models;
using WebAppApi00.Model;
using WebAppApi00.Services;
using WebAppApi00.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Configurar acceso a AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Configurar conexión MySQL
var connectionString =
builder.Configuration.GetConnectionString("DefaultConnection"); 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("Defaul tConnection"),
ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));


builder.Services.AddScoped<IUserService, UserService>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Authentication API",
        Description = "API de ejemplo con JWT en .NET 8"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey, 
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter " +
        "'Bearer' [space] and then your token " + "in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();
// Configurar el middleware de JWT
app.UseMiddleware<JwtMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT API v1"));
}

app.UseHttpsRedirection(); 
app.UseAuthorization(); 
app.MapControllers(); 
app.Run();
