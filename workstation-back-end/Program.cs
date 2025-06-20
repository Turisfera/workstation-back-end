using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using workstation_back_end.Experience.Application.ExperienceCommandServices;
using workstation_back_end.Experience.Domain;
using workstation_back_end.Experience.Domain.Models.Queries;
using workstation_back_end.Experience.Domain.Models.Validators;
using workstation_back_end.Experience.Domain.Services;
using workstation_back_end.Shared.Domain.Repositories;
using workstation_back_end.Shared.Infraestructure.Persistence.Configuration;
using workstation_back_end.Shared.Infraestructure.Persistence.Repositories;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using workstation_back_end.Experience.Infraestructure;
using workstation_back_end.Inquiry.Application.CommandServices;
using workstation_back_end.Inquiry.Application.QueryServices;
using workstation_back_end.Inquiry.Domain.Services;
using workstation_back_end.Inquiry.Domain.Services.Models.Validators;
using workstation_back_end.Inquiry.Domain.Services.Services;
using workstation_back_end.Inquiry.Infraestructure;
using workstation_back_end.Security.Domain.Services;
using workstation_back_end.Security.Application.SecurityCommandServices;
using workstation_back_end.Security.Application.TokenServices;
using workstation_back_end.Users.Domain;
using workstation_back_end.Users.Domain.Services;
using workstation_back_end.Users.Infrastructure;
using workstation_back_end.Users.Application.CommandServices;
using workstation_back_end.Users.Application.QueryServices;
using workstation_back_end.Users.Domain.Models.Validadors;
var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TripMatch API",
        Description = "API to manage travel experiences"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa tu token aquí con el formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// Controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
});

// === JWT Auth ===
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
        };
    });

builder.Services.AddAuthorization(); // NECESARIO para evitar el error de UseAuthorization()

// Base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString == null)
    throw new Exception("Falta la cadena de conexión a la base de datos.");

builder.Services.AddDbContext<TripMatchContext>(options =>
{
    options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});



// Inyección de dependencias (Shared)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IExperienceCommandService, ExperienceCommandService>();
builder.Services.AddScoped<IExperienceQueryService, ExperienceQueryService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateExperienceCommandValidator>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUsuarioCommandValidator>();

builder.Services.AddScoped<IInquiryCommandService, InquiryCommandService>();
builder.Services.AddScoped<IInquiryQueryService, InquiryQueryService>();
builder.Services.AddScoped<IInquiryRepository, InquiryRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateInquiryCommandValidator>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

// Verificar creación de BD
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TripMatchContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();
app.Run();