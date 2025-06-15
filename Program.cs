using tripmatch_back.Shared.Domain;
using tripmatch_back.Shared.Infrastructure.Persistence.Repositories;
using tripmatch_back.Shared.Infrastructure.Persistence.Configuration;
using tripmatch_back.Users.Domain;
using tripmatch_back.Users.Domain.Services;
using tripmatch_back.Users.Infrastructure;
using tripmatch_back.Users.Application.CommandServices;
using tripmatch_back.Users.Application.QueryServices;
using tripmatch_back.Users.Domain.Models.Validadors;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI/Swagger Services
builder.Services.AddControllers();
builder.Services.AddOpenApi();  // Usa Swashbuckle.AspNetCore.OpenApi si estás en .NET 7+
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Esto activa Swagger (via Swashbuckle)
// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null)
    throw new Exception("Database connection string is not set.");

// Configure EF Core with MySQL
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<TripMatchContext>(options =>
    {
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
    });
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<TripMatchContext>(options =>
    {
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Error)
               .EnableDetailedErrors();
    });
}

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUsuarioCommandValidator>();

builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();

// Ensure DB is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TripMatchContext>();
    context.Database.EnsureCreated();
}

// OpenAPI en desarrollo
if (app.Environment.IsDevelopment())
{
   
    app.UseSwagger();
    app.UseSwaggerUI(); // Muestra
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();